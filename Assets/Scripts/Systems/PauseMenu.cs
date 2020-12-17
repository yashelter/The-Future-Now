using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class PauseMenu : MonoBehaviour
{
    public void FreezeTime()
    {
        Time.timeScale = 0;
    }
    public void UnFreezeTime()
    {
        Time.timeScale = 1;
    }
    public void BackToMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }

}


