using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class OrderListUI : MonoBehaviour
{
    [SerializeField] private Transform RecipeParent;
    [SerializeField] private RecipeUI RecipeUITemplate;
    private void Start()
    {
        RecipeUITemplate.gameObject.SetActive(false);
        OrderManager.Instance.OnRecipeSpawned += Instance_OnRecipeSpawned;
        OrderManager.Instance.OnRecipeSuccessed += Instance_OnRecipeSuccessed;
    }

    private void Instance_OnRecipeSuccessed(object sender, System.EventArgs e)
    {
        updateUI();
    }

    private void Instance_OnRecipeSpawned(object sender, System.EventArgs e)
    {
        updateUI();
    }
    private void updateUI()
    {
        foreach (Transform child in RecipeParent)
        {
            if (child != RecipeUITemplate.transform)
            {
                Destroy(child.gameObject);
            }
        }
        List<RecipeSO> recipesolist = OrderManager.Instance.GetOrderList();
        foreach (RecipeSO recipeso in recipesolist)
        {
            RecipeUI recipeUI = GameObject.Instantiate(RecipeUITemplate);
            recipeUI.transform.SetParent(RecipeParent);
            recipeUI.gameObject.SetActive(true);
            recipeUI.UpdateRecipeSo(recipeso);
        }

    }


}
