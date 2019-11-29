using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackComponent : MonoBehaviour
{
    public UnitComponent Me;
    public UnitComponent AttackThis;
    public float AttackDelay;
    float timer = 0f;
    //multiple tagets
    //[HideInInspector]
    public List<UnitComponent> AttackThese;
    Ray ray;

    public bool CanSeeTarget;

    void Start()
    {
        Me = gameObject.GetComponent<UnitComponent>();
        AttackThese = new List<UnitComponent>();
    }

    
    void Attack()
    {
        AttackThis = AttackThese[0];
        if(Me != null && AttackThis != null)
        {
            Me.AttackModifier = Me.SignModifier(AttackThis.myType) * Me.LevelModifier(AttackThis);
            if(Time.deltaTime <= AttackDelay)
            {
                AttackThis.HealthPoints -= Me.AttackPower * Me.AttackModifier;
                //Reset timer
                timer = 0f;
                //Debug.Log(Me.ToString());
                Debug.Log(AttackThis.ToString());
            }
        }
        if (AttackThis == null)
            CanSeeTarget = false;
    }

    private void OnTriggerEnter(Collider collision)
    {
        UnitComponent newUnit = collision.gameObject.GetComponent<UnitComponent>();
        if (newUnit != null)
        {
            if (collision.gameObject.tag.Equals("Enemy"))
            {
                CanSeeTarget = true;
                AttackThese.Add(newUnit);
            }

        }
        else
            CanSeeTarget = false;
    }

    private void OnTriggerExit(Collider collision)
    {
        //check reverse collision is with unittype
        UnitComponent removeUnit = collision.gameObject.GetComponent<UnitComponent>();
        if (removeUnit != null)
        {
            if (collision.gameObject.tag.Equals("Enemy"))
            {
                CanSeeTarget = false;
                AttackThese.Remove(removeUnit);
            }
        }


    }

    void Update()
    {
        if(AttackThis != null && AttackThis.HealthPoints == 0)
        {
            AttackThese.Remove(AttackThese[0]);
            AttackThese.TrimExcess();
        }
        if (AttackThis == null)//When HP reaches 0, object is destroyed which may leave null
        {
            if(AttackThese.Count <= 0)//Check if units in range
            {
                return;
            }
            else //set target to be first element of Enemy list
            {
                AttackThis = AttackThese[0];
            }
        }

        if(AttackThese.Count > 0)
        {
            if (timer <= 0f) 
                timer = AttackDelay;

            if (timer > 0f)
                timer -= Time.deltaTime;

            if (timer <= 0f && CanSeeTarget) 
               Attack();

        }
    }
}
