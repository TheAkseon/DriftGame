using UnityEngine;

public class LevelBehaviour : MonoBehaviour
{
    private int _currentLevel = 1;
    private int _nextLevel;

    public void NextLevel()
    {
        _nextLevel = _currentLevel + 1;

        LevelLoader.Instance.UnloadScene();
        LevelLoader.Instance.LoadLevel(_nextLevel);
        _currentLevel = _nextLevel;
    }

    public void PreviousLevel()
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