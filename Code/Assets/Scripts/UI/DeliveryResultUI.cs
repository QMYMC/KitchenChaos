using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeliveryResultUI : MonoBehaviour
{
    private const string IS_SHOW = "IsShow";
    
    [SerializeField] private Animator deliverySuccessAnimator;
    [SerializeField] private Animator deliveryFailAnimator;
    // Start is called before the first frame update
    void Start()
    {
        OrderManager.Instance.OnRecipeSucceed += OrderManager_OnRecipeSucceed;
        OrderManager.Instance.OnRecipeFailed += OrderManager_OnRecipeFailed;
    }

    private void OrderManager_OnRecipeSucceed(object sender,EventArgs e)
    {
        deliverySuccessAnimator.gameObject.SetActive(true);
        deliverySuccessAnimator.SetTrigger(IS_SHOW);
    }
    
    private void OrderManager_OnRecipeFailed(object sender,EventArgs e)
    {
        deliveryFailAnimator.gameObject.SetActive(true);
        deliveryFailAnimator.SetTrigger(IS_SHOW);
    }
}
