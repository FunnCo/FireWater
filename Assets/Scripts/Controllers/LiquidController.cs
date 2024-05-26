using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// Класс предназначен для управления поведением жидкости.
/// Жидкость может уничтожать игроков с определёнными тегами при их попадании в зону триггера.
/// </summary>
public class LiquidController : MonoBehaviour
{
    // Флаг, указывающий, может ли жидкость уничтожать игрока с тегом "Fire player"
    [SerializeField] private bool canKillFirePlayer = false;

    // Флаг, указывающий, может ли жидкость уничтожать игрока с тегом "Water player"
    [SerializeField] private bool canKillWaterPlayer = false;

    /// <summary>
    /// Метод вызывается при входе другого коллайдера в триггер.
    /// Проверяет тег коллайдера и уничтожает объект игрока, если он соответствует условиям.
    /// </summary>
    /// <param name="collision">Коллайдер, который вошёл в триггер.</param>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (canKillFirePlayer && collision.CompareTag("Fire player"))
        {
            Destroy(collision.gameObject);
            return;
        }
        if (canKillWaterPlayer && collision.CompareTag("Water player"))
        {
            Destroy(collision.gameObject);
            return;
        }
    }
}
