using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HideCanvasScript : MonoBehaviour
{
    public GameObject content;
    public TMP_Text  buttonText;

    private void Start()
    {
        
    }

    public void LabelButton()
    {
        buttonText.text = content.activeSelf ? "Hide" : "Show";
    }

    public void ToggleVisibility()
    {
        content.SetActive(content.activeSelf ? false:true);
        LabelButton();
    }
}
