using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterSwitcher : MonoBehaviour
{
    int index = 0;
    [SerializeField] List<GameObject> players = new List<GameObject>();
    PlayerInputManager manager;

    void Start()
    {
        manager = GetComponent<PlayerInputManager>();        
        manager.playerPrefab = players[index];
        index++;
    }

    // Update is called once per frame
    public void SwitchNewxtSpawnCharacter()
    {
        manager.playerPrefab = players[index];
    }
}
