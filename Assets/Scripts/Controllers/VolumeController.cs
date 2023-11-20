using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumeController : MonoBehaviour
{
    public Slider bgmVolumeSlider;
    public Slider seVolumeSlider;

    void Start()
    {
        // ��l�Ʒƶ���
        bgmVolumeSlider.value = PlayerPrefs.GetFloat("BGMVolume", 0.5f);
        seVolumeSlider.value = PlayerPrefs.GetFloat("SEVolume", 1.0f);

        // �K�[��ť��
        bgmVolumeSlider.onValueChanged.AddListener(HandleBGMVolumeChange);
        seVolumeSlider.onValueChanged.AddListener(HandleSEVolumeChange);
    }

    void HandleBGMVolumeChange(float volume)
    {
        BGMPlayer.Instance.GetComponent<AudioSource>().volume = volume;
        PlayerPrefs.SetFloat("BGMVolume", volume);
    }

    void HandleSEVolumeChange(float volume)
    {
        PlayerPrefs.SetFloat("SEVolume", volume);
    }

    // ���a���UOK�ɫO�s�]�w
    public void SaveSetting()
    {
        PlayerPrefs.Save();
    }
}
