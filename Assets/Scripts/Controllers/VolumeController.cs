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
        // 初始化滑塊值
        bgmVolumeSlider.value = PlayerPrefs.GetFloat("BGMVolume", 0.5f);
        seVolumeSlider.value = PlayerPrefs.GetFloat("SEVolume", 1.0f);

        // 添加監聽器
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

    // 當玩家按下OK時保存設定
    public void SaveSetting()
    {
        PlayerPrefs.Save();
    }
}
