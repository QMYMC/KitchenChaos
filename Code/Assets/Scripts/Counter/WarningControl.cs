using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarningControl : MonoBehaviour
{
    private const string IS_FLICKER = "ISFlicker";
    
    [SerializeField] private GameObject warningUI;
    [SerializeField] private Animator progressBarAnimator;
    
    private bool isWarning = false;
    private float warnSoundRate = 0.2f;
    private float warnSoundTimer = 0f;

    private void Update()
    {
        if (isWarning)
        {
            warnSoundTimer += Time.deltaTime;
            if (warnSoundTimer>warnSoundRate)
            {
                warnSoundTimer = 0f;
                SoundManager.Instance.PlayWarningSound();
            }
        }
    }

    public void ShowWarning()
    {
        if (!isWarning)
        {
            isWarning = true;
            warningUI.SetActive(true);
            progressBarAnimator.SetBool(IS_FLICKER,true);
        }
    }

    public void HideWarning()
    {
        isWarning = false;
        warningUI.SetActive(false);
        progressBarAnimator.SetBool(IS_FLICKER,false);
    }
}
