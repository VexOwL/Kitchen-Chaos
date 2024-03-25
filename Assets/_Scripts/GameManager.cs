using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private State _state;
    private float _waitingToStartTimer = 1, _coutdownTimer = 3, _gameTimer, _gameTimerMax = 10;
    public static GameManager Instance;
    public event EventHandler OnStateChanged;

    private enum State
    {
        WaitingToStart,
        CountdownToStart,
        GamePlaying,
        GameOver
    }

    private void Awake()
    {
        Instance = this;

        _state = State.WaitingToStart;
    }

    private void Update()
    {
        switch (_state)
        {
            case State.WaitingToStart:
                _waitingToStartTimer -= Time.deltaTime;

                if (_waitingToStartTimer <= 0)
                {
                    _state = State.CountdownToStart;
                    OnStateChanged?.Invoke(this, EventArgs.Empty);
                }

                break;

            case State.CountdownToStart:
                _coutdownTimer -= Time.deltaTime;

                if (_coutdownTimer <= 0)
                {
                    _state = State.GamePlaying;

                    _gameTimer = _gameTimerMax;

                    OnStateChanged?.Invoke(this, EventArgs.Empty);
                }

                break;

            case State.GamePlaying:
                _gameTimer -= Time.deltaTime;

                if (_gameTimer <= 0)
                {
                    _state = State.GameOver;
                    OnStateChanged?.Invoke(this, EventArgs.Empty);
                }

                break;

            case State.GameOver:
                break;
        }

        Debug.Log(_state);
    }

    public bool IsGamePlaying()
    {
        return _state == State.GamePlaying;
    }

    public bool IsGameOver()
    {
        return _state == State.GameOver;
    }

    public bool IsCountdownToStartActive()
    {
        return _state == State.CountdownToStart;
    }

    public float GetCountdownToStartTimer()
    {
        return _coutdownTimer;
    }

    public float GetGameTimerNormalized()
    {
        return _gameTimer / _gameTimerMax;
    }
}
