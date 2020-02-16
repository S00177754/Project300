using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitBuildQueue : MonoBehaviour
{
    public BaseController Base;
    Queue<BarracksScript> Barracks;
    int unitsToBeBuilt = 0;

    private void Start()
    {
        Barracks = new Queue<BarracksScript>();
        Base.UnitBarracks.ForEach(ub => Barracks.Enqueue(ub));
    }

    private void Update()
    {
        foreach (var barracks in Base.UnitBarracks)
        {
            if (!Barracks.Contains(barracks))
                Barracks.Enqueue(barracks);
        }

        if(unitsToBeBuilt > 0)
        {
            for (int i = 0; i < unitsToBeBuilt; i++)
            {
                foreach (var item in Barracks)
                {
                    if (!item.IsTraining)
                    {
                        print("CreatingUnit");
                        item.CreateUnit();
                        unitsToBeBuilt++;
                        break;
                    }
                }
            }
        }
    }

    public void QueueUnit()
    {
        unitsToBeBuilt++;
    }
}
