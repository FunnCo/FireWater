using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Класс предназначен для управления движением активируемого объекта.
/// Объект может перемещаться вертикально или горизонтально между двумя позициями
/// и изменять своё состояние при активации или деактивации.
/// </summary>
public class ActivatableMovingObjectController : MonoBehaviour, IActivateble
{
    // Поле, указывающее, находится ли объект в закрытом состоянии
    [SerializeField] bool isClosed;

    // Поле для хранения начального состояния закрытия объекта
    private bool initClosedState;

    // Начальная позиция объекта
    private Vector2 startPosition;

    // Конечная позиция объекта
    private Vector2 endPosition;

    // Скорость движения объекта
    [SerializeField] public float speed;

    // Флаг, указывающий, движется ли объект вертикально
    [SerializeField] public bool isVertical = true;

    // Коэффициент, определяющий дальность движения объекта
    [SerializeField] public float moveWeight = 1;

    /// <summary>
    /// Метод Start вызывается при инициализации объекта.
    /// Инициализирует начальную и конечную позиции объекта, а также начальное состояние закрытия.
    /// </summary>
    void Start()
    {
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();

        startPosition = this.transform.position;
        if (isVertical)
        {
            endPosition = new Vector2(startPosition.x, startPosition.y + spriteRenderer.bounds.size.y * moveWeight);
        }
        else
        {
            endPosition = new Vector2(startPosition.x + spriteRenderer.bounds.size.x * moveWeight, startPosition.y);
        }

        initClosedState = isClosed;
    }

    /// <summary>
    /// Метод FixedUpdate вызывается на каждом фиксированном кадре.
    /// Управляет движением объекта между начальной и конечной позициями.
    /// </summary>
    private void FixedUpdate()
    {
        Vector2 currentPosition = this.transform.position;
        Vector2 targetPosition = isClosed ? startPosition : endPosition;
        bool shouldBeMoving = (isClosed && currentPosition != startPosition) || (!isClosed && currentPosition != endPosition);

        if (shouldBeMoving)
        {
            this.transform.position = Vector2.MoveTowards(currentPosition, targetPosition, speed * Time.deltaTime);
        }
    }

    /// <summary>
    /// Метод для активации объекта.
    /// Изменяет состояние объекта на противоположное от начального.
    /// </summary>
    public void Activate()
    {
        isClosed = !initClosedState;
    }

    /// <summary>
    /// Метод для деактивации объекта.
    /// Возвращает объект в начальное состояние.
    /// </summary>
    public void Deactivate()
    {
        isClosed = initClosedState;
    }
}
