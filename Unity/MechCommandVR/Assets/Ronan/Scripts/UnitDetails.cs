using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class UnitDetails : MonoBehaviour
{

    public CommanderController Commander;
    public Camera unitCam;

    [Header("Unit Info")]
    public int UnitId;
    public string Name;

    public UnitType myType;
    UnitState unitState;
    public int Level;

    [Header("Unit Stats")]
    public float MaxHealth = 1;
    public float Health = 1;

    public float AttackPower = 0.1f;
    public float AttackModifier = 1;

    [Header("Unit States")]
    public bool IsSelected;
    public bool IsControlled;


    public GameObject MinimapIcon;

    void Start()
    {
        var gameObjectRender = MinimapIcon.GetComponent<Renderer>();
        gameObjectRender.material.SetColor("_Color",Commander.PlayerColor);

        IsSelected = false;
        IsControlled = false;
        Health = MaxHealth;
        unitState = UnitState.IDLE;

        Commander.Units.Add(this);
        Debug.Log("Unit " + Name + " Added");
    }

    public void SetDetails(CommanderController commander, int unitId, float maxHealth, float attackPwr, float attackMod )
    {
        Commander = commander;
        UnitId = unitId;
        MaxHealth = maxHealth;
        AttackPower = attackPwr;
        AttackModifier = attackMod;
        Name = "Crackhead" + Random.Range(1,1000);
        myType = (UnitType)Random.Range(0,5);

    }

    private void OnDestroy()
    {
        Commander.RemoveUnitDetails(this);
    }

}
