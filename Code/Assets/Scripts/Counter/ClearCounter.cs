using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearCounter : BaseCounter
{
    public override void InterAction(Player player)
    {
        if (player.IsHaveKitchenObject())
        {//手上有KitchenObject 
            if (player.GetKitchenObject().TryGetComponent<PlateKitchenObject>(out PlateKitchenObject plateKitchenObject))
            {//手上有盘子
                if (!IsHaveKitchenObject())
                {//当前柜台为空
                    TransferKitchenObject(player,this);
                }
                else
                {//当前柜台不为空
                    if (plateKitchenObject.AddKitchenObjectSO(GetKitchenObjectSO()))
                    {
                        DestroyKitchenObject();
                    }
                }
            }
            else
            { //手上是普通食材食材
                if (IsHaveKitchenObject()==false)
                { //当前柜台为空
                    TransferKitchenObject(player,this);
                }
                else
                { //当前柜台不为空
                    if (GetKitchenObject().TryGetComponent<PlateKitchenObject>(out plateKitchenObject))
                    {
                        if (plateKitchenObject.AddKitchenObjectSO(player.GetKitchenObjectSO()))
                        {
                            player.DestroyKitchenObject();
                        }
                    }
                }
            }
        }
        else
        {
            //手上没食材
            if (IsHaveKitchenObject()==false)
            {
                //当前柜台为空
            }
            else
            {
                //当前柜台不为空
                TransferKitchenObject(this,player);
            }
        }
    }
}