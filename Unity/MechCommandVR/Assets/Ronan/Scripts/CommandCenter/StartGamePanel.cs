using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartGamePanel : MonoBehaviour
{
    public GameObject Button;

    public void Setup()
    {
        gameObject.SetActive(true);

    }

    public void StartGame()
    {
        GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>().StartBattle();

    }
}
