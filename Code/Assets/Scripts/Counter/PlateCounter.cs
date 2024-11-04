using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateCounter : BaseCounter
{
    [SerializeField] private float spawRate = 3f;
    [SerializeField] private KitchenObjectSO plateSO;
    [SerializeField] private int plateCoutMax = 5;
    private List<KitchenObject> plateList = new List<KitchenObject>();
    private float timer = 0f;

    private void Update()
    {
        if (plateList.Count < plateCoutMax )
        {
            timer += Time.deltaTime;
        }
        
        if (timer > spawRate)
        {
            timer = 0;
            SpawnPlate();
        }
    }

    public override void InterAction(Player player)
    {

        if (!player.IsHaveKitchenObject())
        {
            if (plateList.Count > 0)
            {
                player.AddKitchenObject(plateList[^1]);
                plateList.RemoveAt(plateList.Count - 1);
            }
        }
    }

    private void SpawnPlate()
    {
        KitchenObject kitchenObject = GameObject.Instantiate(plateSO.prefab, GetHoldPoint()).GetComponent<KitchenObject>();
        kitchenObject.transform.localPosition = Vector3.zero + Vector3.up * 0.1f * plateList.Count;

        plateList.Add(kitchenObject);
    }
}
