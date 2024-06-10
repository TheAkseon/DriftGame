using UnityEngine;

public class LevelBehaviour : MonoBehaviour
{
    [SerializeField] private GameObject _driftLevelsUI;
    [SerializeField] private GameObject _freeRideLevelsUI;
    [SerializeField] private GameObject _gameModesUI;

    private int _currentLevel = 1;
    private int _nextLevel;

    public void LoadNextScene()
    {
        _nextLevel = _currentLevel + 1;

        LevelLoader.Instance.UnloadScene();
        LevelLoader.Instance.LoadLevel(_nextLevel);
        _currentLevel = _nextLevel;
    }

    public void LoadFirstScene()
    {
        LevelLoader.Instance.UnloadScene();
        LevelLoader.Instance.LoadLevel(1);
    }

    public void LoadLevelsUI(bool isDrift)
    {
        if (isDrift)
        {
            _gameModesUI.SetActive(false);
            _driftLevelsUI.SetActive(true);
        }
        else
        {
            _gameModesUI.SetActive(false);
            _freeRideLevelsUI.SetActive(true);
        }
    }

    public void LoadGameModesUI(bool isDrift)
    {
        if (isDrift)
        {
            _driftLevelsUI.SetActive(false);
            _gameModesUI.SetActive(true);
        }
        else
        {
            _freeRideLevelsUI.SetActive(false);
            _gameModesUI.SetActive(true);
        }
    }

    public void LoadPreviousLevel()
    {
        _nextLevel = _currentLevel - 1;
        LevelLoader.Instance.UnloadScene();
        LevelLoader.Instance.LoadLevel(_nextLevel);
    }

    public void Restart()
    {
        LevelLoader.Instance.LoadLevel(_currentLevel);
    }
}