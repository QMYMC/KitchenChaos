using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSound : MonoBehaviour
{
    private Player player;
    private float stepSoundRate = 0.12f;
    private float stepSoundTimer = 0;

    private void Start()
    {
        player = GetComponent<Player>();
    }

    private void Update()
    {
        stepSoundTimer += Time.deltaTime;
        if (stepSoundTimer>stepSoundRate)
        {
            if (player.IsWalking)
            {
                stepSoundTimer = 0;
                SoundManager.Instance.PlayStepSound(1.0f );
            }
        }
    }
}
