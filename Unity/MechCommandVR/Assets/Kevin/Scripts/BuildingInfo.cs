using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class BuildingInfo : MonoBehaviour
{
    public bool isSelected { get; set; } = false;

    public string BuildingName { get; set; }

    float trainTime;

    public GameObject unit;

    private bool isTraining = false;

    Vector3 spawnLocation;
    // Start is called before the first frame update
    void Start()
    {
        spawnLocation = GameObject.Find("Barracks").transform.position - new Vector3(0, 0, 10);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (isSelected)
        {
            //CreateUnits();
            if ((Input.GetKey(KeyCode.U)) && (isTraining == false))
            {
                Debug.Log("Unit about to be created");
                isTraining = true;
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

