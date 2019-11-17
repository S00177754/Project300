﻿using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class UnitDetailButton : MonoBehaviour
{
    public Image imgUnit;
    public Image imgType;
    public TextMeshProUGUI tmpTxtName;
    public TextMeshProUGUI tmpTxtHealth;
    public RenderTexture renderTexture;

    private UnitDetails details;

    public void InitializeUnitButton(UnitDetails unitDetails)
    {
        details = unitDetails;
        tmpTxtName.text = details.Name;
    }


    private void Update()
    {
        if(details != null)
        tmpTxtHealth.text = $"{details.Health} /{details.MaxHealth}";
    }

    public void DebugMe()
    {
        Debug.Log("I am an unit detail button and have been clicked!");
    }
   
}
