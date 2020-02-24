﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommandCenterController : MonoBehaviour
{
    public UnitPanel UnitPanel;
    public UnitCameraSwitcher UniCamSwitch;
    public StartGamePanel StartPanel;
    public HealthPanelController PlayerHealthController;
    public HealthPanelController CPUHealthController;

    private void Start()
    {
        StartPanel.Setup();
    }
}
