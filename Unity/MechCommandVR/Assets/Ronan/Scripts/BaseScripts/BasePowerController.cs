using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasePowerController : MonoBehaviour
{
    [Header("Health")]
    public float Health;
    public int MaxHealth;

    [Header("Base")]
    public BaseController Base;

    private void Start()
    {
        Health = MaxHealth;
    }

    public void SetPower(float power, int maxPower)
    {
        Health = power;
        MaxHealth = maxPower;
    }

    public void ChangePower(int changeBy)
    {
        Health += changeBy;
    }

    public void FullPower()
    {
        Health = MaxHealth;
    }
}
