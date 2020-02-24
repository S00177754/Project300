using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameStateController : MonoBehaviour
{
    public TMP_Text GameStateText;
    public GameObject StatePanel;

    private void Start()
    {
        StatePanel.SetActive(false);
    }

    public void ActivatePanelMessage(string text)
    {
        StatePanel.SetActive(true);
        GameStateText.text = text;
    }

    public void DeactivatePanel()
    {
        StatePanel.SetActive(false);
    }

}
