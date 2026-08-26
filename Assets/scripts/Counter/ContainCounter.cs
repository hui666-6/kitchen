using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContainCounter : BaseCounter
{
    [SerializeField] private KitchenObjectSO kitchen0bjectSO;
    [SerializeField]private ContainerCounterVisual ContainerCounterVisual;

    
    public override void Interact(player player)
    {

        if (player.IsHavekitchenobject()) return;
        {
            creatkitchenobject(kitchen0bjectSO.prefab);
            TransferKitchenObject(this, player);
            ContainerCounterVisual.playOpen();

        }
    }
    
          
       
    
}
