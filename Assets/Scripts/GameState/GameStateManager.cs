using System;
using UnityEngine;

public enum GameState
{
    Playing,
    Paused,
    GameOver,
    Victory
}

public class GameStateManager : MonoBehaviour
{
    public GameState CurrentState { get; private set; } = GameState.Playing;

    public event Action<GameState> StateChanged;

    public void PauseGame()
    {
        if (CurrentState != GameState.Playing) return;
        ChangeState(GameState.Paused);
    }

    public void ResumeGame()
    {
        if (CurrentState != GameState.Paused) return;
        ChangeState(GameState.Playing);
    }

    public void EndGame()
    {
        if (CurrentState == GameState.GameOver ||
            CurrentState == GameState.Victory) return;
        ChangeState(GameState.GameOver);
    }

    public void WinGame()
    {
        if (CurrentState != GameState.Playing) return;
        ChangeState(GameState.Victory);
    }

    private void ChangeState(GameState newState)
    {
        CurrentState = newState;
        StateChanged?.Invoke(newState);
    }
}