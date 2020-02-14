using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingBuilding : MonoBehaviour
{
    private BuildingInfo info;

    float buildTime;

    public GameObject commandCenter;

    public GameObject barracks;

    public GameObject collector;

    Vector3 spawnLocation;

    private bool isBuilding = false;
    // Start is called before the first frame update
    void Start()
    {
        info = GetComponent<BuildingInfo>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (info.isSelected)
        {
            
            if (isBuilding == false)
            {
              /*  if (Input.GetKey(KeyCode.C))
                {

                    buildTime = 10f;
                    WaitForSpawnPoint();
                    if (IsEnoughResources(500, info.commanderController.Resources))
                    {


                        Debug.Log("Command Center being built");

                        info.commanderController.decreaseFunds(500);
                        BuildBuilding(buildTime, commandCenter);
                 

    */

                if (Input.GetKey(KeyCode.B))
                {
                    Debug.Log("Barracks selected");
                    buildTime = 5f;
                    WaitForSpawnPoint();
                    //Extract logic for map selection
                    if (IsEnoughResources(300, info.commanderController.Resources))
                    {
                        if (!IsBlocked())
                        {


                            Debug.Log("Barracks being built");
                            info.commanderController.decreaseFunds(300);
                            BuildBuilding(buildTime, barracks);
                        }
                    }
                }

                if (Input.GetKey(KeyCode.R))
                {
                    Debug.Log("Collector selected");
                    buildTime = 3f;
                    WaitForSpawnPoint();
                    if (IsEnoughResources(150, info.commanderController.Resources))
                    {
                        Debug.Log("Collector being built");
                        info.commanderController.decreaseFunds(150);
                        BuildBuilding(buildTime, collector);
                    }
                }
            }
                
        }
    }

    static bool IsEnoughResources(int RequiredRescources, int currentResources)
    {
        if (RequiredRescources <= currentResources)
        {
            return true;
        }
        else
        {
            Debug.Log("Not enough resources");
            return false;
        }
    }

    static bool IsBlocked()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;


        if (Physics.SphereCast(ray, 10, out hit))
        {
            return false;
        }
        else
        {
            Debug.Log("Building in the way");
            return true;

        }
    }

    IEnumerator BuildBuilding(float seconds, GameObject buliding)
    {
        Debug.Log("Building is building");
        yield return new WaitForSeconds(seconds);

        Instantiate(buliding, spawnLocation, Quaternion.identity);

    }

    IEnumerator WaitForSpawnPoint()
    {
        bool spawnset = false;

        //Vive rework
            if (!Input.GetMouseButton(1))
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit; 

                if (Physics.Raycast(ray, out hit, 100))
                {
                    if (hit.collider.tag == "Ground")
                    {
                        spawnLocation = hit.point;
                        Debug.Log("Spawn Point hit");
                    }
                    spawnset = true;
                }

            }
        yield return null;
    }
}
