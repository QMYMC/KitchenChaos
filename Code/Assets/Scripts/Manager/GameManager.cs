using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public event EventHandler OnStateChanged;
    public event EventHandler OnGamePause;
    public event EventHandler OnGameUnPause;

    private enum State
    {
        WaitingStart,
        CountDownToStart,
        GamePlaying,
        GameOver
    }

    [SerializeField] private Player player;

    private State state;

    private float waitingToStartTimer = 1f;
    private float countDownToStartTimer = 3f;
    private float gamePlayingTimer = 90f;
    private float gamePlayingTimeTotal;

    private bool isGamePause = false;

    private void Awake()
    {
        Instance = this;
        gamePlayingTimeTotal = gamePlayingTimer;
    }

    private void Start()
    {
        TurnToWaitingToStart();
    }

    private void Update()
    {
        SwitchState();
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ToggleGame();
        }
    }

    private void SwitchState()
    {
        switch (state)
        {
            case State.WaitingStart:
                waitingToStartTimer -= Time.deltaTime;
                if (waitingToStartTimer <= 0)
                {
                    TurnToCountDownToStart();
                }

                break;
            case State.CountDownToStart:
                countDownToStartTimer -= Time.deltaTime;
                if (countDownToStartTimer <= 0)
                {
                    TurnToGamePlaying();
                }

                break;
            case State.GamePlaying:
                gamePlayingTimer -= Time.deltaTime;
                if (gamePlayingTimer <= 0)
                {
                    TurnToGameOver();
                }

                break;
            case State.GameOver:
                break;
        }
    }

    private void TurnToWaitingToStart()
    {
        state = State.WaitingStart;
        DisablePlayer();
        OnStateChanged?.Invoke(this, EventArgs.Empty);
    }

    private void TurnToCountDownToStart()
    {
        state = State.CountDownToStart;
        DisablePlayer();
        OnStateChanged?.Invoke(this, EventArgs.Empty);
    }

    private void TurnToGamePlaying()
    {
        state = State.GamePlaying;
        EnablePlayer();
        OnStateChanged?.Invoke(this, EventArgs.Empty);
    }

    private void TurnToGameOver()
    {
        state = State.GameOver;
        DisablePlayer();
        OnStateChanged?.Invoke(this, EventArgs.Empty);
    }

    private void DisablePlayer()
    {
        player.enabled = false;
    }

    private void EnablePlayer()
    {
        player.enabled = true;
    }

    public bool IsCountDownState()
    {
        return state == State.CountDownToStart;
    }

    public bool IsGamePlayingState()
    {
        return state == State.GamePlaying;
    }

    public bool IsGameOverState()
    {
        return state == State.GameOver;
    }

    public float GetCountDownTimer()
    {
        return countDownToStartTimer;
    }

    public void ToggleGame()
    {
        isGamePause = !isGamePause;
        if (isGamePause)
        {
            Time.timeScale = 0;
            OnGamePause?.Invoke(this, EventArgs.Empty);
        }
        else
        {
            Time.timeScale = 1;
            OnGameUnPause?.Invoke(this, EventArgs.Empty);
        }
    }

    public float GetGamePlayingTimer()
    {
        return gamePlayingTimer;
    }

    public float GetGamePlayingTimerNormalized()
    {
        return (gamePlayingTimer / gamePlayingTimeTotal);//这里是从大到小变的，所以是1-
    }
}
