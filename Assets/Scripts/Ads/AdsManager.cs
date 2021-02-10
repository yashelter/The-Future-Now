using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;

public class AdsManager : MonoBehaviour, IUnityAdsListener
{
    private string gameId { get; } = "3994835";
    private bool testMode { get; } = true;
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
        if (Advertisement.GetPlacementState() == PlacementState.Ready)
        {
            if (Advertisement.IsReady() && type == "rewardedVideo")
            {
                Advertisement.Show("rewardedVideo");
            }
            else if ((Advertisement.IsReady() && type == "video"))
            {
                Advertisement.Show("video");
            }
        }
        else
        {
            // hmmmmm
        }

    }

    void IUnityAdsListener.OnUnityAdsReady(string placementId)
    {
        // Реклама готова, можно запускать
    }

    void IUnityAdsListener.OnUnityAdsDidError(string message)
    {
        Debug.LogWarning("Error with ads, fix pls");
        Time.timeScale = 1;
        player.BeAlive();
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
            Time.timeScale = 1;
            player.BeAlive();
            // награда для пользователя за то, что посмотрел ролик.
        }
        else if (showResult == ShowResult.Skipped || showResult == ShowResult.Failed)
        {
            Time.timeScale = 1;
            Debug.LogWarning("Реклама была пропущена или возникла ошибка");
            // не вознаграждайте пользователя за пропуск объявления.
        }
    }
}
