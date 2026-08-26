using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class platekitchenobject :KitchenObject
{
    [SerializeField] private List<KitchenObjectSO> useblekitchenobject;
    [SerializeField] private PlateCompleteVisual PlateCompleteVisual;
    [SerializeField] private KitchenObjectUI KitchenObjectUI;
    private List<KitchenObjectSO> kitchenObjectSOlist = new List<KitchenObjectSO>();

    public bool AddKitchenobjectSO(KitchenObjectSO kitchenobjectSO)
    {
        if (kitchenObjectSOlist.Contains(kitchenobjectSO)) //如果已经有了
        {

            return false;
        }
        if (useblekitchenobject.Contains(kitchenobjectSO) == false)
        { 
          return false;
        }
        PlateCompleteVisual.ShowKitchenObject(kitchenobjectSO);
        KitchenObjectUI.showkitchenobjectUI(kitchenobjectSO);
        kitchenObjectSOlist.Add(kitchenobjectSO);
        return true;

    }

    public List<KitchenObjectSO> GetkitchenObjectList()
    { 
    
     return kitchenObjectSOlist;
    }

}
