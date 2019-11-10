using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class UnitPanel : MonoBehaviour
{
    public RectTransform lstGridUnits;
    public GameObject UnitDetailPrefab;
    public CommanderController Commander;

    public int PlayerNumber;

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
