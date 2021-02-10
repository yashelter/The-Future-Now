using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public int damage;
    private bool giveDamage = false;
    [HideInInspector]public bool inCombat = false;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (inCombat)
        {
            CheckAndDamage(collision);
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
          if(inCombat && !giveDamage)
        {
            CheckAndDamage(collision);
        }
    }
    private void CheckAndDamage(Collider2D collision)
    {
        if (collision.CompareTag("Enemy") && collision.gameObject.GetComponent<AIController>().isHitable)
        {
            collision.GetComponent<AIController>().GetDamage(damage);
            giveDamage = true;
        }
        else if (collision.CompareTag("Player") && collision.gameObject.GetComponent<PlayerController>().GetLife() &&
            collision.gameObject.GetComponent<PlayerController>().isHitable)
        {
            giveDamage = true;
            collision.GetComponent<PlayerController>().GetDamage(damage);
        }

    }

}
