using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


/// <summary>
/// Класс предназначен для переключения спавна между игровыми персонажами.
/// Управляет текущим активным персонажем и изменяет его при необходимости.
/// </summary>
public class CharacterSwitcher : MonoBehaviour
{
    // Текущий индекс выбранного персонажа
    private int index = 0;

    // Список игровых объектов, представляющих персонажей
    [SerializeField] private List<GameObject> players = new List<GameObject>();

    // Менеджер ввода игрока
    private PlayerInputManager manager;

    /// <summary>
    /// Метод Start вызывается при инициализации объекта.
    /// Инициализирует менеджер ввода игрока и устанавливает первого персонажа.
    /// </summary>
    void Start()
    {
        manager = GetComponent<PlayerInputManager>();
        manager.playerPrefab = players[index];
        index++;
    }

    /// <summary>
    /// Метод для переключения на следующего персонажа при спавне.
    /// Изменяет активного персонажа на следующего в списке.
    /// </summary>
    public void SwitchNewxtSpawnCharacter()
    {
        manager.playerPrefab = players[index];
    }
}