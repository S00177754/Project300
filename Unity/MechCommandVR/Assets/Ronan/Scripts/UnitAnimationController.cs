using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum UnitAnimationTriggers { None,Death,RoundhouseKick,Punching,Damaged}

public class UnitAnimationController : MonoBehaviour
{
    public Animator animator;
    public bool IsWalking = false;
    private UnitAnimationTriggers triggerState;

    private void Start()
    {
        triggerState = UnitAnimationTriggers.None;
    }

    private void Update()
    {
        WalkCheck();
        TriggerCheck();
    }

    public void TriggerCheck()
    {
        switch (triggerState)
        {
            default:
            case UnitAnimationTriggers.None:
                break;

            case UnitAnimationTriggers.Death:
                animator.SetTrigger("Death");
                break;

            case UnitAnimationTriggers.Damaged:
                animator.SetTrigger("Damaged");
                break;

            case UnitAnimationTriggers.Punching:
                animator.SetTrigger("Punching");
                break;

            case UnitAnimationTriggers.RoundhouseKick:
                animator.SetTrigger("RoundhouseKick");
                break;

        }
    }

    public void SetTrigger(UnitAnimationTriggers state)
    {
        triggerState = state;
    }

    //Bool states
    private void WalkCheck()
    {
        if (IsWalking != animator.GetBool("Walking"))
            animator.SetBool("Walking", IsWalking);
    }

    public void Death()
    {
        SetTrigger(UnitAnimationTriggers.Death);
    }
}
