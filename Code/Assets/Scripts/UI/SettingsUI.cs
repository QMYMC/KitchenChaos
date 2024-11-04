using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SettingsUI : MonoBehaviour
{
    public static SettingsUI Instance { get; private set; }
    
    [SerializeField] private GameObject uiParent;
    [SerializeField] private Slider soundSlider;
    [SerializeField] private TextMeshProUGUI soundNumber;
    [SerializeField] private Slider musicSlider;
    [SerializeField] private TextMeshProUGUI musicNumber;
    [SerializeField] private Button closeButton;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        Hide();
        soundSlider.onValueChanged.AddListener(OnSoundSliderValueChanged);
        musicSlider.onValueChanged.AddListener(OnMusicSliderValueChanged);
        closeButton.onClick.AddListener((Hide));

        soundSlider.value = SoundManager.Instance.GetVolume();
        musicSlider.value = MusicManager.Instance.GetVolume();
        
        // 初始化显示文本
        UpdateSoundText(soundSlider.value);
        UpdateMusicText(musicSlider.value);
    }

    private void OnSoundSliderValueChanged(float value)
    {
        SoundManager.Instance.ChangeVolume(value);
        UpdateSoundText(value);
    }

    private void OnMusicSliderValueChanged(float value)
    {
        MusicManager.Instance.ChangeVolume(value);
        UpdateMusicText(value);
    }

    private void UpdateSoundText(float value)
    {
        soundNumber.text = (value * 100).ToString("F0"); // 显示乘以100后的整数
    }

    private void UpdateMusicText(float value)
    {
        musicNumber.text = (value * 100).ToString("F0"); // 显示乘以100后的整数
    }

    public void Show()
    {
        uiParent.SetActive(true);
    }

    private void Hide()
    {
        uiParent.SetActive(false);
    }
}