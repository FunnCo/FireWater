using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


/// <summary>
/// Класс предназначен для управления движением игрока.
/// Обеспечивает перемещение игрока, прыжки и обработку столкновений с землей.
/// </summary>
public class PlayerMovementController : MonoBehaviour
{
    // Rigidbody игрока
    private Rigidbody2D rbPlayer;
    
    // Горизонтальное направление движения игрока
    private float horizontalDirecton = 0f;
    
    // Слой для определения земли
    public LayerMask groundLayerMask;
    
    // Слой для определения подвижной земли
    public LayerMask movableGroundLayerMask;
    
    // Физический материал для скользкой поверхности
    public PhysicsMaterial2D slipperyMaterial;
    
    // Физический материал для нормальной поверхности
    public PhysicsMaterial2D normalMaterial;
    
    // Скорость движения игрока
    public float playerSpeed;
    
    // Дистанция проверки земли
    public float groundCheckDistance;
    
    // Сила прыжка игрока
    public float playerJumpForce = 10;
    
    // Компонент SpriteRenderer игрока
    private SpriteRenderer playerRenderer;
    
    // Коллайдер игрока
    private CapsuleCollider2D playerCollider;
    
    // Аниматор игрока
    private Animator playernimator;
    
    // Флаг, указывающий, смотрит ли игрок вправо
    private bool isFacingRight = true;
    
    // Количество прыжков, выполненных игроком
    private int jumpCount = 0;
    
    // Флаг, указывающий, должен ли игрок прыгнуть
    private bool shouldJump = false;

    /// <summary>
    /// Метод Start вызывается при инициализации объекта.
    /// Инициализирует компоненты Rigidbody2D, SpriteRenderer, Animator и CapsuleCollider2D.
    /// </summary>
    void Start()
    {
        rbPlayer = GetComponent<Rigidbody2D>();
        playerRenderer = GetComponent<SpriteRenderer>();
        playernimator = GetComponent<Animator>();
        playerCollider = GetComponent<CapsuleCollider2D>();
    }

    /// <summary>
    /// Метод Update вызывается на каждом кадре.
    /// Управляет сменой направления взгляда игрока.
    /// </summary>
    void Update()
    {
        Flip();
    }

    /// <summary>
    /// Метод для получения угла поверхности под игроком.
    /// Проверяет наличие земли под игроком и возвращает угол поверхности, если земля найдена.
    /// </summary>
    /// <returns>Угол поверхности под игроком или null, если земля не найдена.</returns>
    private float? getGroundedAngleOrNull()
    {
        Vector2 bottomOfPlayer = transform.position - new Vector3(0f, playerCollider.size.y / 2);
        RaycastHit2D staticGroundHit = Physics2D.Raycast(bottomOfPlayer, Vector2.down, groundCheckDistance, groundLayerMask);
        RaycastHit2D movableGroundHit = Physics2D.Raycast(bottomOfPlayer, Vector2.down, groundCheckDistance, movableGroundLayerMask);

        if (staticGroundHit)
        {
            Debug.DrawRay(staticGroundHit.point, staticGroundHit.normal, Color.red);
            return Vector2.Angle(Vector2.up, staticGroundHit.normal);
        }
        if (movableGroundHit)
        {
            Debug.DrawRay(movableGroundHit.point, movableGroundHit.normal, Color.red);
            return Vector2.Angle(Vector2.up, movableGroundHit.normal);
        }

        return null;
    }

    /// <summary>
    /// Метод FixedUpdate вызывается на каждом фиксированном кадре.
    /// Управляет движением, прыжками и физикой игрока.
    /// </summary>
    private void FixedUpdate()
    {
        float? groundAngle = getGroundedAngleOrNull();
        bool isGrounded = false;

        if (groundAngle <= 50)
        {
            isGrounded = true;
            rbPlayer.AddForce(new Vector2(0f, -300f));
            rbPlayer.sharedMaterial = normalMaterial;
        }

        playernimator.SetBool("is_in_air", !isGrounded);

        if (isGrounded)
        {
            jumpCount = 0;
            playernimator.SetBool("is_running", horizontalDirecton != 0f);
            playernimator.SetFloat("vertical_speed", 0f);
        }

        if (!isGrounded || groundAngle > 50)
        {
            playernimator.SetBool("is_running", false);
            playernimator.SetFloat("vertical_speed", (float)Math.Round(rbPlayer.velocity.y, 4));
            rbPlayer.sharedMaterial = slipperyMaterial;
        }

        if (shouldJump && jumpCount < 2)
        {
            if (groundAngle <= 50)
            {
                rbPlayer.AddForce(new Vector2(horizontalDirecton * 10f, 300f));
            }
            Debug.Log("Jumped with jumpcount=" + jumpCount);
            shouldJump = false;
            jumpCount++;
            rbPlayer.velocity = new Vector2(rbPlayer.velocity.x, playerJumpForce);
        }

        rbPlayer.velocity = new Vector2(horizontalDirecton * playerSpeed, rbPlayer.velocity.y);
    }

    /// <summary>
    /// Метод для смены направления взгляда игрока.
    /// Переворачивает спрайт игрока при изменении направления движения.
    /// </summary>
    private void Flip()
    {
        if (isFacingRight && horizontalDirecton < 0f || !isFacingRight && horizontalDirecton > 0f)
        {
            isFacingRight = !isFacingRight;
            playerRenderer.flipX = !playerRenderer.flipX;
        }
    }

    /// <summary>
    /// Метод для управления движением игрока.
    /// Вызывается при изменении направления движения.
    /// </summary>
    /// <param name="context">Контекст ввода, содержащий данные о направлении движения.</param>
    public void Move(InputAction.CallbackContext context)
    {
        horizontalDirecton = context.ReadValue<Vector2>().x;

        if (horizontalDirecton != 0)
        {
            horizontalDirecton = horizontalDirecton * (1 / Math.Abs(horizontalDirecton));
        }
    }

    /// <summary>
    /// Метод для выполнения прыжка игрока.
    /// Вызывается при начале прыжка.
    /// </summary>
    /// <param name="context">Контекст ввода, содержащий данные о прыжке.</param>
    public void Jump(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            if (jumpCount < 2)
            {
                shouldJump = true;
            }
        }
    }
}

