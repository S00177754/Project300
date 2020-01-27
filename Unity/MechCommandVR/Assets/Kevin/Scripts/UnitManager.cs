using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitManager : MonoBehaviour
{
    RaycastHit hit;
    
    public GameObject selectedUnit;
    private UnitInfo selectedInfo;
    public BuildingInfo buildingInfo { get; set; }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    void FixedUpdate()
    {
        if (Input.GetMouseButton(0))
        {
            var camRay = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(camRay, out hit))
            {
                if (hit.transform.CompareTag("PlayerUnit"))
                {
                    selectedUnit = hit.collider.gameObject;
                    selectedInfo = selectedUnit.GetComponent<UnitInfo>();

                    selectedInfo.isSelected = true;
                }
                else if (hit.transform.CompareTag("Building"))
                {
                    selectedUnit = hit.collider.gameObject;
                    buildingInfo = selectedUnit.GetComponent<BuildingInfo>();

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
