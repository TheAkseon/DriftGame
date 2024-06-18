using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.SceneManagement;
using YG;

public class YandexSDK : MonoBehaviour
{
    [SerializeField] private Localization _localization;

    private int _mainMenuIndex = 1;
    private LevelLoader _levelLoader;
    private const string _saveKey = "SaveData";
    private string _language;

    public bool IsAdRunning;

    public static YandexSDK Instance;

    public string CurrentLanguage => _language;

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

    private void Start()
    {
        if (YandexGame.SDKEnabled)
        {
            GetLoad();
            _levelLoader = FindObjectOfType<LevelLoader>();
            _language = YandexGame.EnvironmentData.language;
            _localization.SetLanguage(_language);
            
            LevelLoader.Instance.LoadLevel(_mainMenuIndex, GameReady);
        }
    }

    private void OnEnable()
    {
        YandexGame.GetDataEvent += GetLoad;
    }

    private void OnDisable()
    {
        YandexGame.GetDataEvent -= GetLoad;
    }

    private void OnInBackgroundChange(bool inBackground)
    {
        if (!IsAdRunning)
            MuteAudio(inBackground);
    }

    private void MuteAudio(bool value)
    {
        Time.timeScale = value ? 0f : 1f;
        AudioListener.pause = value;
        AudioListener.volume = value ? 0f : 1f;
        SoundsManager.Instance.Mute("music", value);
    }

    private void GetLoad()
    {
        SaveData.Instance.Data.Coins = YandexGame.savesData.Coins;
        SaveData.Instance.Data.RecordDriftScore = YandexGame.savesData.RecordDriftScore;
        SaveData.Instance.Data.muteMusic = YandexGame.savesData.muteMusic;
        SaveData.Instance.Data.AppliedCarIndex = YandexGame.savesData.AppliedCarIndex;
        SaveData.Instance.Data.IsBuyShop = YandexGame.savesData.IsBuyShop;

        SaveManager.Save(_saveKey, SaveData.Instance.Data);
    }

    private void GameReady()
    {
        YandexGame.GameReadyAPI();
    }
}