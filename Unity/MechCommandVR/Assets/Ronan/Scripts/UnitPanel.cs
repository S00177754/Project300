using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class UnitPanel : MonoBehaviour
{
    public RectTransform lstGridUnits;
    public GameObject UnitDetailPrefab;
    public CommanderController Commander;

    private void Awake()
    {
        Commander.UnitsAdded += GenerateList;
    }

    void Start()
    {
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

                UnitDetailButton gridButton = button.GetComponent<UnitDetailButton>();
                gridButton.InitializeUnitButton(Commander.Units[i]);
                Debug.Log("Button created");
            }
        }
    }

}
