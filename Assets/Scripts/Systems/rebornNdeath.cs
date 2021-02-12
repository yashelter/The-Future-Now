using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

// Возвращает игрока к жизни, после нажатия на кнопку просмотра рекламы
public class rebornNdeath : MonoBehaviour
{
    public TextMeshProUGUI text;
    public void Start()
    {
        text.text = FindObjectOfType<LocalizationSystem>().GetKey("remainRebornings") + " " 
            + PlayerPrefs.GetInt("rebornings", 0);
    }
    public void RebornPlayer()
    {
        if (PlayerPrefs.GetInt("rebornings", 0) <= 0)
        {
            KillPlayer();
        }
        else
        {
            FindObjectOfType<PlayerController>().ReturnAlive();
            PlayerPrefs.SetInt("rebornings", PlayerPrefs.GetInt("rebornings", 1) - 1);
            Start();
        }
    }
    public void KillPlayer()
    {
        FindObjectOfType<PlayerController>().Death();
    }
    
}
