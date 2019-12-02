using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum UnitType
{
    Rock,
    Paper,
    Scissor,
    Lizard,
    Spock
}

public class AttackComponent : MonoBehaviour
{
    public UnitDetails Me;
    public UnitDetails AttackThis;
    string AttackTag;
    public float AttackDelay;
    float timer = 0f;
    //multiple tagets
    //[HideInInspector]
    public List<UnitDetails> AttackThese;
    Ray ray;

    public bool CanSeeTarget;

    void Start()
    {
        Me = gameObject.GetComponent<UnitDetails>();
        AttackThese = new List<UnitDetails>();
        //Using Player1 and Player2 for future implementation of multiplayer
        if (gameObject.tag == "Player1")
            AttackTag = "Player2";
        if (gameObject.tag == "Player2")
            AttackTag = "Player1";
    }

    
    void Attack()
    {
        AttackThis = AttackThese[0];
        if(Me != null && AttackThis != null)
        {
            Me.AttackModifier = SignModifier(AttackThis.myType) * LevelModifier(AttackThis);
            if(Time.deltaTime <= AttackDelay)
            {
                AttackThis.Health -= Me.AttackPower * Me.AttackModifier;
                //Reset timer
                timer = 0f;
                Debug.Log(Me.ToString());
                //Debug.Log(AttackThis.ToString());
            }
        }
        if (AttackThis == null)
            CanSeeTarget = false;
    }

    private void OnTriggerEnter(Collider collision)
    {
        UnitDetails newUnit = collision.gameObject.GetComponent<UnitDetails>();
        if (newUnit != null)
        {
            if (collision.gameObject.tag.Equals(AttackTag))
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

        //check reverse trigger is with unittype
        UnitDetails removeUnit = collision.gameObject.GetComponent<UnitDetails>();
        if (removeUnit != null)
        {
            if (collision.gameObject.tag.Equals(AttackTag))
            {
                AttackThese.Remove(removeUnit);
                if(AttackThese.Count == 0)//Check if enemies still in range
                    CanSeeTarget = false;
            }
        }


    }

    void Update()
    {
        //
        if (AttackThis != null && AttackThis.Health <= 0)
        {
            AttackThese.Remove(AttackThis);
            Destroy(AttackThis.gameObject);
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

    public float SignModifier(UnitType enemySign)
    {
        switch (Me.myType)
        {
            case UnitType.Rock:
                switch (enemySign)
                {
                    case UnitType.Rock:
                        return 1.0f; //Draw
                    case UnitType.Paper:
                        return 0.75f; //Loose
                    case UnitType.Scissor:
                        return 1.25f; //Win
                    case UnitType.Lizard:
                        return 1.25f; //Win
                    case UnitType.Spock:
                        return 0.75f; //Loose
                    default:
                        return 0;
                }
            case UnitType.Paper:
                switch (enemySign)
                {
                    case UnitType.Rock:
                        return 1.25f; //Win
                    case UnitType.Paper:
                        return 1.0f; //Draw
                    case UnitType.Scissor:
                        return 0.75f; //Loose
                    case UnitType.Lizard:
                        return 0.75f; //Loose
                    case UnitType.Spock:
                        return 1.25f; //Draw
                    default:
                        return 0;
                }
            case UnitType.Scissor:
                switch (enemySign)
                {
                    case UnitType.Rock:
                        return 0.75f; //Loose
                    case UnitType.Paper:
                        return 1.25f; //Win
                    case UnitType.Scissor:
                        return 1.0f; //Draw
                    case UnitType.Lizard:
                        return 1.25f; //Win
                    case UnitType.Spock:
                        return 0.75f; //Loose
                    default:
                        return 0;
                }
            case UnitType.Lizard:
                switch (enemySign)
                {
                    case UnitType.Rock:
                        return 0.75f; //Loose
                    case UnitType.Paper:
                        return 1.25f; //Win
                    case UnitType.Scissor:
                        return 0.75f; //Loose
                    case UnitType.Lizard:
                        return 1.0f; //Draw
                    case UnitType.Spock:
                        return 1.25f; //Win
                    default:
                        return 0;
                }
            case UnitType.Spock:
                switch (enemySign)
                {
                    case UnitType.Rock:
                        return 1.25f; //Win 
                    case UnitType.Paper:
                        return 0.75f; //Loose
                    case UnitType.Scissor:
                        return 1.25f; //Win
                    case UnitType.Lizard:
                        return 0.75f; //Loose
                    case UnitType.Spock:
                        return 1.0f; //Draw
                    default:
                        return 0;
                }
            default:
                return 0;
        }
    }

    public float LevelModifier(UnitDetails enemy)
    {
        switch (Me.Level)
        {
            case 1:
                switch (enemy.Level)
                {
                    case 1:
                        return 1;
                    case 2:
                        return 0.85f;
                    case 3:
                        return 0.6f;
                    case 4:
                        return 0.5f;
                    default:
                        return 0.3f;
                }
            case 2:
                switch (enemy.Level)
                {
                    case 1:
                        return 1.15f;
                    case 2:
                        return 1f;
                    case 3:
                        return 0.85f;
                    case 4:
                        return 0.6f;
                    default:
                        return 0.5f;
                }
            case 3:
                switch (enemy.Level)
                {
                    case 1:
                        return 1.3f;
                    case 2:
                        return 1.15f;
                    case 3:
                        return 1f;
                    case 4:
                        return 0.85f;
                    default:
                        return 0.6f;
                }
            case 4:
                switch (enemy.Level)
                {
                    case 1:
                        return 1.5f;
                    case 2:
                        return 1.3f;
                    case 3:
                        return 1.15f;
                    case 4:
                        return 1f;
                    default:
                        return 0.85f;
                }
            case 5:
                switch (enemy.Level)
                {
                    case 1:
                        return 1.6f;
                    case 2:
                        return 1.5f;
                    case 3:
                        return 1.3f;
                    case 4:
                        return 1.15f;
                    default:
                        return 1f;
                }
            default:
                return 1;
        }
    }

}
