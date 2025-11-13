using System.Collections;
using UnityEngine;

public class SimGameManager : MonoBehaviour
{
    public static SimGameManager Instance = null;

    private bool isMainMenuActive = false;
    
    public enum GameState
    {
        isPlaying,
        isPaused
    }

    public GameState currentState;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(Instance);
        }
        Instance = this;

        currentState = GameState.isPlaying;
    }
    public void PauseGame()
    {
        currentState = GameState.isPaused;
    }
    public void StartGame()
    {
        currentState = GameState.isPlaying;
    }

    public void SetMenuState(bool state)
    {
        isMainMenuActive = state;
    }
    public bool IsMainMenuActive()
    {
        return isMainMenuActive;
    }

    public bool IsGamePlaying()
    {
        return currentState == GameState.isPlaying;
    }
}