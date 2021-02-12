using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MenuSystem : MonoBehaviour
{
    public TextMeshProUGUI advText;
    public void Start()
    {
        SetAdvText();
    }
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
    public void SetAdvText()
    {
        advText.text = FindObjectOfType<LocalizationSystem>().GetKey("advText1") + " "
            + PlayerPrefs.GetInt("rebornings", 0) + " " 
            + FindObjectOfType<LocalizationSystem>().GetKey("advText2");
    }
   
}
