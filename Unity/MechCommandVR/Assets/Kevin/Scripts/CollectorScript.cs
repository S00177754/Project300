using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectorScript : MonoBehaviour
{
    public bool isSelected { get; set; } = false;

    private GameObject player;
    public CommanderController commanderController;

 

    public string BuildingName { get; set; }

    private float TimeDelay = 0f;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("PlayerController");
        commanderController = player.GetComponent<CommanderController>();
        Debug.Log("Script found");
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        TimeDelay = TimeDelay + Time.fixedDeltaTime;


        if (TimeDelay > 1)
        {
            Debug.Log("Timer hit");
            commanderController.increaseFunds();
            TimeDelay = 0f;
        }
    }
}
