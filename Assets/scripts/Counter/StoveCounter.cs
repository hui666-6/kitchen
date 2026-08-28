using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
//using UnityEditorInternal;
using UnityEngine;
using UnityEngine.UIElements;

public class StoveCounter : BaseCounter

{
    public static event EventHandler onFryingStarted;
    [SerializeField] private FryingRecipeSO fryingrecipelist;
    [SerializeField] private FryingRecipeSO burningrecipelist;
    [SerializeField] private StoveCouonterVisual stoveCouonterVisual;
    [SerializeField] private ProgressBarUI progressbarUI;
    [SerializeField] private AudioSource sound;
    private WarningUI warningUI;
    private float warningtimenormalize = 0.5f;
    private FryingRecipe fryingrecipe;
    private float fryingTime = 0;
    private StoveState State;
    public enum StoveState
    {
        Idle,
        Frying,
        Bruning
    }
    private void Start()
    {
        warningUI=GetComponent<WarningUI>();
    }
    public override void Interact(player player)
    {
        if (player.IsHavekitchenobject())
        {
            if (IsHavekitchenobject() == false)
            {
                if (fryingrecipelist.TryGetCuttinigRecipe(
                  player.GetKitchenObject().GetKitchenObjectSO(), out FryingRecipe fryingRecipe))
                {

                    TransferKitchenObject(player, this);
                    StartFrying(fryingRecipe);
                }
                else if (burningrecipelist.TryGetCuttinigRecipe(
                    player.GetKitchenObject().GetKitchenObjectSO(), out FryingRecipe bruningrecipe))
                {
                    TransferKitchenObject(player, this);
                    StartBurning(bruningrecipe);
                }
            }
            else
            {

            }
        }
        else
        {
            if (IsHavekitchenobject() == false)
            {

            }
            else
            {
                turntoidle();
                TransferKitchenObject(this, player);

            }

        }
    }
    private void Update()
    {
        switch (State)
        {
            case StoveState.Idle:
                break;
            case StoveState.Frying:
                fryingTime += Time.deltaTime;
                progressbarUI.UpdateProgress(fryingTime / fryingrecipe.fryingTime);
                if (fryingTime >= fryingrecipe.fryingTime)
                {
                    Destorykitchenobject();
                    creatkitchenobject(fryingrecipe.output.prefab);
                    if (burningrecipelist.TryGetCuttinigRecipe(
                        GetKitchenObject().GetKitchenObjectSO(), out FryingRecipe newfryingRecipe))
                    {
                        StartBurning(newfryingRecipe);
                    }
                    else
                    {
                        turntoidle();
                    }


                }
                break;
            case StoveState.Bruning:

                fryingTime += Time.deltaTime;
                progressbarUI.UpdateProgress(fryingTime / fryingrecipe.fryingTime);
                if (fryingTime / fryingrecipe.fryingTime > warningtimenormalize)
                {
                    warningUI.show();
                }

                if (fryingTime >= fryingrecipe.fryingTime)
                {
                    Destorykitchenobject();
                    creatkitchenobject(fryingrecipe.output.prefab);
                    turntoidle();

                }
               
                break;
            default:
                break;

        }

    }
    private void StartFrying(FryingRecipe fryingrecipe)
    {
        fryingTime = 0;
        this.fryingrecipe = fryingrecipe;
        State = StoveState.Frying;
        stoveCouonterVisual.ShowStoveEffect();
        onFryingStarted?.Invoke(this, EventArgs.Empty);
        sound.Play();

    }
    private void StartBurning(FryingRecipe fryingrecipe)
    {
        if (fryingrecipe == null)
        {
            Debug.LogWarning("??????????????????????");
            return;
        }
        stoveCouonterVisual.ShowStoveEffect();
        fryingTime = 0;
        this.fryingrecipe = fryingrecipe;
        State = StoveState.Bruning;
        sound.Play();

    }
    private void turntoidle()
    {
        progressbarUI.hide();
        State = StoveState.Idle;
        stoveCouonterVisual.HideStoveEffect();
        sound.Pause();
        warningUI.hide();
    }
    public static void clearStaticData()
    {
        onFryingStarted = null;
    }
}
