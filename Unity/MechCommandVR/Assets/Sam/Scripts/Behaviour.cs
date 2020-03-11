using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum UnitState
{
    SCOUTING,
    ATTACKING,
    IDLE,
    FOLLOWING
}
public class Behaviour : MonoBehaviour
{
    [SerializeField]
    UnitState unitState;
    AttackComponent attackComponent;

    void Start()
    {
        unitState = UnitState.ATTACKING;
        attackComponent = gameObject.GetComponent<AttackComponent>();
    }

    //Simple methods that can be assigned to buttons to manually change unitState
    public void ChangeToAttack()
    {
        unitState = UnitState.ATTACKING;
    }

    public void ChangeToIdle()
    {
        unitState = UnitState.IDLE;
    }

    public void ChangeToScouting()
    {
        unitState = UnitState.SCOUTING;
    }


    void Update()
    {
        //switch(unitState)
        //{
        //    case (UnitState.IDLE):
        //        //logic to determine time to attack - select or InRange of enemy
        //        //if (attackComponent.CanSeeTarget)
        //        //    unitState = UnitState.FOLLOWING;
                
        //        if (attackComponent.CanAttackTarget)
        //            unitState = UnitState.ATTACKING;
        //        break;

        //    case (UnitState.SCOUTING):
        //        //logic for scouting to be added if necessary
        //        break;

        //    case (UnitState.FOLLOWING):
        //        //attackComponent.Move();
        //        if (attackComponent.CanAttackTarget)
        //            unitState = UnitState.ATTACKING;

        //        if (!attackComponent.CanSeeTarget)
        //            unitState = UnitState.IDLE;
        //        break;

        //    case (UnitState.ATTACKING):
        //        if (attackComponent != null)
        //        {
        //            //attackComponent.enabled = true;//Enable attack component
        //            attackComponent.Move();
        //        }
        //        if (!attackComponent.CanAttackTarget)
        //        {
        //            //attackComponent.enabled = false;
        //            unitState = UnitState.FOLLOWING;
        //        }
        //        break;
        //}

        
        //Chase state: 'sees' enemy and follows 
        //Scouting: less important
        //Attacking: High priority

        //AI: buid units on timer
        //send units out when so many are ready to go
    }

    
}
