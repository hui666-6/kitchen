using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KitchenObjectUI : MonoBehaviour
{
  [SerializeField]  private IconUI iconTempleteUI;
    private void Start()
    {
        iconTempleteUI.hide();
    }
    public void showkitchenobjectUI(KitchenObjectSO kitchenObjectSO)
    {
        IconUI newIcon = GameObject.Instantiate(iconTempleteUI, transform);
        newIcon.show(kitchenObjectSO.Sprite);
    }
}
