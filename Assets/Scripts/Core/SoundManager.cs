using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;

    public AudioClip buttonClickSound;
    public AudioClip ballUpgradeSound;
    private AudioSource audioSource;

    void Awake()
    {
        // 單例模式
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            audioSource = GetComponent<AudioSource>();

            // 確保遊戲物件有 AudioSource 組件 (沒有的話就添加一個)
            if (audioSource == null)
            {
                audioSource = gameObject.AddComponent<AudioSource>();
            }
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    // 播放按鈕SE方法
    public void PlayButtonClickSound()
    {
        audioSource.PlayOneShot(buttonClickSound);
    }

    // 播放球升級SE方法
    public void PlayBallUpgradeSound()
    {
        audioSource.PlayOneShot(ballUpgradeSound);
    }
}
