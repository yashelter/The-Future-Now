﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public int damage;
    [HideInInspector]public bool inCombat = false;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(inCombat && collision.CompareTag("Enemy"))
        {
            collision.GetComponent<AIController>().GetDamage(damage);
        }
        else if(inCombat && collision.CompareTag("Player") && collision.gameObject.GetComponent<PlayerController>().GetLife())
        {
            collision.GetComponent<PlayerController>().GetDamage(damage);
        }
    }   

}
