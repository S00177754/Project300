using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarracksScript : MonoBehaviour, ISelectableMinimap
{
    [Header("Commander")]
    public BaseController Base;

    [Header("Barracks Stats")]
    public GameObject MinimapIcon;
    public GameObject UnitPrefab;
    public Transform UnitSpawnLocation;
    public int UnitCost = 50;
    public float CooldownTime = 5; //Changed to Cooldown Timer since it gives universal meaning
    private float Timer = 0f;
    public bool IsTraining = false;
    public bool IsSelected { get; set; }

    private void Start()
    {
        var gameObjectRender = MinimapIcon.GetComponent<Renderer>();
        gameObjectRender.material.SetColor("_Color", Base.Owner.PlayerColor);
        Base.AddUnitBarracks(this);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        #region Old
        //if (info.isSelected)
        //{

        //    ////CreateUnits();
        //    //if ((Input.GetKey(KeyCode.U)) && (isTraining == false))
        //    //{
        //    //    if (info.commanderController.Resources >= 50)
        //    //    { 
        //    //        Debug.Log("Unit about to be created");
        //    //        isTraining = true;
        //    //        Commander.DecreaseFunds(50);
        //    //        StartCoroutine(TrainUnit(5)); //should use a variable instead of magic number for values, write so level designer can easily adjust values
        //    //    }
        //    //    else
        //    //    {
        //    //        Debug.Log("Not enough resources");
        //    //    }
        //    //}
        #endregion

        if (IsTraining)
        {
            Timer += Time.deltaTime;

            if (Timer >= CooldownTime && Base.Owner.Resources >= UnitCost)
            {
                Base.Owner.DecreaseFunds(UnitCost);
                TrainUnit();
                Timer = 0f;
            }
        }
    }

    /// <summary>
    /// Returns true if unit is created. Use value to know if barracks is available
    /// </summary>
    /// <returns></returns>
    public bool CreateUnit() //Method can be called from the UI to activate it
    {
        if (IsTraining || Base.Owner.Resources < UnitCost)
            return false;
        else
        {
            IsTraining = true;
            return true;
        }
    }


    private void TrainUnit()
    {
        Base.CreatedUnits++;

        GameObject unit = Instantiate(UnitPrefab, UnitSpawnLocation.position, Quaternion.identity);
        UnitDetails details = unit.GetComponent<UnitDetails>();

        details.SetDetails(Base.Owner, Base.CreatedUnits, 1f, 0.25f, 1f);
        Base.Owner.AddUnitDetails(details);

        IsTraining = false;
    }

    public void AddBaseReference(BaseController baseController)
    {
        Base = baseController;
    }

    //IEnumerator TrainUnit(float seconds) //No need for enumerator due to update method
    //{
    //    Debug.Log("Unit being created");
    //    yield return new WaitForSeconds(seconds);


    //    Instantiate(unit, spawnLocation, Quaternion.identity);
    //    isTraining = false;
    //}
}

