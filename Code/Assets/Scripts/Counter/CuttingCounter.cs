using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CuttingCounter : BaseCounter
{
    [SerializeField] private CuttingRecipeListSO cuttingRecipeList;
    [SerializeField] private ProgressBarUI progressBarUI;
    [SerializeField] private CuttingCounterVisual cuttingCounterVisual;
    private int cuttingCount = 0;

    public static event EventHandler OnCut;

    public override void InterAction(Player player)
    {
        if (player.IsHaveKitchenObject())
        {
            //手上有食材
            if (IsHaveKitchenObject() == false)
            {
                //当前柜台为空
                cuttingCount = 0;
                TransferKitchenObject(player, this);
            }
            else
            {
                //当前柜台不为空
            }
        }
        else
        {
            //手上没食材
            if (IsHaveKitchenObject() == false)
            {
                //当前柜台为空
            }
            else
            {
                //当前柜台不为空
                progressBarUI.Hide();
                TransferKitchenObject(this, player);
            }
        }
    }

    public override void InterActionOperate(Player player)
    {
        if (IsHaveKitchenObject())
        {
            if (cuttingRecipeList.TryGetCuttingRecipe(GetKitchenObject().GetKitchenObjectSo(), out CuttingRecipe cuttingRecipe))
            {
                Cut();
                progressBarUI.UpdateProgress((float)cuttingCount / cuttingRecipe.cuttingCountMax);
                if (cuttingCount == cuttingRecipe.cuttingCountMax)
                {
                    DestroyKitchenObject();
                    CreateKitchenObject(cuttingRecipe.output.prefab);
                }
            }
        }
    }

    private void Cut()
    {
        OnCut?.Invoke(this,EventArgs.Empty);
        cuttingCount++;
        cuttingCounterVisual.PlayCut();
    }

    public static void ClearStaticData()
    {
        OnCut = null;
    }
}