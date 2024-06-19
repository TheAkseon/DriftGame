using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuView : MonoBehaviour
{
    [SerializeField] private ShopController _shopController;

    [Header("Canvas")]
    [SerializeField] private LevelCanvasView _levelCanvasView;

    [Header("Buttons")]
    [SerializeField] private Button _audioButton;
    [SerializeField] private Button _startButton;

    public Button StartButton => _startButton;

    private void Start()
    {
        _startButton.onClick.AddListener(ActivateLevelCanvas);
        _audioButton.onClick.AddListener(SetAudio);
    }

    private void ActivateLevelCanvas()
    {
        _levelCanvasView.gameObject.SetActive(true);
        SaveData.Instance.Data.AppliedCarIndex = _shopController.LoadIndex;
        SaveData.Instance.SaveYandex();
    }

    private void SetAudio()
    {

    }
}
