using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DriftScore : MonoBehaviour
{
    [SerializeField] private float _driftTreshold = 10f;
    [SerializeField] private float _requiredSpeed = 10f;

    private RCC_CarControllerV3 _rccCarController;
    private bool _isDrifting = false;


    public TextMeshProUGUI driftScoreText;
    public float driftThreshold = 10f;
    public float[] scoreMilestones = { 1000f, 5000f, 10000f }; // Пороги для увеличения множителя
    public float[] multipliers = { 1f, 2f, 3f, 4f }; // Множители очков
    private int currentMultiplierIndex = 0;

    private float driftScore = 0f;
    private float driftStartTime = 0f;

    private void Start()
    {
        _rccCarController = FindObjectOfType<RCC_CarControllerV3>();
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

        driftScoreText.text = "+" + Mathf.RoundToInt(driftScore).ToString();
    }

    void StartDrift()
    {
        _isDrifting = true;
        driftStartTime = Time.time;
    }

    void EndDrift()
    {
        _isDrifting = false;
        float driftDuration = Time.time - driftStartTime;
        driftScore += driftDuration * 10f * multipliers[currentMultiplierIndex]; // Применение множителя
        Debug.Log("Drift Score: " + driftScore);
        CheckForMilestones();
        SaveData.Instance.Data.Coins += Convert.ToInt32(driftScore / 0.7f);
        SaveData.Instance.SaveYandex();
    }

    void UpdateDrift(float driftAngle)
    {
        // Дополнительная логика для обновления очков в реальном времени, если нужно
        float driftDuration = Time.time - driftStartTime;
        driftScore += driftAngle * Time.deltaTime * multipliers[currentMultiplierIndex]; // Применение множителя
    }

    void CheckForMilestones()
    {
        if (currentMultiplierIndex < scoreMilestones.Length && driftScore >= scoreMilestones[currentMultiplierIndex])
        {
            currentMultiplierIndex++;
            Debug.Log("Multiplier increased! Current multiplier: " + multipliers[currentMultiplierIndex]);
        }
    }
}
