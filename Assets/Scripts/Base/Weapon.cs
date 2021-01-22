using System.Collections;
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
    }   

}
