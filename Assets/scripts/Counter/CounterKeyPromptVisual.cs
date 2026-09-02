using UnityEngine;

/// <summary>
/// 通用柜台按键提示视觉效果。
/// 适用于所有继承自 BaseCounter 的柜台。
/// 只在新手教程关中，当该柜台被选中时显示按键提示，取消选中时隐藏。
/// </summary>
public class CounterKeyPromptVisual : MonoBehaviour
{
    [SerializeField] private BaseCounter counter;      // 目标柜台，若为空会自动尝试获取当前父级柜台
    [SerializeField] private GameObject keyPromptVisual;  // 柜台正上方的按键图片根对象

    private void Reset()
    {
        if (counter == null)
        {
            counter = GetComponentInParent<BaseCounter>();
        }
    }

    private void Awake()
    {
        if (counter == null)
        {
            counter = GetComponentInParent<BaseCounter>();
        }
    }

    private void OnEnable()
    {
        Hide();

        if (counter == null)
        {
            return;
        }

        counter.OnSelected += Counter_OnSelected;
        counter.OnDeselected += Counter_OnDeselected;
    }

    private void OnDisable()
    {
        if (counter != null)
        {
            counter.OnSelected -= Counter_OnSelected;
            counter.OnDeselected -= Counter_OnDeselected;
        }
    }

    private void Counter_OnSelected(object sender, System.EventArgs e)
    {
        if (LevelManager.Instance != null && LevelManager.Instance.IsTutorialLevel())
        {
            Show();
        }
    }

    private void Counter_OnDeselected(object sender, System.EventArgs e)
    {
        Hide();
    }

    private void Show()
    {
        if (keyPromptVisual != null)
        {
            keyPromptVisual.SetActive(true);
        }
    }

    private void Hide()
    {
        if (keyPromptVisual != null)
        {
            keyPromptVisual.SetActive(false);
        }
    }
}
