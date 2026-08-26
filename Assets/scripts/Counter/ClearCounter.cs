using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearCounter : BaseCounter
{
    
    public override void Interact(player player)
    {
        if (player.IsHavekitchenobject())//玩家手上有食材或盘子
        {
            if (player.GetKitchenObject().TryGetComponent<platekitchenobject>(out platekitchenobject platekitchenobject))//有盘子
            {
                if (IsHavekitchenobject() == false)
                {
                    TransferKitchenObject(player, this); //将盘子放在桌子上
                }
                else //柜台不是空
                {
                 bool Success = platekitchenobject.AddKitchenobjectSO(GetKitchenObjectSO());
                 if (Success)
                 {
                   Destorykitchenobject();
                 }
                }

            }
            else //玩家手上是食材
            {
                if (IsHavekitchenobject() == false) //柜台为空
                {
                    TransferKitchenObject(player, this);
                }
                else
                {
                    if (GetKitchenObject().TryGetComponent<platekitchenobject>(out platekitchenobject))
                    {
                        if (platekitchenobject.AddKitchenobjectSO(player.GetKitchenObjectSO())) 
                        {
                            player.Destorykitchenobject();
                        }
                    }
                }

            }
        }

        else 
        {
            
            if (IsHavekitchenobject() == false) //柜台为空
            {
              
            }
            else
            {
                TransferKitchenObject(this, player);
            }


        }

    }
    

}
