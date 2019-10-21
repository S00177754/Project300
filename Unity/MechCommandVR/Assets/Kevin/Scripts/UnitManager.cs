using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitManager : MonoBehaviour
{
    RaycastHit hit;
    //List<MovementController> selectedUnits = new List<MovementController>();
    public GameObject selectedUnit;
    private UnitInfo selectedInfo;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
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

                    //SelectUnit(hit.transform.GetComponent<MovementController>());
                }
                Debug.Log(hit.transform);
            }

            else if (hit.transform.CompareTag("Ground"))
            {
                selectedUnit = null;
            }
        }
        
    }

  /*  private void SelectUnit(MovementController unit)
    {
        //selectedUnits.Clear();
        selectedUnits.Add(unit);
        
        //unit.Find("Highlight").gameObject.SetActive(true);
    }

    private void DeselectUnits()
    {
        for (int i = 0; i < selectedUnits.Count; i++)
        {
            //selectedUnits[i].Find("Hignlight").gameObject.SetActive(false);
        }
    }*/
}
