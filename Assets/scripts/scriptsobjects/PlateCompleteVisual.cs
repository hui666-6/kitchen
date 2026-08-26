using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateCompleteVisual : MonoBehaviour
{
    [Serializable] public class Kitchenobjestso_Model
    { 
    
        public KitchenObjectSO kitchenObjectSO;
        public GameObject Model;
    
      
    }
    [SerializeField] private List<Kitchenobjestso_Model> modemap;
    public void ShowKitchenObject(KitchenObjectSO kitchenObjectSO)
    {
        foreach (Kitchenobjestso_Model model in modemap) 
        {
            if (model.kitchenObjectSO == kitchenObjectSO)
            { 
              model.Model.SetActive(true);
              return;
            }
        }

        

        
   
    }
}
