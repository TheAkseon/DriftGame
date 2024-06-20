using System;
using UnityEngine;
using TMPro;

public class DriftScore : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _driftScoreMultiplier;
    [SerializeField] private TextMeshProUGUI _driftScoreText;
    [SerializeField] private TextMeshProUGUI _driftScoreTitle;

    [SerializeField] private float _driftTreshold = 10f;
    [SerializeField] private float _requiredSpeed = 10f;

    private RCC_CarControllerV3 _rccCarController;
    private bool _isDrifting = false;


    public float driftThreshold = 10f;
    public float[] scoreMilestones = { 500f, 1000f, 1500f, 2000f, 2500f }; // Пороги для увеличения множителя
    public float[] multipliers = { 2f, 3f, 4f, 5f, 6f }; // Множители очков
    private int currentMultiplierIndex = 0;

    private float driftScore = 0f;
    private float driftStartTime = 0f;

    private bool _isActivateMultiplier = false;

    private void Start()
    {
        _rccCarController = FindObjectOfType<RCC_CarControllerV3>();
        _driftScoreMultiplier.gameObject.SetActive(false);
        _driftScoreText.gameObject.SetActive(false);
        _driftScoreTitle.gameObject.SetActive(false);
    }

    private void Update()
    {
        float driftAngle = Vector3.Angle(_rccCarController.transform.forward, _rccCarController.Rigid.velocity);

        if (driftAngle > _driftTreshold && _rccCarController.speed > _requiredSpeed) 
        {
            if (!_isDrifting)
            {
                StartDrift();
            }
            UpdateDrift(driftAngle);
        }
        else
        {
            if (_isDrifting)
            {
                EndDrift();
            }
        }

        _driftScoreText.text = "+" + Mathf.RoundToInt(driftScore).ToString();
    }

    void StartDrift()
    {
        _isDrifting = true;
        //driftStartTime = Time.time;
        _driftScoreText.gameObject.SetActive(true);
        _driftScoreTitle.gameObject.SetActive(true);
    }

    void EndDrift()
    {
        _isDrifting = false;
        //float driftDuration = Time.time - driftStartTime;
        //driftScore += driftDuration * 10f * multipliers[currentMultiplierIndex]; // Применение множителя
        Debug.Log("Drift Score: " + driftScore);
        //CheckForMilestones();
        SaveData.Instance.Data.Coins += Convert.ToInt32(driftScore * 0.5f);

        _driftScoreMultiplier.gameObject.SetActive(false);
        _driftScoreText.gameObject.SetActive(false);
        _driftScoreTitle.gameObject.SetActive(false);

        driftScore = 0;

        SaveData.Instance.SaveYandex();
    }

    void UpdateDrift(float driftAngle)
    {
        // Дополнительная логика для обновления очков в реальном времени, если нужно
        float driftDuration = Time.time - driftStartTime;

        if (_isActivateMultiplier)
        {
            driftScore += driftAngle * Time.deltaTime * multipliers[currentMultiplierIndex]; // Применение множителя
        }
        else
        {
            driftScore += driftAngle * Time.deltaTime;
        }

        CheckForMilestones();
    }

    void CheckForMilestones()
    {
        if (currentMultiplierIndex < scoreMilestones.Length && driftScore >= scoreMilestones[currentMultiplierIndex])
        {
            _isActivateMultiplier = true;
            currentMultiplierIndex++;
            _driftScoreMultiplier.text = multipliers[currentMultiplierIndex].ToString();
            _driftScoreMultiplier.gameObject.SetActive(true);
            Debug.Log("Multiplier increased! Current multiplier: " + multipliers[currentMultiplierIndex]);
        }
    }
}
