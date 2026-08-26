using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IconUI : MonoBehaviour
{
    [SerializeField] private Image Iconimage;
    public void show(Sprite sprite)
    { 
     gameObject.SetActive(true);
     Iconimage.sprite = sprite;
    }
    public void hide()
    {
        gameObject.SetActive(false);
    }
}
