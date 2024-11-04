using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KitchenObjectGridUI : MonoBehaviour
{
    [SerializeField] private KitchenObjectIconUI iconUI;

    private void Start()
    {
        iconUI.Hide();
    }

    public void ShowKitchenObjecUI(KitchenObjectSO kitchenObjectSO)
    {
        KitchenObjectIconUI newIconUI = GameObject.Instantiate(iconUI,transform);
        newIconUI.Show(kitchenObjectSO.sprite);
    }
}