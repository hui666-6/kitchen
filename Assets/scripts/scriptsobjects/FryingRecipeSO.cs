using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu()]
public class FryingRecipeSO :ScriptableObject
{
    public List<FryingRecipe> list;
    public bool TryGetCuttinigRecipe(KitchenObjectSO input, out FryingRecipe fryingrecipe)
    {
        foreach (FryingRecipe recipe in list)
        {
            if (recipe.input == input)
            {
                fryingrecipe = recipe;
                return true;
            }
        }
        fryingrecipe = null;
        return false;
    }

}

[Serializable] public class FryingRecipe 
{
    public KitchenObjectSO input;
    public KitchenObjectSO output;
    public float fryingTime;
} 


