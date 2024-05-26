using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LiquidController : MonoBehaviour
{
    [SerializeField] bool canKillFirePlayer = false;
    [SerializeField] bool canKillWaterPlayer = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(canKillFirePlayer && collision.tag == "Fire player")
        {
            Destroy(collision.gameObject);
            return;
        }
        if (canKillWaterPlayer && collision.tag == "Water player")
        {
            Destroy(collision.gameObject);
            return;
        }
    }

}
