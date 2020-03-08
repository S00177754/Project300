using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class UtilityBuildingScript : MonoBehaviour
{
    [Header("Base")]
    public BaseController Base;
    public float BuildingRange = 100f;

    [Header("Unit Barracks Stats")]
    public GameObject UnitBarracksPrefab;
    public Transform UnitSpawnPoint;
    public float BarracksBuildCooldown = 5f;
    public int BarracksBuildCost = 550;

    [Header("Resource Collector Stats")]
    public GameObject ResourceCollectorPrefab;
    public float CollectorBuildCooldown = 3f;
    public int CollectorBuildCost = 300;

    private enum Building { Barracks, Resource }
    private Building BuildMode = Building.Resource;
    private float Timer = 0f;
    private float CooldownTime = 5f;
    private bool IsBuilding = false;

    Vector3 SpawnLocation; //Pass in position via method?

    private void Start()
    {
        UnitBarracksPrefab.GetComponent<BarracksScript>().Base = Base;
        UnitBarracksPrefab.GetComponent<BarracksScript>().UnitSpawnLocation = UnitSpawnPoint;
        ResourceCollectorPrefab.GetComponent<CollectorScript>().BaseController = Base;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        #region Old
        //  if (info.isSelected)
        //  {

        //      if (isBuilding == false)
        //      {

        //*/

        //          if (Input.GetKey(KeyCode.B))
        //          {
        //              Debug.Log("Barracks selected");
        //              buildTime = 5f;
        //              WaitForSpawnPoint();
        //              //Extract logic for map selection
        //              if (IsEnoughResources(300, info.commanderController.Resources))
        //              {
        //                  if (!IsBlocked())
        //                  {


        //                      Debug.Log("Barracks being built");
        //                      info.commanderController.decreaseFunds(300);
        //                      BuildBuilding(buildTime, barracks);
        //                  }
        //              }
        //          }

        //          if (Input.GetKey(KeyCode.R))
        //          {
        //              Debug.Log("Collector selected");
        //              buildTime = 3f;
        //              WaitForSpawnPoint();
        //              if (IsEnoughResources(150, info.commanderController.Resources))
        //              {
        //                  Debug.Log("Collector being built");
        //                  info.commanderController.decreaseFunds(150);
        //                  BuildBuilding(buildTime, collector);
        //              }
        //          }
        //      }

        //  }
        #endregion

        if (IsBuilding)
        {
            Timer += Time.deltaTime;

            if (Timer >= CooldownTime)
            {
                switch (BuildMode)
                {
                    case Building.Barracks:
                        BuildUtility(UnitBarracksPrefab, BarracksBuildCost, SpawnLocation);
                        break;

                    default:
                    case Building.Resource:
                        BuildUtility(ResourceCollectorPrefab, CollectorBuildCost, SpawnLocation);
                        break;
                }

                Timer = 0f;
            }
        }
    }

    public bool BuildUnitBarracks(Vector3 position) //Added to let the UI pass the position in
    {
        if (IsBuilding || !IsEnoughResources(BarracksBuildCost) || IsBlocked(position)) //Add range check in here with an OR
        { return false; }

        BuildMode = Building.Barracks;
        CooldownTime = BarracksBuildCooldown;
        SpawnLocation = position;
        IsBuilding = true;

        return true;
    }

    public bool BuildResourceCollector(Vector3 position)
    {
        if (IsBuilding || !IsEnoughResources(CollectorBuildCost) || IsBlocked(position) || IsInRange(position)) //Add range check in here with an OR
        { return false; }

        BuildMode = Building.Resource;
        CooldownTime = CollectorBuildCooldown;
        SpawnLocation = position;
        IsBuilding = true;

        return true;
    }

    private void BuildUtility(GameObject prefab, int cost, Vector3 position)
    {
        Base.Owner.DecreaseFunds(cost);
        GameObject go = Instantiate(prefab, position, Quaternion.identity);
        go.tag = "Player1";
        IsBuilding = false;
    }

    public bool IsEnoughResources(int RequiredRescources)
    {
        if (RequiredRescources <= Base.Owner.Resources)
        {
            return true;
        }
        else
        {
            Debug.Log("Not enough resources");
            return false;
        }
    }

    public bool IsBlocked(Vector3 position)
    {
        //Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Ray ray = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>().MiniMapCam.ScreenPointToRay(position);

        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            if (hit.collider.gameObject.tag == "Ground")
            {
                RaycastHit sphereHit;

                if (Physics.SphereCast(hit.point, 10f, Vector3.zero, out sphereHit, 10f))
                {
                    if (sphereHit.collider.gameObject.CompareTag("MapInteractable"))
                        return true;
                }
                return false;
            }
        }

        return true;

    }

    public bool IsInRange(Vector3 spawn)
    {
        //float range = 100;
        //Vector3 commandCenter = GameObject.Find("Command Center").transform.position;
        if (Vector3.Distance(spawn, Base.PowerBuilding.transform.position) > BuildingRange)
        {
            return false;
        }
        else
        {
            return true;
        }
    }
        public float Range;
}

    //IEnumerator BuildBuilding(float seconds, GameObject buliding)
    //{
    //    Debug.Log("Building is building");
    //    yield return new WaitForSeconds(seconds);

    //    Instantiate(buliding, spawnLocation, Quaternion.identity);

    //}

    //IEnumerator WaitForSpawnPoint()
    //{
    //    bool spawnset = false;

    //    //Vive rework
    //        if (!Input.GetMouseButton(1))
    //        {
    //            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
    //            RaycastHit hit; 

    //            if (Physics.Raycast(ray, out hit, 100))
    //            {
    //                if (hit.collider.tag == "Ground")
    //                {
    //                    spawnLocation = hit.point;
    //                    Debug.Log("Spawn Point hit");
    //                }
    //                spawnset = true;
    //            }

    //        }
    //    yield return null;
    //}

