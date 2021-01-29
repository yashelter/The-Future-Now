using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour, Damageable
{
    public int heath = 10;
    public void GetDamage(int damage)
    {
        heath -= damage;
        if(heath <= 0)
        {
            Destroy(gameObject);
        }
    }
}
