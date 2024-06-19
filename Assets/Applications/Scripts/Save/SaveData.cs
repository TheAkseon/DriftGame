using System;
using System.Collections.Generic;
using UnityEngine;
using YG;

public class SaveData : MonoBehaviour
{
    public static SaveData Instance;

    [SerializeField] public DataHolder _data;

    public DataHolder Data => _data;

    private const string _leaderboardTxt = "Leaderboard";
    private const string _saveKey = "SaveData";

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

    private void Update()
    {
        if (Input.GetKey(KeyCode.R))
        {
            _data = new DataHolder();

            SaveManager.Reset(_saveKey, _data);
            SaveYandex();
        }
    }

    private void OnDisable()
    {
        SaveYandex();
        Save();
    }

    public void Save()
    {
        SaveManager.Save(_saveKey, _data);
    }

    public void Load()
    {
        var data = SaveManager.Load<DataHolder>(_saveKey);
        _data = data;
    }

    public void SaveYandex()
    {
        YandexGame.savesData.Coins = Data.Coins;
        YandexGame.savesData.RecordDriftScore = Data.RecordDriftScore;
        YandexGame.savesData.muteMusic = Data.muteMusic;
        YandexGame.savesData.AppliedCarIndex = Data.AppliedCarIndex;
        YandexGame.savesData.IsBuyShop = Data.IsBuyShop;


        YandexGame.SaveProgress();
    }
    public void SaveLeaderBoard()
    {
        YandexGame.NewLeaderboardScores("LeaderBoard", Data.RecordDriftScore);
    }
}

[Serializable]
public class DataHolder
{
    public string DayTime;

    public int Coins = 0;
    public int RecordDriftScore = 0;
    public bool muteMusic = false;
    public int AppliedCarIndex = 0;
    public List<bool> IsBuyShop = new List<bool>() {true, false, false, false, false, false, false, false, false, false,
    false, false, false, false, false, false, false};
}
