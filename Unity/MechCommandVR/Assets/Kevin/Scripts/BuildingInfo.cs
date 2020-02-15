using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class BuildingInfo : MonoBehaviour //ISelectableMinimap interface and BaseController replace this for efficiency
{
    private GameObject player;
    public CommanderController commanderController;
    public bool isSelected { get; set; } = false;

    public string BuildingName { get; set; }

  

    Vector3 spawnLocation;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("PlayerController");
        commanderController = player.GetComponent<CommanderController>();

    }

    // Update is called once per frame
    void FixedUpdate()
    {
    
    }

    public void CreateUnits()
    {
       
    }

 
}

