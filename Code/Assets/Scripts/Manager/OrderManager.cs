using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class OrderManager : MonoBehaviour
{
     [SerializeField] private RecipeListSO recipeSOList;
     [SerializeField] private float orderRate = 2f;
     [SerializeField] private int orderMaxCount = 5;

     private List<RecipeSO> orderRecipeSOList = new List<RecipeSO>();
     private float orderTime = 0f;
     private bool isStartOrder = false;
     private int orderCount = 0;
     private int successDeliveryCount = 0;

     public event EventHandler OnRecipeSpawned;
     public event EventHandler OnRecipeSucceed;
     public event EventHandler OnRecipeFailed;
     
     public static OrderManager Instance
     {
          get;
          private set;
     }

     private void Awake()
     {
          Instance = this;
     }

     private void Start()
     {
          GameManager.Instance.OnStateChanged += GameManager_OnStateChanged;
     }

     private void Update()
     {
          if (isStartOrder)
          {
               OrderUpdate();
          }
     }

     private void GameManager_OnStateChanged(object sender,EventArgs e)
     {
          if (GameManager.Instance.IsGamePlayingState())
          {
               StartSpawnOrder();
          }
     }

     private void OrderUpdate()
     {
          orderTime += Time.deltaTime;
          if (orderTime >= orderRate)
          {
               orderTime = 0;
               OrderANewRecipe();
          }
     }

     private void OrderANewRecipe()
     {
          if (orderCount >= orderMaxCount) return;
          
          orderCount++;
          int index = Random.Range(0, recipeSOList.recipeSOList.Count);
          orderRecipeSOList.Add(recipeSOList.recipeSOList[index]);
          OnRecipeSpawned?.Invoke(this, EventArgs.Empty);
     }

     public void DeliveryRecipe(PlateKitchenObject plateKitchenObject)
     {
          RecipeSO correctRecipe = null;
          foreach (RecipeSO item in orderRecipeSOList)
          {
               if (IsCorrect(item, plateKitchenObject))
               {
                    correctRecipe = item;
                    break;
               }
          }

          if (correctRecipe==null)
          {
               Debug.Log("上菜失败！");
               OnRecipeFailed?.Invoke(this,EventArgs.Empty);
          }
          else
          {
               orderRecipeSOList.Remove(correctRecipe);
               OnRecipeSucceed?.Invoke(this,EventArgs.Empty);
               successDeliveryCount++;
               Debug.Log("上菜成功");
          }
     }

     private bool IsCorrect(RecipeSO recipe, PlateKitchenObject plateKitchenObject)
     {
          List<KitchenObjectSO> list1 = recipe.kitchenObjectSOList;
          List<KitchenObjectSO> list2 = plateKitchenObject.GetKitchenObjectSOList();
          
          if(list1.Count!=list2.Count) return false;

          foreach (KitchenObjectSO kitchenObjectSO in list1)
          {
               if (list2.Contains(kitchenObjectSO)==false)
               {
                    return false;
               }
          }

          return true;
     }

     public List<RecipeSO> GetOrderList()
     {
          return orderRecipeSOList;
     }

     private void StartSpawnOrder()
     {
          isStartOrder = true;
     }

     public int GetSuccessDeliveryCount()
     {
          return successDeliveryCount; 
     }
}