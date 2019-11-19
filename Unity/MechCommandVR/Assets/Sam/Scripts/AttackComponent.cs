using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackComponent : MonoBehaviour
{
    UnitComponent Me;

    void Start()
    {
        Me = gameObject.GetComponent<UnitComponent>();
       //Initialise Attacking unit
    }

    
    void Attack(UnitComponent attackThis)
    {
        Me.AttackModifier = Me.SignModifier(attackThis.myType) * Me.LevelModifier(attackThis);
        attackThis.HealthPoints -= Me.AttackPower * Me.AttackModifier;
    }

    void Update()
    {
        
    }
}
