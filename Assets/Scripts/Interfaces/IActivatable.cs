using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Интерфейс, предназначенный для объектов, которые могут быть активированы и деактивированы.
/// </summary>
public interface IActivateble
{
    /// <summary>
    /// Метод для активации объекта.
    /// </summary>
    void Activate();

    /// <summary>
    /// Метод для деактивации объекта.
    /// </summary>
    void Deactivate();
}