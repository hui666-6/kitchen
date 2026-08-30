using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    private enum State
    { 
        waitingtostart,
        countdowntostart,
        gameplaying,
        gameover    
    }
    private State state;
    private float watingtostarttime= 1;
    private float countdownstarttime = 3;
    private float gameplayingtimer = 60;
    private float gameplayingtimerTotal;
    [SerializeField] private player player;
    public event EventHandler onchangstate;
    public event EventHandler OnGamePaused;
    public event EventHandler OnGameUnPaused;
    private bool isgamePause=false;

    private void Awake()
    {
        Instance = this;
        gameplayingtimerTotal = gameplayingtimer;
       
    }
    private void Start()
    {
        TrunTowaitingtostart();
        gameinput.Instance.OnPause += GameInput_OnPause;
    }

    private void GameInput_OnPause(object sender, EventArgs e)
    {
        ToggleGame();
    }

    void  Update()
    {
        switch (state)
        { 
            case State.waitingtostart:
                watingtostarttime -= Time.deltaTime;
                if (watingtostarttime <= 0)
                { 
                  TruntoCountDownToStart();
                }
                break;
            case State.countdowntostart:
                countdownstarttime -= Time.deltaTime;
               
                if (countdownstarttime <= 0)
                {
                    TrunTogameplaying();
                
                }
                break ;
            case State.gameplaying:
                gameplayingtimer -= Time.deltaTime;
                if (gameplayingtimer <= 0)
                { 
                  TrunTogameover();
                }
                break ;
            case State.gameover:
                break ;
                default:
                break;
        
        
        
        
        }
        
    }

    private void TrunTowaitingtostart()
    { 
      state= State.waitingtostart;
      DisablePlayer();
      onchangstate?.Invoke(this, EventArgs.Empty);
    }

    private void TruntoCountDownToStart()
    { 
        state=State.countdowntostart;
        DisablePlayer() ;
        onchangstate?.Invoke(this, EventArgs.Empty);
    }
    private void TrunTogameplaying()
    { 
      state= State.gameplaying;
      EnablePlayer();
      onchangstate?.Invoke(this, EventArgs.Empty);

    }
    private void TrunTogameover()
    { 
     state =State.gameover;
     DisablePlayer();
     onchangstate?.Invoke(this, EventArgs.Empty);

    }
    private  void DisablePlayer()
    { 
        player.enabled = false;
      
    }
    private void EnablePlayer()
    {
        player.enabled = true;
        
    }
    public bool IsCountDownToStart()
    { 
      return state==State.countdowntostart;
  
    }
    public float Getcountdowntimer()
    { 
      return countdownstarttime;
    }
    public bool IsGamePlayingState()
    {
        return state==State.gameplaying; 
    }
    public bool IsGameOverState()
    {
        return state == State.gameover;
    }
    public bool IsGameWaitingToStart()
    { 
    
      return  state==State.waitingtostart;
    }
    public void ToggleGame()
    {
        isgamePause = !isgamePause;
        if (isgamePause)
        {
            Time.timeScale = 0;
            OnGamePaused?.Invoke(this, EventArgs.Empty);
        }
        else
        {
            Time.timeScale= 1;
            OnGameUnPaused?.Invoke(this, EventArgs.Empty);
        }
    }
    public float GetGamePlayingTime()
    { 
      return gameplayingtimer;
    }
    public float GetGamePlayingTimeNormal()
    { 
     return gameplayingtimer/gameplayingtimerTotal;
    }
}
