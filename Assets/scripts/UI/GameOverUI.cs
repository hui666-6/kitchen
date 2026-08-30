using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameOverUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI number;
    [SerializeField] GameObject uiparent;
    void Start()
    {
        hide();
        GameManager.Instance.onchangstate += GameManager_onchangstate;
    }

    private void GameManager_onchangstate(object sender, System.EventArgs e)
    {
        if (GameManager.Instance.IsGameOverState())
        { 
           show();
           number.text=OrderManager.Instance.GetsuccessDeliverCount().ToString();
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
