using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasePowerController : MonoBehaviour
{
    [Header("Health")]
    public int Power;
    public int MaxPower;

    [Header("Base")]
    public BaseController Base;

    private void Start()
    {
        Power = MaxPower;
    }

    public void SetPower(int power, int maxPower)
    {
        Power = power;
        MaxPower = maxPower;
    }

    public void ChangePower(int changeBy)
    {
        Power += changeBy;
    }

    public void FullPower()
    {
        Power = MaxPower;
    }
}
