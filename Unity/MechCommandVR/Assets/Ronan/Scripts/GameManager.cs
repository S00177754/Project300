using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Camera MiniMapCam;
    public GameObject StartPanel;

    [Header("Commanders")]
    public CommanderController Player;
    public CommanderController AI;

    private GameState gameState;

    private void Awake()
    {
        Time.timeScale = 0;
    }

    private void Start()
    {
        
    }

    private void Update()
    {
        CheckForGameEnd();
        UpdateCommandCenter();
    }

    public void UpdateCommandCenter()
    {
        Player.Base.CommandHUB.PlayerHealthController.SetHealth(Player.Base.PowerBuilding.Health);
        Player.Base.CommandHUB.CPUHealthController.SetHealth(AI.Base.PowerBuilding.Health);
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

        StartPanel.SetActive(false);
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
