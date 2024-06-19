using UnityEngine;

public class TimeOfDayController : MonoBehaviour
{
    [SerializeField] private GameObject _directionalLightDay;
    [SerializeField] private GameObject _directionalLightNight;

    private void Start()
    {
        if(SaveData.Instance.Data.DayTime == "day")
        {
            _directionalLightDay.SetActive(true);
            _directionalLightNight.SetActive(false);
        }
        else
        {
            _directionalLightDay.SetActive(false);
            _directionalLightNight.SetActive(true);
        }
    }
}
