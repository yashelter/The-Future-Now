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
        SceneManager.LoadScene(PlayerPrefs.GetInt("level", 1));
        // made Acync
    }
    public void Exit()
    {
        Application.Quit();
    }
    public void RestoreDefoults()
    {
        PlayerPrefs.DeleteAll();
        string path = Application.persistentDataPath + "/player.annet";
        if (File.Exists(path))
        {
            System.IO.File.Delete(path);
        }
        // сбросить настройки
    }
}
