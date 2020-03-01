using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void UnitsAddToController(UnitDetails details);

public class CommanderController : MonoBehaviour
{
    public string Username;
    public Color PlayerColor;
    public List<UnitDetails> Units;
    public BaseController Base;
    
    public int Resources;
    //public List<BuildingInfo> buildings;

    public event UnitsAddToController UnitsAdded;

    private void Awake()
    {
        
    }

    private void Start()
    {
        
    }

    private void Update()
    {
        
    }

    public void DecreaseFunds(int amount)
    {
        Resources -= amount;
    }

    public void AddUnitDetails(UnitDetails details)
    {
        
        if (UnitsAdded != null)
            UnitsAdded(details);
    }




}
