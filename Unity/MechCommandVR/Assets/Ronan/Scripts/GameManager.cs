using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Camera MiniMapCam;
   // public GameObject StartPanel;

    [Header("Commanders")]
    public CommanderController Player;
    public CommanderController AI;

    public static GameState gameState;

    private void Awake()
    {
        PauseGame();
    }

    private void Start()
    {
        
    }

    private void Update()
    {
        CheckForGameEnd();
        UpdateCommandCenter();

        switch (gameState)
        {
            case GameState.Battling:
                Player.Base.CommandHUB.GameStateController.ActivatePanelMessage("Enemies Incoming");
                break;

            case GameState.GameOver:
                if(Player.Base.PowerBuilding.Health <= 0)
                    Player.Base.CommandHUB.GameStateController.ActivatePanelMessage("Game Over");
                else if(AI.Base.PowerBuilding.Health <= 0)
                    Player.Base.CommandHUB.GameStateController.ActivatePanelMessage("You Win!");
                break;

            case GameState.Paused:
                Player.Base.CommandHUB.GameStateController.DeactivatePanel();
                break;
        }
    }

    public void UpdateCommandCenter()
    {
        Player.Base.CommandHUB.PlayerHealthController.SetHealth(Player.Base.PowerBuilding.Health, Player.Base.PowerBuilding.MaxHealth);
        Player.Base.CommandHUB.CPUHealthController.SetHealth(AI.Base.PowerBuilding.Health, Player.Base.PowerBuilding.MaxHealth);
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

        //StartPanel.SetActive(false);
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
