using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    public static TutorialManager Instance { get; private set; }

    [SerializeField] private TextMeshProUGUI stepText;
    [SerializeField] private GameObject stepUIParent;

    private int stepIndex = -1;
    private bool tutorialRunning = false;
    private Coroutine hideRoutine;

    private const string STEP_TEXT_NAME = "TutorialStepText";

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        if (stepText == null)
        {
            GameObject found = GameObject.Find(STEP_TEXT_NAME);
            if (found != null)
            {
                stepText = found.GetComponent<TextMeshProUGUI>();
            }
        }

        GameManager.Instance.onchangstate += OnGameStateChanged;
        KitchenObjectHolder.onpickup += OnPickup;
        CuttingCounter.onchop += OnChop;
        StoveCounter.onFryingStarted += OnFryingStarted;
        OrderManager.Instance.OnRecipeSuccessed += OnDeliverySuccess;

        SetStepUIActive(false);
    }

    private void OnDestroy()
    {
        if (GameManager.Instance != null)
        {
            GameManager.Instance.onchangstate -= OnGameStateChanged;
        }
        KitchenObjectHolder.onpickup -= OnPickup;
        CuttingCounter.onchop -= OnChop;
        StoveCounter.onFryingStarted -= OnFryingStarted;
        if (OrderManager.Instance != null)
        {
            OrderManager.Instance.OnRecipeSuccessed -= OnDeliverySuccess;
        }
    }

    private void OnGameStateChanged(object sender, EventArgs e)
    {
        if (GameManager.Instance.IsGameOverState())
        {
            SetStepUIActive(false);
            return;
        }

        bool isTutorial = LevelManager.Instance != null && LevelManager.Instance.IsTutorialLevel;
        if (GameManager.Instance.IsGamePlayingState() && isTutorial)
        {
            StartTutorial();
        }
        else
        {
            SetStepUIActive(false);
        }
    }

    private void StartTutorial()
    {
        if (tutorialRunning) return;
        tutorialRunning = true;
        stepIndex = 0;
        SetStepUIActive(true);
        UpdateStepText();
    }

    private void OnPickup(object sender, EventArgs e)
    {
        if (tutorialRunning && stepIndex == 0)
        {
            stepIndex = 1;
            UpdateStepText();
        }
    }

    private void OnChop(object sender, EventArgs e)
    {
        AdvanceAfterProcessing();
    }

    private void OnFryingStarted(object sender, EventArgs e)
    {
        AdvanceAfterProcessing();
    }

    private void AdvanceAfterProcessing()
    {
        if (tutorialRunning && stepIndex == 1)
        {
            stepIndex = 2;
            UpdateStepText();
        }
    }

    private void OnDeliverySuccess(object sender, EventArgs e)
    {
        if (tutorialRunning && stepIndex == 2)
        {
            stepIndex = 3;
            UpdateStepText();
            tutorialRunning = false;
            if (hideRoutine != null) StopCoroutine(hideRoutine);
            hideRoutine = StartCoroutine(HideAfterDelay(3f));
        }
    }

    private void UpdateStepText()
    {
        if (stepText == null) return;

        string forward = gameinput.Instance.GetBindingDisplayString(gameinput.BindingType.forward);
        string back = gameinput.Instance.GetBindingDisplayString(gameinput.BindingType.back);
        string left = gameinput.Instance.GetBindingDisplayString(gameinput.BindingType.left);
        string right = gameinput.Instance.GetBindingDisplayString(gameinput.BindingType.right);
        string interact = gameinput.Instance.GetBindingDisplayString(gameinput.BindingType.get);
        string operate = gameinput.Instance.GetBindingDisplayString(gameinput.BindingType.cut);

        switch (stepIndex)
        {
            case 0:
                stepText.text = "第 1 步：用 [" + forward + "/" + back + "/" + left + "/" + right + "] 走到食材柜，按 [" + interact + "] 拿起食材";
                break;
            case 1:
                stepText.text = "第 2 步：把食材放到灶台或切菜台上，按 [" + interact + "] 放上去加工（煎或切）";
                break;
            case 2:
                stepText.text = "第 3 步：把加工好的食材放上盘子，走到出餐口按 [" + interact + "] 送餐";
                break;
            case 3:
                stepText.text = "教程完成！继续练习，倒计时结束后结算成绩";
                break;
            default:
                stepText.text = "";
                break;
        }
    }

    private void SetStepUIActive(bool active)
    {
        if (stepUIParent != null)
        {
            stepUIParent.SetActive(active);
        }
        else if (stepText != null)
        {
            stepText.gameObject.SetActive(active);
        }
    }

    private IEnumerator HideAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        SetStepUIActive(false);
    }
}
