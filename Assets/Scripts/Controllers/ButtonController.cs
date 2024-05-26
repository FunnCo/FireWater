using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

/// <summary>
/// Класс предназначен для управления кнопкой, которая активирует или деактивирует 
/// заданные объекты при нажатии.
/// </summary>
public class ButtonController : MonoBehaviour
{
    // Список объектов, которые будут активированы
    [SerializeField] List<Object> objectsToActivate;

    // Список объектов, реализующих интерфейс IActivateble, которые могут быть активированы или деактивированы
    List<IActivateble> activatableTargets => objectsToActivate.Select(obj => obj.GetComponent<IActivateble>()).ToList();

    // Флаг, указывающий, нажата ли кнопка
    bool isButtonPressed = false;

    /// <summary>
    /// Метод вызывается при входе другого объекта в триггер кнопки.
    /// Устанавливает флаг, что кнопка нажата.
    /// </summary>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        isButtonPressed = true;
    }

    /// <summary>
    /// Метод вызывается при выходе другого объекта из триггера кнопки.
    /// Сбрасывает флаг, что кнопка нажата, и деактивирует все цели.
    /// </summary>
    private void OnTriggerExit2D(Collider2D collision)
    {
        isButtonPressed = false;
        activatableTargets.ForEach(obj => obj.Deactivate());
    }

    /// <summary>
    /// Метод Update вызывается на каждом кадре.
    /// Активирует все цели, если кнопка нажата.
    /// </summary>
    void Update()
    {
        if (isButtonPressed)
        {
            activatableTargets.ForEach(obj => obj.Activate());
        }
    }
}
