using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashCounter :BaseCounter
{
    public static event EventHandler onobjecttransh;
    public override void Interact(player player)
    {
        if (player.IsHavekitchenobject())
        { 
           player.Destorykitchenobject();
           onobjecttransh?.Invoke(this, EventArgs.Empty);
        }
    }
    public static void ClearStaticData()
    { 
      onobjecttransh = null;
    }
}
