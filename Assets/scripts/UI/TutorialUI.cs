using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TutorialUI : MonoBehaviour
{
    [SerializeField] private GameObject uiparent;
    [SerializeField] private TextMeshProUGUI forwardkey;
    [SerializeField]private TextMeshProUGUI  backkey;
    [SerializeField] private TextMeshProUGUI leftkey;
    [SerializeField] private TextMeshProUGUI rightkey;
    [SerializeField] private TextMeshProUGUI getkey;
    [SerializeField] private TextMeshProUGUI cutkey;
    [SerializeField] private TextMeshProUGUI pausekey;
    private void Start()
    {
        GameManager.Instance.onchangstate += gamemanager_onchangstate;
        show();
    }

    private void gamemanager_onchangstate(object sender, System.EventArgs e)
    {
        if (GameManager.Instance.IsGameWaitingToStart())
        {
            show();
            UpdateVisual();
        }
        else 
        { 
         hide();
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
    private void UpdateVisual()
    {
        forwardkey.text = gameinput.Instance.GetBindingDisplayString(gameinput.BindingType.forward);
        backkey.text = gameinput.Instance.GetBindingDisplayString(gameinput.BindingType.back);
        leftkey.text = gameinput.Instance.GetBindingDisplayString(gameinput.BindingType.left);
        rightkey.text = gameinput.Instance.GetBindingDisplayString(gameinput.BindingType.right);
        getkey.text = gameinput.Instance.GetBindingDisplayString(gameinput.BindingType.get);
        cutkey.text = gameinput.Instance.GetBindingDisplayString(gameinput.BindingType.cut);
        pausekey.text = gameinput.Instance.GetBindingDisplayString(gameinput.BindingType.pause);

    }
}
