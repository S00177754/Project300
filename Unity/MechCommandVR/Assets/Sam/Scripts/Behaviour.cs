using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Behaviour : MonoBehaviour
{
    UnitState unitState;
    void Start()
    {
        unitState = UnitState.IDLE;
    }

    void Update()
    {
        switch(unitState)
        {
            case (UnitState.IDLE):
                //Logic for slecting to scout
                unitState = UnitState.SCOUTING;
                //logic to determine time to attack
                unitState = UnitState.ATTACKING;
                break;
            case (UnitState.SCOUTING):
                break;
            case (UnitState.ATTACKING):
                break;
        }
    }
}
