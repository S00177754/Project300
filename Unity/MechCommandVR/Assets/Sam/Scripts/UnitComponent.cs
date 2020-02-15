using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitComponent : MonoBehaviour
{
    public UnitDetails details;

    public float SignModifier(UnitType enemySign)
    {
        switch (details.myType)
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

    public float LevelModifier(UnitComponent enemy)
    {
        switch (details.Level)
        {
            case 1:
                switch (enemy.details.Level)
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
                switch (enemy.details.Level)
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
                switch (enemy.details.Level)
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
                switch (enemy.details.Level)
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
                switch (enemy.details.Level)
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


    void Start()
    {
        details = gameObject.GetComponent<UnitDetails>();
    }


    public override string ToString()
    {
        return String.Format(details.Name + "\tHP:\t{0}\t\tModifier:\t{1}", details.Health, details.AttackModifier);
    }
}
