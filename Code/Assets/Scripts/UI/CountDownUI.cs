using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CountDownUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI numberText;
    
    private void Start()
    {
        GameManager.Instance.OnStateChanged += GameManager_OnStateChanged;
    }

    private void Update()
    {
        if (GameManager.Instance.IsCountDownState())
        {
            numberText.text = Mathf.CeilToInt( GameManager.Instance.GetCountDownTimer()).ToString();
        }
    }

    private void GameManager_OnStateChanged(object sender, System.EventArgs e)
    {
        numberText.gameObject.SetActive(GameManager.Instance.IsCountDownState());
    }
}
