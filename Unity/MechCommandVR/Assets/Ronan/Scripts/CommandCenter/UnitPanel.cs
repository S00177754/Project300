using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class UnitPanel : MonoBehaviour
{
    public RectTransform lstGridUnits;
    public GameObject UnitDetailPrefab;
    public CommanderController Commander;
    public UnitCameraSwitcher unitCamSwitch;

    private void Awake()
    {
       Commander.UnitsAdded += AddToList;
    }

    void Start()
    {
        GenerateList();
    }

    void Update()
    {
        
    }

    private void GenerateList()
    {
        if (Commander != null)
        {
            Debug.Log("GenerateList");
            for (int i = 0; i < Commander.Units.Count; i++)
            {
                GameObject button = Instantiate(UnitDetailPrefab, lstGridUnits);

                UnitControlPanel gridButton = button.GetComponent<UnitControlPanel>();
                gridButton.Initialize(Commander.Units[i],unitCamSwitch);
                Debug.Log("Button created");
            }
        }
    }

    private void AddToList(UnitDetails details)
    {
        //Commander.Units.Add(details);

        GameObject button = Instantiate(UnitDetailPrefab, lstGridUnits);

        UnitControlPanel gridButton = button.GetComponent<UnitControlPanel>();
        gridButton.Initialize(details, unitCamSwitch);
        Debug.Log("Button created");
    }

    private void RemoveFromList(UnitDetails details)
    {
        Destroy(lstGridUnits.GetComponentsInChildren<UnitDetails>().Where(c => c == details).SingleOrDefault().gameObject);
        print("Removed");
    }

}
