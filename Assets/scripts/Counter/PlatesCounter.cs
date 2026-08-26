using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class PlatesCounter : BaseCounter
{
    [SerializeField] private KitchenObjectSO plateso;
    [SerializeField] private float spawnrate=3;
    [SerializeField] private float platecountmax=5;
    private float Timer=0;
    private List<KitchenObject> plateslist=new List<KitchenObject>();
  

    private void Update()
    {
   
        if (plateslist.Count < platecountmax)
        {
            Timer += Time.deltaTime;
        }
        if (Timer>spawnrate)
        {
            Timer = 0;
            spawnplate();
        }
    }
    public override void Interact(player player)
    {

        if (player.IsHavekitchenobject()==false)
        {
            if (plateslist.Count > 0)
            {

                player.AddKitchenObject(plateslist[plateslist.Count - 1]);
                plateslist.RemoveAt(plateslist.Count - 1);
            }
        }
       
    
}
    public void spawnplate()
    {   if (plateslist.Count >= platecountmax) 
        {
            Timer = 0;
            return; 
        }
        
            KitchenObject kitchenObject = GameObject.Instantiate(plateso.prefab, GetHoldpoint()).GetComponent<KitchenObject>();
            kitchenObject.transform.localPosition = Vector3.zero + Vector3.up * 0.1f * plateslist.Count;
            plateslist.Add(kitchenObject);
        

    }
}
