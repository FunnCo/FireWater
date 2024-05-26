using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovementController : MonoBehaviour
{
    // Start is called before the first frame update

    private Rigidbody2D rbPlayer;
    private float horizontalDirecton = 0f;

    // A mask determining what is ground to the character
    public LayerMask groundLayerMask;
    public LayerMask movableGroundLayerMask;

    public PhysicsMaterial2D slipperyMaterial;
    public PhysicsMaterial2D normalMaterial;

    public float playerSpeed;
    public float groundCheckDistance;
    public float playerJumpForce = 10;

    private SpriteRenderer playerRenderer;
    private CapsuleCollider2D playerCollider;
    private Animator playernimator;
    
    private bool isFacingRight = true;

    private int jumpCount = 0;
    private bool shouldJump = false;
    

    void Start()
    {
        rbPlayer = GetComponent<Rigidbody2D>();
        playerRenderer = GetComponent<SpriteRenderer>();
        playernimator = GetComponent<Animator>();
        playerCollider = GetComponent<CapsuleCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Flip();      
    }     

    private float? getGroundedAngleOrNull()
    {
        Vector2 bottomOfPlayer = transform.position - new Vector3(0f, playerCollider.size.y/2);
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
        
        if(!isGrounded || groundAngle > 50)
        {
            
            playernimator.SetBool("is_running", false);
            playernimator.SetFloat("vertical_speed", (float)Math.Round(rbPlayer.velocity.y, 4));
            rbPlayer.sharedMaterial = slipperyMaterial;
        }

        if (shouldJump && jumpCount < 2)
        {
            if(groundAngle <= 50)
            {
                rbPlayer.AddForce(new Vector2(horizontalDirecton*10f, 300f));
            }
            Debug.Log("Jumped with jumpcount=" + jumpCount);
            shouldJump = false;
            jumpCount++;
            rbPlayer.velocity = new Vector2(rbPlayer.velocity.x, playerJumpForce);            
        }        

        rbPlayer.velocity = new Vector2(horizontalDirecton*playerSpeed, rbPlayer.velocity.y);             
    }

    private void Flip()
    {
        if (isFacingRight && horizontalDirecton < 0f || !isFacingRight && horizontalDirecton > 0f)
        {
            isFacingRight = !isFacingRight;
            playerRenderer.flipX = !playerRenderer.flipX;
        }
    }


    public void Move(InputAction.CallbackContext context)
    {
        horizontalDirecton = context.ReadValue<Vector2>().x;
        if (horizontalDirecton != 0)
        {
            horizontalDirecton = horizontalDirecton * (1 / Math.Abs(horizontalDirecton));
        }
    }

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
