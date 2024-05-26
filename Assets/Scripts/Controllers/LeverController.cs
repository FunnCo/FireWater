using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;


/// <summary>
/// Класс предназначен для управления рычагом, который активирует или деактивирует объекты при повороте.
/// </summary>
public class LeverController : MonoBehaviour
{
    // Список объектов, которые должны быть активированы
    [SerializeField] private List<Object> objectsToActivate;

    // Список объектов, реализующих интерфейс IActivateble
    private List<IActivateble> activatableTargets => objectsToActivate.Select(obj => obj.GetComponent<IActivateble>()).ToList();

    /// <summary>
    /// Метод Update вызывается на каждом кадре.
    /// Проверяет поворот рычага и активирует или деактивирует объекты в зависимости от угла поворота.
    /// </summary>
    void Update()
    {
        if (transform.rotation.eulerAngles.z < 50)
        {
            activatableTargets.ForEach(obj => obj.Activate());
        }
        if (transform.rotation.eulerAngles.z > 310)
        {
            activatableTargets.ForEach(obj => obj.Deactivate());
        }
    }
}
