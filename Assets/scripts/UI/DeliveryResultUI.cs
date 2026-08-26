using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeliveryResultUI : MonoBehaviour
{
    private const string ISSHOW = "IsShow";
    [SerializeField] private Animator successanimator;
    [SerializeField] private Animator failanimator;
    private void Start()
    {
        OrderManager.Instance.OnRecipeSuccessed += OrderManager_OnRecipeSuccessed;
        OrderManager.Instance.OnRecipeFailed += OrderManager_OnRecipeFailed;
    }

    private void OrderManager_OnRecipeFailed(object sender, System.EventArgs e)
    {
        failanimator.gameObject.SetActive(true);
        failanimator.SetTrigger(ISSHOW);
    }

    private void OrderManager_OnRecipeSuccessed(object sender, System.EventArgs e)
    {
        successanimator.gameObject.SetActive(true);
        successanimator.SetTrigger(ISSHOW);
    }
}

