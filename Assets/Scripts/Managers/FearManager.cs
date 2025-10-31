using System.Collections;
using UnityEngine;

public class FearManager : MonoBehaviour
{
    [SerializeField]
    private float gameOverTimer = 30f;

    //if room state is not default then add timer to gameover similar to amnesia decreasing the intensity of lights with time

    private float gameTimer;

    private bool timerEnabled = false;

    private void Start()
    {
        gameTimer = gameOverTimer;
        StateChecker.OnCheckStateChanged += StateChecker_OnCheckStateChanged;
    }

    private void StateChecker_OnCheckStateChanged(object sender, StateChecker.OnCheckStateChangeEventArgs e)
    {
        if(e.state == StateChecker.State.Original)
        {
            if(timerEnabled)
            {
                ResetGameTimer();
            }
        }
        else
        {
            SetGameTimer();
        }
    }

    private void Update()
    {
        if(timerEnabled)
        if (gameTimer > 0)
        {
            gameTimer -= Time.deltaTime;
        }
        else
        {
            Debug.Log("gameOver");
        }
    }

    private void SetGameTimer()
    {
        timerEnabled = true;
    }
    private void ResetGameTimer()
    {
        gameTimer = gameOverTimer;
        timerEnabled = false;
    }
}