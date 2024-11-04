using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class StoveCounter : BaseCounter
{
    [SerializeField] private FryingRacipeListSO fryingRecipeList;
    [SerializeField] private FryingRacipeListSO burningRecipeList;
    [SerializeField] private StoveCounterVisual stoveCounterVisual;
    [SerializeField] private ProgressBarUI progressBarUI;
    private FryingRecipe fryingRecipe;
    private float fryingTimer = 0f;
    private StoveState state = StoveState.Idle;
    [SerializeField] private AudioSource sound;
    private WarningControl warningControl;

    private enum StoveState
    {
        Idle,
        Frying,
        Burning
    }

    public override void InterAction(Player player)
    {
        if (player.IsHaveKitchenObject())
        {
            //手上有食材
            if (IsHaveKitchenObject() == false)
            {
                //当前柜台为空
                if (fryingRecipeList.TryGetFryingRecipe(player.GetKitchenObject().GetKitchenObjectSo(), out FryingRecipe fryingRecipe))
                {
                    TransferKitchenObject(player, this);
                    StartFrying(fryingRecipe);
                }
                else if (burningRecipeList.TryGetFryingRecipe(player.GetKitchenObject().GetKitchenObjectSo(),out FryingRecipe burningRecipe))
                {
                    TransferKitchenObject(player, this);
                    StartBurning(burningRecipe);
                }
                else
                {
                    
                }
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
                StartToIdle();
                TransferKitchenObject(this, player);
            }
        }
    }

    private void Start()
    {
        warningControl = GetComponent<WarningControl>();
    }

    private void Update()
    {
        switch (state)
        {
            case StoveState.Idle:
                break;
            case StoveState.Frying:
                fryingTimer += Time.deltaTime;
                progressBarUI.UpdateProgress(fryingTimer/fryingRecipe.fryingTime);
                if (fryingTimer >= fryingRecipe.fryingTime)
                {
                    DestroyKitchenObject();
                    CreateKitchenObject(fryingRecipe.output.prefab);
                    burningRecipeList.TryGetFryingRecipe(GetKitchenObject().GetKitchenObjectSo(), out FryingRecipe newFryingRecipe);
                    StartBurning(newFryingRecipe);
                }
                break;
            case StoveState.Burning:
                fryingTimer += Time.deltaTime;
                progressBarUI.UpdateProgress(fryingTimer/fryingRecipe.fryingTime);
                
                float warningTimeNormalized = 0.5f;
                if (fryingTimer/fryingRecipe.fryingTime>warningTimeNormalized)
                {
                    warningControl.ShowWarning();
                }
                
                if (fryingTimer >= fryingRecipe.fryingTime)
                {
                    DestroyKitchenObject();
                    CreateKitchenObject(fryingRecipe.output.prefab);
                    StartToIdle();
                }
                break;
        }
    }

    private void StartFrying(FryingRecipe fryingRecipe)
    {
        fryingTimer = 0;
        this.fryingRecipe = fryingRecipe;
        state = StoveState.Frying;
        stoveCounterVisual.ShowStoveEffect();
        
        sound.Play();
    }

    private void StartBurning(FryingRecipe fryingRecipe)
    {
        if (fryingRecipe==null)
        {
            Debug.LogWarning("无法获取Burning食谱，无法进行Burning");
            state = StoveState.Idle;
        }
        fryingTimer = 0f;
        this.fryingRecipe = fryingRecipe;
        state = StoveState.Burning;
        stoveCounterVisual.ShowStoveEffect();
        
        sound.Play();
    }

    private void StartToIdle()
    {
        state = StoveState.Idle;
        stoveCounterVisual.HideStoveEffect();
        progressBarUI.Hide();
        
        sound.Pause();
        
        warningControl.HideWarning();
    }
}