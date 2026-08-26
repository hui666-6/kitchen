using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CuttingCounterVisual : MonoBehaviour
{
    private Animator anim;
    private  const string CUT = "Cut";
    void Start()
    {
        anim = GetComponent<Animator>();
    }
    public void playCut()
    {
        anim.SetTrigger(CUT);
    }
   
   
}
