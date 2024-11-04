using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance { get; private set; }
    
    [SerializeField] private AudioClipRefsSO audioClipRefsSO;

    private float volume = 1.0f; // 默认值为1.0

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        // 其他初始化逻辑...
        OrderManager.Instance.OnRecipeFailed += OrderManager_OnRecipeFailed;
        OrderManager.Instance.OnRecipeSucceed += OrderManager_OnRecipeSucceed;
        CuttingCounter.OnCut += CuttingCounter_OnCut;
        KitchenObjectHolder.OnPickUp += KitchenObject_OnPickUP;
        KitchenObjectHolder.OnDrop += KitchenObject_OnPickDrop;
        TrashCounter.OnObjectTrash += TrashCounter_OnObjectTrash;
    }

    // 播放声音，使用指定位置和音量
    private void PlaySound(AudioClip[] clips, Vector3 position, float volumeMultiple = 1.0f)
    {
        if (clips.Length == 0) return; // 确保clips不为空
        int index = Random.Range(0, clips.Length);
        AudioSource.PlayClipAtPoint(clips[index], position, volumeMultiple * volume);
    }

    // 播放特定的音效
    public void PlayStepSound(float volumeMultiple)
    {
        PlaySound(audioClipRefsSO.footStep, Camera.main.transform.position, volumeMultiple);
    }
    
    // 播放警告声音
    public void PlayWarningSound()
    {
        PlaySound(audioClipRefsSO.warning, Camera.main.transform.position);
    }

    // 更改音量
    public void ChangeVolume(float value)
    {
        volume = value; // 更新音量
    }

    // 获取当前音量
    public float GetVolume()
    {
        return volume;
    }

    // 处理成功的配方
    private void OrderManager_OnRecipeSucceed(object sender, System.EventArgs e)
    {
        PlaySound(audioClipRefsSO.deliverySuccess, Camera.main.transform.position);
    }

    // 处理失败的配方
    private void OrderManager_OnRecipeFailed(object sender, System.EventArgs e)
    {
        PlaySound(audioClipRefsSO.deliveryFail, Camera.main.transform.position);
    }

    // 处理切割音效
    private void CuttingCounter_OnCut(object sender, System.EventArgs e)
    {
        PlaySound(audioClipRefsSO.chop, Camera.main.transform.position);
    }

    // 处理物体拾取音效
    private void KitchenObject_OnPickUP(object sender, System.EventArgs e)
    {
        PlaySound(audioClipRefsSO.objectPickUp, Camera.main.transform.position);
    }

    // 处理物体放置音效
    private void KitchenObject_OnPickDrop(object sender, System.EventArgs e)
    {
        PlaySound(audioClipRefsSO.objectDrop, Camera.main.transform.position);
    }

    // 处理物体垃圾处理音效
    private void TrashCounter_OnObjectTrash(object sender, System.EventArgs e)
    {
        PlaySound(audioClipRefsSO.trash, Camera.main.transform.position);
    }
}
