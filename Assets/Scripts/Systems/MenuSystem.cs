using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MenuSystem : MonoBehaviour
{
    public void LoadGame()
    {
        if (!PlayerPrefs.HasKey("level"))
        {
            PlayerPrefs.SetInt("level", 1);
        }
        SceneManager.LoadScene(PlayerPrefs.GetInt("level"));
        // made Acync
    }
    public void Exit()
    {
        Application.Quit();
    }
    public void RestoreDefoults()
    {
        string lang = PlayerPrefs.GetString("language");
        PlayerPrefs.DeleteAll();
        PlayerPrefs.SetString("language", lang);
        PlayerPrefs.SetInt("rebornings", 3);
        string path = Application.persistentDataPath + "/player.annet";
        if (File.Exists(path))
        {
            File.Delete(path);
        }
        // сбросить настройки
    }
   
}
