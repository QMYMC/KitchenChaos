using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateKitchenObject : KitchenObject
{
    [SerializeField] private List<KitchenObjectSO> validKitchenObjectSOList;
    [SerializeField] private PlateCompleteVisual plateCompleteVisual;
    [SerializeField] private KitchenObjectGridUI kitchenObjectGridUI;
    private List<KitchenObjectSO> kitchenObjectSOList = new List<KitchenObjectSO>();

    public bool AddKitchenObjectSO(KitchenObjectSO kitchenObjectSO)
    {
        if (kitchenObjectSOList.Contains(kitchenObjectSO))
        {
            return false;
        }

        if (!validKitchenObjectSOList.Contains(kitchenObjectSO))
        {
            //不能往盘子上添加这个
            return false;
        }

        plateCompleteVisual.ShowKitchenObject(kitchenObjectSO);
        kitchenObjectGridUI.ShowKitchenObjecUI(kitchenObjectSO);
        kitchenObjectSOList.Add(kitchenObjectSO);
        return true;
    }

    public List<KitchenObjectSO> GetKitchenObjectSOList()
    {
        return kitchenObjectSOList;
    }
}
