using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseController : MonoBehaviour
{
    [Header("Commander")]
    public CommanderController Owner;

    [Header("Base Buildings")]
    public BasePowerController PowerBuilding;
    public CommandCenterController CommandHUB;
    public List<CollectorScript> ResourceCollectors;
    public List<BarracksScript> UnitBarracks;
    int queuedUnits;
    public int CreatedUnits = 0;
    public UtilityBuildingScript Builder;
    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(queuedUnits > 0)
        {
            foreach (BarracksScript Barrack in UnitBarracks)
            {
                if(!Barrack.IsTraining)
                {
                    if(Barrack.CreateUnit())
                    {
                        queuedUnits--;
                        if (queuedUnits <= 0)
                            break;
                    }
                }
            }
        }
    }

    public void AddToQueue()
    {
        queuedUnits++;
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

    public int GetQueuedUnitsAmount()
    {
        return queuedUnits;
    }
}
