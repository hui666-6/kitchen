using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseCounter :KitchenObjectHolder
{
    [SerializeField] private GameObject selectedcounter;

    // 被玩家选中/取消选中时触发，供各类柜台的视觉表现（如按键提示）订阅
    public event EventHandler OnSelected;
    public event EventHandler OnDeselected;

    public virtual void Interact(player player)
    {
        Debug.LogWarning("��������û����д��");    
    }
    public virtual void InteractOperate(player player)
    {
        
    }
    public void SelectCounter()
    {
        selectedcounter.SetActive(true);
        OnSelected?.Invoke(this, EventArgs.Empty);
    }
    public void CancelSelect()
    {
        selectedcounter.SetActive(false);
        OnDeselected?.Invoke(this, EventArgs.Empty);
    }

}
