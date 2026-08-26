using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RecipeUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI recipenametext;
    [SerializeField] private Transform kitchenobjectparent;
    [SerializeField] private Image iconuitemplate;
    private void Start()
    {
        iconuitemplate.gameObject.SetActive(false);
    }
    public void UpdateRecipeSo(RecipeSO recipeso)
    {
        recipenametext.text=recipeso.recipeName;
        foreach (KitchenObjectSO kitchenObjectSO in recipeso.kitchenObjectSOList)
        {
            Image newicon = GameObject.Instantiate(iconuitemplate);
            newicon.transform.SetParent(kitchenobjectparent);
            newicon.sprite = kitchenObjectSO.Sprite;
            newicon.gameObject.SetActive(true);
        }
    }
}
