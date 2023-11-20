using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BGMPlayer : MonoBehaviour
{
    public static BGMPlayer Instance;
    public AudioClip bgm;
    public AudioSource audioSource;
    
    void Awake()
    {
        // ³æ¨Ò¼Ò¦¡
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    void Start()
    {
        PlaySound(bgm, PlayerPrefs.GetFloat("BGMVolume", 0.5f), true);
    }

    public void PlaySound(AudioClip clip, float volume, bool loop)
    {
        audioSource.clip = clip;
        audioSource.volume = volume;
        audioSource.loop = loop;
        audioSource.Play();
    }
}
