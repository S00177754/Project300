using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingBuilding : MonoBehaviour
{
    public bool isSelected { get; set; } = false;

    public string BuildingName { get; set; }

    float buildTime;

    public GameObject commandCenter;

    public GameObject barracks;

    public GameObject collector;

    Vector3 spawnLocation;

    private bool isBuilding;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (isSelected)
        {
            if (isBuilding == false)
            {
                if (Input.GetKey(KeyCode.C))
                {
                    buildTime = 10f;
                    WaitForSpawnPoint();
                    Debug.Log("Command Center being built");
                    BuildBuilding(buildTime, commandCenter);
                }

                if (Input.GetKey(KeyCode.B))
                {
                    buildTime = 5f;
                    WaitForSpawnPoint();
                    Debug.Log("Barracks being built");
                    BuildBuilding(buildTime, barracks);
                }
                if (Input.GetKey(KeyCode.R))
                {
                    buildTime = 3f;
                    WaitForSpawnPoint();
                    Debug.Log("Collector being built");
                    BuildBuilding(buildTime, collector);
                }
            }
                
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

        while (!spawnset)
        {
            if (Input.GetMouseButton(0))
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit, 100))
                {
                    if (hit.collider.tag == "Ground")
                    {
                        spawnLocation = hit.point;
                    }
                }

            }
        }
        yield return null;
    }
}
