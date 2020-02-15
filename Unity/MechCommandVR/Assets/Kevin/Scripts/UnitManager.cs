using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitManager : MonoBehaviour
{
    RaycastHit hit;
    
    public GameObject selectedUnit;
    private UnitInfo selectedInfo;
    private BuildingInfo buildingInfo;
    //private BuildingBuilding buildingConstruction;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    void FixedUpdate()
    {
        if (Input.GetMouseButton(0))
        {
            Debug.Log("Ray being cast");
            var camRay = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(camRay, out hit))
            {
                Debug.Log("Tags being checked");
                if (hit.transform.CompareTag("PlayerUnit"))
                {
                    selectedUnit = hit.collider.gameObject;
                    selectedInfo = selectedUnit.GetComponent<UnitInfo>();

                    selectedInfo.isSelected = true;
                }
                else if (hit.transform.CompareTag("Barracks"))
                {

                    Debug.Log("Setting selected unit");
                    selectedUnit = hit.collider.gameObject;

                    Debug.Log("Building info being set");
                    buildingInfo = selectedUnit.GetComponent<BuildingInfo>();

                    Debug.Log("Marking building as selected");
                    buildingInfo.isSelected = true;
                }

                else if (hit.transform.CompareTag("CommandCenter"))
                {
                    Debug.Log("Setting selected unit");
                    selectedUnit = hit.collider.gameObject;

                    Debug.Log("Building info being set");
                    //buildingConstruction = selectedUnit.GetComponent<BuildingBuilding>();

                    buildingInfo = selectedUnit.GetComponent<BuildingInfo>();

                    Debug.Log("Marking building as selected");
                    buildingInfo.isSelected = true;
                }

                Debug.Log(hit.transform);
            }

            else if (hit.transform.CompareTag("Ground"))
            {
                selectedUnit = null;
            }
        }
        
    }

}
