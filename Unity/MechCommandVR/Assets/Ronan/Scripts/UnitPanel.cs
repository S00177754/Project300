using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitPanel : MonoBehaviour
{
    public RectTransform lstGridUnits;
    public GameObject UnitDetailPrefab;
    public List<int> unitIdList;
    public int amount;

    void Start()
    {
        LoadList();
    }

    void Update()
    {
        
    }

    public void LoadList()
    {
        if(unitIdList != null)
        {
            for (int i = 0; i < amount; i++)
            {
                GameObject button = Instantiate(UnitDetailPrefab, lstGridUnits);

                UnitDetailButton gridButton = button.GetComponent<UnitDetailButton>();
                gridButton.InitializeUnitButton();
                Debug.Log("Button created");
            }
        }
    }




            //gridButton.Selected += GridButton_OnSelected;
            //gridButton.Clicked += GridButton_OnClicked;
        
    
}
