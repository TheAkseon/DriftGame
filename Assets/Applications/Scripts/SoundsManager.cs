using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SoundsManager : MonoBehaviour
{
    public static SoundsManager Instance = null;

    private float _startVolume;

    private float _fadeDuration = 0.5f;
    private string _nameSoundLevel = "1";

    public string NameSoundLevel => _nameSoundLevel;

    //private int currentClip = 0;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            //DontDestroyOnLoad(gameObject);
        }
        //else
        //Destroy(gameObject);
    }

    private void Start()
    {
    }


    public void Mute(string source, bool value)
    {
    }

    public void FadeOut()
    {
        
    }

}

[Serializable]
public class Music
{
    public int levelNumber;
    public AudioClip audio;
}
