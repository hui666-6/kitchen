using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TutorialUI : MonoBehaviour
{
    [SerializeField] private GameObject uiparent;
    [SerializeField] private List<TextMeshProUGUI> keyTexts = new List<TextMeshProUGUI>();

    private void Start()
    {
        GameManager.Instance.onchangstate += gamemanager_onchangstate;
        show();
    }
    private void OnDisable()
    {
        GameManager.Instance.onchangstate -= gamemanager_onchangstate;
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
/// <summary>
/// 更新按键显示 gameinput 在awake里会从playerprefs读取玩家自定义的按键覆盖
/// </summary>
    private void UpdateVisual()
    {
        var bindingTypes = new[]
        {
            gameinput.BindingType.forward,
            gameinput.BindingType.back,
            gameinput.BindingType.left,
            gameinput.BindingType.right,
            gameinput.BindingType.get,
            gameinput.BindingType.cut,
            gameinput.BindingType.pause
        };

        for (int i = 0; i < bindingTypes.Length && i < keyTexts.Count; i++)
        {
            keyTexts[i].text = gameinput.Instance.GetBindingDisplayString(bindingTypes[i]);
        }
    }
}
