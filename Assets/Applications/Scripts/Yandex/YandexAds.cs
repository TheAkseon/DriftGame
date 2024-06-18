using System;
using UnityEngine;
using YG;

public class YandexAds : MonoBehaviour
{
    public static YandexAds Instance;

    private bool _isRewarded = false;
    public bool IsRewarded => _isRewarded;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void OnEnable()
    {
        YandexGame.RewardVideoEvent += Rewarded;
    }

    private void OnDisable()
    {
        YandexGame.RewardVideoEvent -= Rewarded;
    }

    /*public void ShowInterstitial()
    {
#if UNITY_WEBGL && !UNITY_EDITOR
        InterstitialAd.Show(OnAdOpen, OnIterstitialAddClose);
#endif
    }*/
    private void Rewarded(int id)
    {
        if (id == 1)
        {
            
        }
        else if (id == 2)
        {

        }
    }

    public void ShowRewardAd(int id)
    {
        YandexGame.RewVideoShow(id);
    }

    public void OnAdOpen()
    {
        Time.timeScale = 0;
        AudioListener.volume = 0;
        YandexSDK.Instance.IsAdRunning = true;
    }

    public void OnAdClose()
    {
        YandexSDK.Instance.IsAdRunning = false;
        Time.timeScale = 1;
        AudioListener.volume = 1;
        OnAdRewarded();
    }

    public void OnInterstialAdClose()
    {
        YandexSDK.Instance.IsAdRunning = false;
        Time.timeScale = 1;
        AudioListener.volume = 1;
    }

    public void OnAdRewarded()
    {
        _isRewarded = true;
    }

    public void OnAdRewardedFalse()
    {
        _isRewarded = false;
    }

    /*public void OnIterstitialAddClose(bool value)
    {
        Time.timeScale = 1;
        AudioListener.volume = 1;
    }*/
}
