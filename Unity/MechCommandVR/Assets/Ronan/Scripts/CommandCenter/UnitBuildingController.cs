using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UnitBuildingController : MonoBehaviour
{
    public TMP_Text QueueAmountTMP;
    public BaseController Base;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        QueueAmountTMP.text = $"{Base.GetQueuedUnitsAmount()}";
    }

    public void QueueUnit()
    {
        Base.AddToQueue();
    }
}
