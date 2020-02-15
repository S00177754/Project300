using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void UnitsAddedToController();

public class CommanderController : MonoBehaviour
{
    public string Username;
    public Color PlayerColor;
    public List<UnitDetails> Units;
    public BaseController Base;
    
    public int Resources;
    //public List<BuildingInfo> buildings;

    public event UnitsAddedToController UnitsAdded;

    private void Awake()
    {
        
    }

    private void Start()
    {
        if (UnitsAdded != null)
            UnitsAdded();
    }

    private void Update()
    {
        
    }

    public void DecreaseFunds(int amount)
    {
        Resources -= amount;
    }


}
