using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class UnitComponent : MonoBehaviour
{
    public string Name;
    public UnitType myType;
    public decimal HealthPoints;
    public decimal AttackPower;
    public decimal AttackModifier;
    public int Level;

    public enum UnitType
    {
        Rock,
        Paper,
        Scissor,
        Lizard,
        Spock
    }

    public decimal SignModifier(UnitType enemySign)
    {
        switch (myType)
        {
            case UnitType.Rock:
                switch (enemySign)
                {
                    case UnitType.Rock:
                        return 1.0m; //Draw
                    case UnitType.Paper:
                        return 0.75m; //Loose
                    case UnitType.Scissor:
                        return 1.25m; //Win
                    case UnitType.Lizard:
                        return 1.25m; //Win
                    case UnitType.Spock:
                        return 0.75m; //Loose
                    default:
                        return 0;
                }
            case UnitType.Paper:
                switch (enemySign)
                {
                    case UnitType.Rock:
                        return 1.25m; //Win
                    case UnitType.Paper:
                        return 1.0m; //Draw
                    case UnitType.Scissor:
                        return 0.75m; //Loose
                    case UnitType.Lizard:
                        return 0.75m; //Loose
                    case UnitType.Spock:
                        return 1.25m; //Draw
                    default:
                        return 0;
                }
            case UnitType.Scissor:
                switch (enemySign)
                {
                    case UnitType.Rock:
                        return 0.75m; //Loose
                    case UnitType.Paper:
                        return 1.25m; //Win
                    case UnitType.Scissor:
                        return 1.0m; //Draw
                    case UnitType.Lizard:
                        return 1.25m; //Win
                    case UnitType.Spock:
                        return 0.75m; //Loose
                    default:
                        return 0;
                }
            case UnitType.Lizard:
                switch (enemySign)
                {
                    case UnitType.Rock:
                        return 0.75m; //Loose
                    case UnitType.Paper:
                        return 1.25m; //Win
                    case UnitType.Scissor:
                        return 0.75m; //Loose
                    case UnitType.Lizard:
                        return 1.0m; //Draw
                    case UnitType.Spock:
                        return 1.25m; //Win
                    default:
                        return 0;
                }
            case UnitType.Spock:
                switch (enemySign)
                {
                    case UnitType.Rock:
                        return 1.25m; //Win 
                    case UnitType.Paper:
                        return 0.75m; //Loose
                    case UnitType.Scissor:
                        return 1.25m; //Win
                    case UnitType.Lizard:
                        return 0.75m; //Loose
                    case UnitType.Spock:
                        return 1.0m; //Draw
                    default:
                        return 0;
                }
            default:
                return 0;
        }
    }

    public decimal LevelModifier(UnitComponent enemy)
    {
        switch (Level)
        {
            case 1:
                switch (enemy.Level)
                {
                    case 1:
                        return 1;
                    case 2:
                        return 0.85m;
                    case 3:
                        return 0.6m;
                    case 4:
                        return 0.5m;
                    default:
                        return 0.3m;
                }
            case 2:
                switch (enemy.Level)
                {
                    case 1:
                        return 1.15m;
                    case 2:
                        return 1m;
                    case 3:
                        return 0.85m;
                    case 4:
                        return 0.6m;
                    default:
                        return 0.5m;
                }
            case 3:
                switch (enemy.Level)
                {
                    case 1:
                        return 1.3m;
                    case 2:
                        return 1.15m;
                    case 3:
                        return 1m;
                    case 4:
                        return 0.85m;
                    default:
                        return 0.6m;
                }
            case 4:
                switch (enemy.Level)
                {
                    case 1:
                        return 1.5m;
                    case 2:
                        return 1.3m;
                    case 3:
                        return 1.15m;
                    case 4:
                        return 1m;
                    default:
                        return 0.85m;
                }
            case 5:
                switch (enemy.Level)
                {
                    case 1:
                        return 1.6m;
                    case 2:
                        return 1.5m;
                    case 3:
                        return 1.3m;
                    case 4:
                        return 1.15m;
                    default:
                        return 1m;
                }
            default:
                return 1;
        }
    }


    void Start()
    {
        
    }

    void Update()
    {

    }
}
