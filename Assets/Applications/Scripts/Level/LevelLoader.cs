using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceProviders;

public class LevelLoader : MonoBehaviour
{
    private SceneInstance _currentSceneInstance;
    
    public static LevelLoader Instance;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void LoadLevel(int sceneIndex, Action loaded = null)
    {
        string sceneName = sceneIndex.ToString();

        AsyncOperationHandle<SceneInstance> operation = Addressables.LoadSceneAsync(sceneName, LoadSceneMode.Single);

        operation.Completed += handle =>
        {
            if (handle.Status == AsyncOperationStatus.Succeeded)
            {
                _currentSceneInstance = handle.Result;
                
                Debug.Log("—цена загружена!");
            }
            else 
            {
                Debug.LogError("ќшибка при загрузке сцены!");
            }
        }; 
    } 

    public void UnloadScene()
    {
        Addressables.UnloadSceneAsync(_currentSceneInstance);
    }
}
