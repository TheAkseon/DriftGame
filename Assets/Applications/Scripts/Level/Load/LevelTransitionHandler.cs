using UnityEngine;

public class LevelTransitionHandler : MonoBehaviour
{
    [SerializeField] private LevelCanvasView _levelCanvasView;

    private int _gameLevelIndex = 2;

    private void Start()
    {
        _levelCanvasView.DayFreeRideButton.onClick.AddListener(() => LoadLevel(TimesOfDay.Day));
        _levelCanvasView.NightFreeRideButton.onClick.AddListener(() => LoadLevel(TimesOfDay.Night));
    }

    private void LoadLevel(TimesOfDay timesOfDay)
    {
        if (timesOfDay == TimesOfDay.Day)
        {
            SaveData.Instance.Data.DayTime = "day";
            
        }
        else
        {
            SaveData.Instance.Data.DayTime = "night";
        }

        SaveData.Instance.Save();
        LevelLoader.Instance.LoadLevel(_gameLevelIndex);
    }

    private enum TimesOfDay
    {
        Day,
        Night
    }
}
