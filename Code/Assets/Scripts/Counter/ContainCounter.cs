using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContainCounter : BaseCounter
{
    [SerializeField] private KitchenObjectSO kitchenObjectSO;
    [SerializeField] private ContainCounterVisual containCounterVisual;

    public override void InterAction(Player player)
    {
        if (player.IsHaveKitchenObject()) return;

        CreateKitchenObject(kitchenObjectSO.prefab);
        TransferKitchenObject(this, player);
        
        containCounterVisual.PlayOpen();
    }
}
