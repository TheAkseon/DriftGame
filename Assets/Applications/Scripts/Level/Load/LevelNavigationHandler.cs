using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelNavigationHandler : MonoBehaviour
{
    [SerializeField] private Button _restartButton;

    private int _levelIndex = 2;

    private void Start()
    {
        _restartButton.onClick.AddListener(RestartLevel);
    }

    private void RestartLevel()
    {
        LevelLoader.Instance.LoadLevel(_levelIndex);
    }
}
