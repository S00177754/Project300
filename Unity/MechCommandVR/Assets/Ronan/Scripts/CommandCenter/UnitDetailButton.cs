﻿using System;
using System.Collections;
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
    public TextMeshProUGUI tmpTxtType;
    public Slider sldrHealth;
    public RenderTexture renderTexture;
    private UnitCameraSwitcher unitCamSwitch;

    private UnitDetails details;

    public void InitializeUnitButton(UnitDetails unitDetails,UnitCameraSwitcher unitCameraSwitcher)
    {
        details = unitDetails;
        tmpTxtName.text = details.Name;
        tmpTxtType.text = "Type: " + Enum.GetName(typeof(UnitType), details.myType);
        unitCamSwitch = unitCameraSwitcher;
    }


    private void Update()
    {
        if (details != null)
        {
            if (details.Health * 100 > 0)
            {
                tmpTxtHealth.text = $"HP: {Mathf.Round(details.Health * 100)} /{details.MaxHealth * 100}";
            }
            else
            {
                Destroy(gameObject);
            }
            sldrHealth.value = details.Health;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void ActivateCam()
    {
        unitCamSwitch.SetCameraToUnit(details);
    }

    public void DebugMe()
    {
        Debug.Log("I am an unit detail button and have been clicked!");
    }
   
}
