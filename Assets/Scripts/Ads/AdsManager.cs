using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;

public class AdsManager : MonoBehaviour, IUnityAdsListener
{
    private MenuSystem ms;
    private string gameId { get; } = "3994835";
    private bool testMode { get; } = true;
    private void Start()
    {
        if (Advertisement.isSupported)
        {
            Advertisement.Initialize(gameId, testMode);
            Advertisement.AddListener(this);
            ms = FindObjectOfType<MenuSystem>();
        }
    }

    public void Show(string type = "rewardedVideo")
    {
        Start();
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
        // случилась ошибка
    }

    void IUnityAdsListener.OnUnityAdsDidStart(string placementId)
    {
        // дополнительные действия, которые необходимо предпринять, 
        // когда конечные пользователи запускают объявление.
    }

    void IUnityAdsListener.OnUnityAdsDidFinish(string placementId, ShowResult showResult)
    {
        if (placementId == "rewardedVideo" && showResult == ShowResult.Finished)
        {
            // награда пользователя за просмотр рекламы
            PlayerPrefs.SetInt("rebornings", PlayerPrefs.GetInt("rebornings", 0) + 1);
            ms.SetAdvText();
        }
        else if (showResult == ShowResult.Skipped || showResult == ShowResult.Failed)
        {
            // не вознаграждайте пользователя за пропуск объявления.
        }
    }
}
