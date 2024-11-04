using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContainCounterVisual : MonoBehaviour
{
    private Animator anim;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    public void PlayOpen()
    {
        anim.SetTrigger("OpenClose");
    }
}
