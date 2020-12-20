using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;

public class AdsManager : MonoBehaviour, IUnityAdsListener
{
    private string gameId = "3939689";
    private bool testMode = false;
    private PlayerController player;
    void Start()
    {
        player = FindObjectOfType<PlayerController>();
        if (Advertisement.isSupported)
        {
            Advertisement.Initialize(gameId, testMode);
            Advertisement.AddListener(this);
        }
    }

    public void Show(string type = "rewardedVideo")
    {
        if (Advertisement.IsReady() && type == "rewardedVideo")
        {
            Advertisement.Show("rewardedVideo");
        }else if((Advertisement.IsReady() && type == "video"))
        {
            Advertisement.Show("video");
        }

    }

    void IUnityAdsListener.OnUnityAdsReady(string placementId)
    {
        // Реклама готова, можно запускать
    }

    void IUnityAdsListener.OnUnityAdsDidError(string message)
    {
        // ошибка
    }

    void IUnityAdsListener.OnUnityAdsDidStart(string placementId)
    {
        Time.timeScale = 0;
        // дополнительные действия, которые необходимо предпринять, когда конечные пользователи запускают объявление.
    }

    void IUnityAdsListener.OnUnityAdsDidFinish(string placementId, ShowResult showResult)
    {
        if (placementId == "rewardedVideo" && showResult == ShowResult.Finished)
        {
            player.ReturnAlive();
            // награда для пользователя за то, что посмотрел ролик.
        }
        else if (showResult == ShowResult.Skipped || showResult == ShowResult.Failed)
        {
            Debug.LogWarning("Реклама была пропущена или возникла ошибка");
            // не вознаграждайте пользователя за пропуск объявления.
        }
    }
}
