using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SettingsUI : MonoBehaviour
{   public static SettingsUI instance { get; private set; }
    [SerializeField] private GameObject uiparent;
    [SerializeField] private Button musicbutton;
    [SerializeField] private Button soundsbutton;
    [SerializeField] private TextMeshProUGUI soundtext;
    [SerializeField] private TextMeshProUGUI musictext;
    [SerializeField] private Button Exitbutton;


    [SerializeField] private Button forwardbutton;
    [SerializeField] private Button backbutton;
    [SerializeField] private Button leftbutton;
    [SerializeField] private Button rightbutton;
    [SerializeField] private Button getbutton;
    [SerializeField] private Button cutbutton;
    [SerializeField] private Button pausebutton;

    [SerializeField] private TextMeshProUGUI forward;
    [SerializeField] private TextMeshProUGUI back;
    [SerializeField] private TextMeshProUGUI left;
    [SerializeField] private TextMeshProUGUI right;
    [SerializeField] private TextMeshProUGUI get;
    [SerializeField] private TextMeshProUGUI cut;
    [SerializeField] private TextMeshProUGUI pause;

    [SerializeField] private GameObject rebinding;


    public void Awake()
    {
        instance = this;
    }
    void Start()
    {
        hide();
        UpdateVisual();
        musicbutton.onClick.AddListener(() => 
        {
            MusicManager.instance.OnChangeVolume();
            UpdateVisual();
        });
        soundsbutton.onClick.AddListener(() =>
        {
            SoundManager.instance.ChangeVolume();
            UpdateVisual();
        });
        Exitbutton.onClick.AddListener(() =>
        {
            hide();
        });
        forwardbutton.onClick.AddListener(() => 
        {
            Rebinding(gameinput.BindingType.forward);
        });
        backbutton.onClick.AddListener(() =>
        {
            Rebinding(gameinput.BindingType.back);
        });
        
        leftbutton.onClick.AddListener(() =>
        {
            Rebinding(gameinput.BindingType.left);
        });
        rightbutton.onClick.AddListener(() =>
        {
            Rebinding(gameinput.BindingType.right);
        });
        getbutton.onClick.AddListener(() =>
        {
            Rebinding(gameinput.BindingType.get);
        });
        cutbutton.onClick.AddListener(() =>
        {
            Rebinding(gameinput.BindingType.cut);
        });
        pausebutton.onClick.AddListener(() =>
        {
            Rebinding(gameinput.BindingType.pause);
        });
    }
     
   public void show()
    { 
      uiparent.SetActive(true);
    }
    private void hide()
    {
      uiparent.SetActive(false);
    }
    private void UpdateVisual()
    {
        soundtext.text="音效大小:"+SoundManager.instance.GetVolume();
        musictext.text="音乐大小:"+MusicManager.instance.GetVolume();
        forward.text = gameinput.Instance.GetBindingDisplayString(gameinput.BindingType.forward);
        back.text = gameinput.Instance.GetBindingDisplayString(gameinput.BindingType.back);
        left.text = gameinput.Instance.GetBindingDisplayString(gameinput.BindingType.left);
        right.text = gameinput.Instance.GetBindingDisplayString(gameinput.BindingType.right);
        get.text = gameinput.Instance.GetBindingDisplayString(gameinput.BindingType.get);
        cut.text = gameinput.Instance.GetBindingDisplayString(gameinput.BindingType.cut);
        pause.text = gameinput.Instance.GetBindingDisplayString(gameinput.BindingType.pause);
    }
    private void Rebinding(gameinput.BindingType bindingType)
    {
        rebinding.SetActive(true);
        gameinput.Instance.ReBinding( bindingType , () => 
        {
            rebinding.SetActive(false);
            UpdateVisual();
        });
    }
   
}
