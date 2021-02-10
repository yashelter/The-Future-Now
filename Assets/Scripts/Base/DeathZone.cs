using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathZone : MonoBehaviour
{
    public int damage = 0;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // пока умирают все, но вохможно это не нужно
        if(collision.gameObject.CompareTag("Player") &&
            collision.gameObject.GetComponent<PlayerController>().GetLife() &&
            collision.gameObject.GetComponent<PlayerController>().isHitable)
        {
            collision.GetComponent<PlayerController>().GetDamage(damage);
        }
    }
}
