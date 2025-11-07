using System.Collections;
using UnityEngine;

public class SimGameManager : MonoBehaviour
{
    public static SimGameManager Instance;

    public enum GameState
    {
        isPlaying,
        isPaused
    }

    public GameState currentState;

    private void Start()
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
    public bool IsGamePlaying()
    {
        return currentState == GameState.isPlaying;
    }
}