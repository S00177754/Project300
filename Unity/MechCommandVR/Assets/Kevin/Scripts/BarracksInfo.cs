using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarracksInfo : MonoBehaviour
{
    public bool isSelected { get; set; } = false;

    public string BuildingName { get; set; }

    private GameObject player;
    public CommanderController commanderController;
    // Start is called before the first frame update

    float trainTime;

    public GameObject unit;

    private bool isTraining = false;

    Vector3 spawnLocation;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("PlayerController");
        commanderController = player.GetComponent<CommanderController>();
        spawnLocation = GameObject.Find("Barracks").transform.position - new Vector3(0, 0, 10);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (isSelected)
        {
            //CreateUnits();
            if ((Input.GetKey(KeyCode.U)) && (isTraining == false) && (commanderController.Resources >= 50))
            {
                Debug.Log("Unit about to be created");
                isTraining = true;
                commanderController.decreaseFunds(50);
                StartCoroutine(TrainUnit(5));

            }
        }
    }

    public void CreateUnits()
    {

    }

    IEnumerator TrainUnit(float seconds)
    {
        Debug.Log("Unit being created");
        yield return new WaitForSeconds(seconds);


        Instantiate(unit, spawnLocation, Quaternion.identity);
        isTraining = false;
    }
}

