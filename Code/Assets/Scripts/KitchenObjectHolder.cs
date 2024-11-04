using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KitchenObjectHolder : MonoBehaviour
{
    [SerializeField] private Transform holdPoint;
    private KitchenObject kitchenObject;

    public static event EventHandler OnDrop;
    public static event EventHandler OnPickUp;

    public Transform GetHoldPoint()
    {
        return holdPoint;
    }
    
    public KitchenObject GetKitchenObject()
    {
        return kitchenObject;
    }

    public KitchenObjectSO GetKitchenObjectSO()
    {
        return kitchenObject.GetKitchenObjectSo();
    }

    public bool IsHaveKitchenObject()
    {
        return kitchenObject != null;
    }

    public void SetKitchenObject(KitchenObject kitchenObject)
    {
        if (this.kitchenObject!=kitchenObject && kitchenObject!=null && this is BaseCounter)
        {
            OnDrop?.Invoke(this,EventArgs.Empty);
        }
        else if(this.kitchenObject!=kitchenObject && kitchenObject!=null && this is Player)
        {
            OnPickUp?.Invoke(this,EventArgs.Empty);
        }
        
        this.kitchenObject = kitchenObject;
        kitchenObject.transform.localPosition = Vector3.zero;
    }
    
    public void TransferKitchenObject(KitchenObjectHolder sourceHolder, KitchenObjectHolder targetHolder)
    {
        if (sourceHolder.GetKitchenObject() == null)
        {
            Debug.LogWarning("原柜台上不存在食材，转移失败！");
            return;
        }

        if (targetHolder.GetKitchenObject() != null)
        {
            Debug.LogWarning("目标柜台上存在食材，转移失败！");
            return;
        }
        
        targetHolder.AddKitchenObject(sourceHolder.kitchenObject);
        sourceHolder.ClearKitchenObject();
    }

    public void AddKitchenObject(KitchenObject kitchenObject)
    { 
        kitchenObject.transform.SetParent(holdPoint);
        SetKitchenObject(kitchenObject);
    }

    public void ClearKitchenObject()
    {
        this.kitchenObject = null;
    }
    
    public void CreateKitchenObject(GameObject kitchenObjectPrefab)
    {
        KitchenObject kitchenObject = GameObject.Instantiate(kitchenObjectPrefab, GetHoldPoint()).GetComponent<KitchenObject>();
        SetKitchenObject(kitchenObject);
    }
    
    public void DestroyKitchenObject()
    {
         Destroy(kitchenObject.gameObject);
         ClearKitchenObject();
    }
    
    public static void ClearStaticData()
    {
        OnPickUp = null;
        OnDrop = null;
    }
}