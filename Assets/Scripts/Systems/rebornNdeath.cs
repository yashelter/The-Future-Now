using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rebornNdeath : MonoBehaviour
{
    // Возвращает игрока к жизни, после нажатия на кнопку просмотра рекламы
    public void RebornPlayer()
    {
        FindObjectOfType<PlayerController>().ReturnAlive();
    }
    public void KillPlayer()
    {
        FindObjectOfType<PlayerController>().Death();
    }
}
