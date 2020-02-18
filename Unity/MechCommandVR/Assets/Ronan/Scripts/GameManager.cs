using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Camera MiniMapCam;

    [Header("Commanders")]
    public CommanderController Player;
    public CommanderController AI;

    private GameState gameState;

    private void Start()
    {
        //Player.
    }

    private void Update()
    {
        CheckForGameEnd();
    }

    public void CheckForGameEnd()
    {
        if(Player.Base.PowerBuilding.Health <= 0)
        {
            gameState = GameState.GameOver;
        }
        else if(AI.Base.PowerBuilding.Health <= 0)
        {
            gameState = GameState.GameOver;
        }
    }

    public void StartBattle()
    {
        Time.timeScale = 1;
        gameState = GameState.Battling;

        Player.Base.PowerBuilding.FullPower();
        AI.Base.PowerBuilding.FullPower();
    }

    public void PauseGame()
    {
        Time.timeScale = 0;
        gameState = GameState.Paused;
    }
}

public enum GameState
{
    Battling,
    Paused,
    GameOver
}
