using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackComponent : MonoBehaviour
{
    UnitComponent Me;
    UnitComponent AttackThis;
    public float AttackDelay;
    float timer = 0f;
    //multiple tagets
    List<UnitComponent> AttackThese;
    Ray ray;

    bool CanSeeTarget;

    void Start()
    {
        Me = gameObject.GetComponent<UnitComponent>();
    }

    
    void Attack()
    {
        if(Me != null)
        {
            Me.AttackModifier = Me.SignModifier(AttackThis.myType) * Me.LevelModifier(AttackThis);
            if(Time.deltaTime <= AttackDelay)
            {
                AttackThis.HealthPoints -= Me.AttackPower * Me.AttackModifier;
                //Reset timer
                timer = 0f;
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        AttackThis = collision.gameObject.GetComponent<UnitComponent>();
        if (AttackThis != null)
        {
            if (collision.gameObject.tag.Equals("Enemy"))
            {
                //TODO add target lock to avoid switiching targets
                //raycast will work better for multiple enemies
                CanSeeTarget = true;
                Attack();
            }

        }
        else
            CanSeeTarget = false;
    }

    private void OnCollisionExit(Collision collision)
    {
        //check reverse collision is with unittype
        AttackThis = collision.gameObject.GetComponent<UnitComponent>();
        if (AttackThis != null)
        {
            if (collision.gameObject.tag.Equals("Enemy"))
            {
                CanSeeTarget = false;
            }

        }
    }

    void Update()
    {
        if (AttackThis == null)
            return;

        if (timer <= 0f) 
            timer = AttackDelay;

        if (timer > 0f)
            timer -= Time.deltaTime;

        if (timer <= 0f) 
           Attack();
    }
}
