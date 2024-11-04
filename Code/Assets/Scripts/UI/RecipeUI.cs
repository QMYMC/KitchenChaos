using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RecipeUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI recipeNameText;
    [SerializeField] private Transform kitchenObjectParent;
    [SerializeField] private Image iconUITemplate;

    private void Start()
    {
        iconUITemplate.gameObject.SetActive(false);
    }

    public void UpdateUI(RecipeSO recipeSO)
    {
        recipeNameText.text = recipeSO.recipeName;
        foreach (KitchenObjectSO item in recipeSO.kitchenObjectSOList)
        {
            Image icon = GameObject.Instantiate(iconUITemplate, kitchenObjectParent);
            icon.sprite = item.sprite;
            icon.gameObject.SetActive(true);
        }
    }

}
