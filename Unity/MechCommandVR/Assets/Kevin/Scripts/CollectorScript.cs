using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectorScript : MonoBehaviour
{
    private BuildingInfo info;
    public bool isSelected { get; set; } = false;



 

    public string BuildingName { get; set; }

    private float TimeDelay = 0f;
    // Start is called before the first frame update
    void Start()
    {
        info = GetComponent<BuildingInfo>();
        Debug.Log("Script found");
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        TimeDelay = TimeDelay + Time.fixedDeltaTime;


        if (TimeDelay > 1)
        {
           // Debug.Log("Timer hit");
            info.commanderController.increaseFunds();
            TimeDelay = 0f;
        }
    }
}
