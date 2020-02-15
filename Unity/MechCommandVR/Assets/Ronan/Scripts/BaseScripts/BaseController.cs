using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseController : MonoBehaviour
{
    [Header("Commander")]
    public CommanderController Owner;

    [Header("Base Buildings")]
    public BasePowerController PowerBuilding;
    public List<CollectorScript> ResourceCollectors;
    public List<BarracksScript> UnitBarracks;

    public List<BuildingInfo> buildings;
    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddResourceCollector(CollectorScript collector)
    {
        collector.AddBaseReference(this);
        ResourceCollectors.Add(collector);
    }

    public void AddUnitBarracks(BarracksScript barracks)
    {
        barracks.AddBaseReference(this);
        UnitBarracks.Add(barracks);
    }
}
