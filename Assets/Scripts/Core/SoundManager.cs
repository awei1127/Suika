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
        // ��ҼҦ�
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            audioSource = GetComponent<AudioSource>();

            // �T�O�C������ AudioSource �ե� (�S�����ܴN�K�[�@��)
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

    // ������sSE��k
    public void PlayButtonClickSound()
    {
        audioSource.PlayOneShot(buttonClickSound);
    }

    // ����y�ɯ�SE��k
    public void PlayBallUpgradeSound()
    {
        audioSource.PlayOneShot(ballUpgradeSound);
    }
}
