using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneManagerment : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            PlayerPrefs.SetInt("level", (PlayerPrefs.GetInt("level")) + 1);
            gameObject.AddComponent<MenuSystem>().LoadGame();
        }
        
        
    }
   
}
