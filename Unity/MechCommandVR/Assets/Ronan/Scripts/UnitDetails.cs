﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitDetails : MonoBehaviour
{
    public CommanderController Commander;

    [Header("Unit Info")]
    public int UnitId;
    public string Name;

    public UnitType myType;
    public int Level;

    [Header("Unit Stats")]
    public float MaxHealth;
    public float Health;

    public float AttackPower;
    public float AttackModifier;

    [Header("Unit States")]
    public bool IsSelected;
    public bool IsControlled;


    public GameObject MinimapIcon;

    void Awake()
    {
        var gameObjectRender = MinimapIcon.GetComponent<Renderer>();
        gameObjectRender.material.SetColor("_Color",Commander.PlayerColor);

        IsSelected = false;
        IsControlled = false;
        Health = MaxHealth;

        Commander.Units.Add(this);
        Debug.Log("Unit " + Name + " Added");
    }

}
