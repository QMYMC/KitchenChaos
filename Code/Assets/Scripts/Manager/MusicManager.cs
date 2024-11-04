using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public static MusicManager Instance { get; private set; }

    private AudioSource audioSource;
    private float originalVolume = 1.0f; // 默认值为1.0

    private void Awake()
    {
        Instance = this;
        audioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        originalVolume = audioSource.volume;
    }

    public void ChangeVolume(float value)
    {
        audioSource.volume = originalVolume * value; // 更新音量
        audioSource.mute = value <= 0; // 根据音量设置静音
    }

    public float GetVolume()
    {
        return audioSource.volume / originalVolume; // 返回比例
    }
}
