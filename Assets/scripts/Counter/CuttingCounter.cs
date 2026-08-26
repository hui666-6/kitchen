using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static CuttingRecipListSO;

public class CuttingCounter : BaseCounter
{
    [SerializeField] public CuttingRecipListSO cuttingRecipList;
    [SerializeField] private ProgressBarUI progressBarUI;
    [SerializeField] private CuttingCounterVisual CuttingCounterVisual;
                     private int cuttingcount;
    public static event EventHandler onchop;
    public override void Interact(player player)
    {
        if (player.IsHavekitchenobject())
        {
            if (IsHavekitchenobject() == false)
            {
                cuttingcount = 0; 
                TransferKitchenObject(player, this);
            }
            else
            {


            }
        }
        else
        {
            if (IsHavekitchenobject() == false)
            {

            }
            else
            {
                TransferKitchenObject(this, player);
                progressBarUI.hide();
            }

        }

    }
    public override void InteractOperate(player player)
    {
        if (IsHavekitchenobject())
        {
         
            if (cuttingRecipList.TryGetCuttinigRecipe(GetKitchenObject().GetKitchenObjectSO(), out CuttingRecipe cuttingRecipe))
            {

                    cut();
                    progressBarUI.UpdateProgress((float)cuttingcount / cuttingRecipe.cuttingcountmax);
                    if (cuttingcount == cuttingRecipe.cuttingcountmax)
                    {

                        Destorykitchenobject();
                        creatkitchenobject(cuttingRecipe.output.prefab);
                    }
            }
        }
    }
   private void cut()
   {
     onchop?.Invoke(this, EventArgs.Empty);
     cuttingcount++;
     CuttingCounterVisual.playCut();
   }

    public static void clearStaticData()
    { 
     onchop = null;
    }

}
