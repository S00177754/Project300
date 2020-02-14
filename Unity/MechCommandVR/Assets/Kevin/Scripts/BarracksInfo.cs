using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarracksInfo : MonoBehaviour
{
    private BuildingInfo info;

    // Start is called before the first frame update

    float trainTime;

    public GameObject unit;

    private bool isTraining = false;

    Vector3 spawnLocation;
    // Start is called before the first frame update
    void Start()
    {
        info = GetComponent<BuildingInfo>();
        spawnLocation = GameObject.Find("Barracks").transform.position - new Vector3(0, 0, 10);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (info.isSelected)
        {

            //CreateUnits();
            if ((Input.GetKey(KeyCode.U)) && (isTraining == false))
            {
                if (info.commanderController.Resources >= 50)
                { 
                    Debug.Log("Unit about to be created");
                    isTraining = true;
                    info.commanderController.decreaseFunds(50);
                    StartCoroutine(TrainUnit(5));
                }
                else
                {
                    Debug.Log("Not enough resources");
                }
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

