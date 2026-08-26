using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class KitchenObjectHolder : MonoBehaviour
{
    [SerializeField] private Transform Holdpoint;//外界赋值
    private KitchenObject kitchenObject;
    public static event EventHandler ondrop;
    public static event EventHandler onpickup;

    public bool IsHavekitchenobject() 
    {
        return kitchenObject != null;
     }
    public KitchenObject GetKitchenObject()
    {
        return kitchenObject;

    }
    public KitchenObjectSO GetKitchenObjectSO()
    { 
      return kitchenObject.GetKitchenObjectSO();
    }
    public void SetKitchenobject(KitchenObject kitchenObject)
    {
        if (this.kitchenObject != kitchenObject && kitchenObject != null && this is BaseCounter)
        {
            ondrop?.Invoke(this, EventArgs.Empty);
        }
        else if (this.kitchenObject != kitchenObject && kitchenObject != null && this is player)
        { 
          onpickup?.Invoke(this, EventArgs.Empty);
        }
            this.kitchenObject = kitchenObject;
            kitchenObject.transform.localPosition = Vector3.zero;

    }
    public Transform GetHoldpoint()
    {
        return Holdpoint;
    
    }
    public void TransferKitchenObject(KitchenObjectHolder sourceHolder, KitchenObjectHolder targetHolder)
    {
        if (sourceHolder.GetKitchenObject() == null)
        {
            Debug.LogWarning("源柜台上不存在食材，转移失败。");
            return;
        }
        if (targetHolder.GetKitchenObject() != null)
        {

            Debug.LogWarning("目标柜台上存在食材，转移失败。");
            return;
        }

        targetHolder.AddKitchenObject(sourceHolder.GetKitchenObject());
        sourceHolder.ClearKitchenObject();
    }

    public void AddKitchenObject(KitchenObject kitchenObject)
    {
        kitchenObject.transform.SetParent(Holdpoint);
        SetKitchenobject(kitchenObject);
    }
    public void ClearKitchenObject()
    {
        this.kitchenObject = null;
    }
    public void Destorykitchenobject()
    {
        Destroy(kitchenObject.gameObject);
        ClearKitchenObject ();
    }
    public void creatkitchenobject(GameObject kitchenobjectprefab)
    {
        KitchenObject kitchenObject = GameObject.Instantiate(kitchenobjectprefab, GetHoldpoint()).GetComponent<KitchenObject>();
        SetKitchenobject(kitchenObject);

    }
    public static void ClearStaticData()
    { 
        ondrop=null;
        onpickup=null;
    
    }
}

