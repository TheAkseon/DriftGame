using UnityEngine;
using UnityEngine.UI;

public class LevelNavigationHandler : MonoBehaviour
{
    [SerializeField] private Button _restartButton;
    [SerializeField] private Button _menuButton;

    private int _menuIndex = 1;
    private int _levelIndex = 2;

    private void Start()
    {
        _restartButton.onClick.AddListener(RestartLevel);
        _menuButton.onClick.AddListener(ExitToMenu);
    }

    private void RestartLevel()
    {
        LevelLoader.Instance.LoadLevel(_levelIndex);
    }

    private void ExitToMenu()
    {
        LevelLoader.Instance.LoadLevel(_menuIndex);
    }
}
