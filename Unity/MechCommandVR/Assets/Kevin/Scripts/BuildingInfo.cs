using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class BuildingInfo : MonoBehaviour
{
    public bool isSelected { get; set; } = false;

    public string BuildingName { get; set; }

    float trainTime;

    public GameObject unit { get; set; }



    Vector3 spawnLocation;
    // Start is called before the first frame update
    void Start()
    {
        spawnLocation = GameObject.Find("Barracks").transform.position - new Vector3(0, 0, 10);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        while (isSelected)
        {
            Debug.Log("Units good to make");
            CreateUnits();
        }
    }

    public void CreateUnits()
    {
        if (Input.GetKey(KeyCode.U))
        {

            TrainUnit(5);
            
        }
    }

    IEnumerator TrainUnit(int seconds)
    {
        int count = seconds;
        while (count > 0)
        {
            yield return new WaitForSeconds(1);
            count--;
        }
        Instantiate(unit, spawnLocation, Quaternion.identity);
    }
}

