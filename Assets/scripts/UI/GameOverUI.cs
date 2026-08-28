using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameOverUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI number;
    [SerializeField] TextMeshProUGUI starText;
    [SerializeField] GameObject uiparent;
    [SerializeField] Button nextbutton;
    [SerializeField] Button retrybutton;
    [SerializeField] Button menubutton;

    void Start()
    {
        hide();
        GameManager.Instance.onchangstate += GameManager_onchangstate;
        if (nextbutton != null)
        {
            nextbutton.onClick.AddListener(() => Loader.LoadLevel(LevelManager.Instance.CurrentIndex + 1));
        }
        if (retrybutton != null)
        {
            retrybutton.onClick.AddListener(() => Loader.LoadLevel(LevelManager.Instance.CurrentIndex));
        }
        if (menubutton != null)
        {
            menubutton.onClick.AddListener(() => Loader.load(Loader.scene.GameMenu));
        }
    }

    private void GameManager_onchangstate(object sender, System.EventArgs e)
    {
        if (GameManager.Instance.IsGameOverState())
        {
            show();
            int deliveryCount = OrderManager.Instance.GetsuccessDeliverCount();
            number.text = deliveryCount.ToString();
            if (LevelManager.Instance != null)
            {
                int star = LevelManager.Instance.CompleteLevel(deliveryCount);
                if (starText != null)
                {
                    starText.text = "⭐" + star + " 星";
                }
                if (nextbutton != null)
                {
                    nextbutton.gameObject.SetActive(!LevelManager.Instance.IsLastLevel());
                }
            }
        }
    }

    private void show()
    { 
      uiparent.SetActive(true);
    }
    private void hide()
    {
        uiparent.SetActive(false);
    }
}
