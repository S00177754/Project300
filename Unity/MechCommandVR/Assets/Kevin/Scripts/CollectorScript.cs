using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectorScript : MonoBehaviour, ISelectableMinimap
{
    [Header("Base")]
    public BaseController BaseController;


    [Header("Collector Stats")]
    public GameObject MinimapIcon;
    public int ProductionAmount = 1;
    public float CooldownTime = 0f; //Changed to Cooldown Timer since it gives universal meaning
    private float Timer = 0f;
    public bool IsSelected { get; set; } = false;

    private void Start()
    {
        var gameObjectRender = MinimapIcon.GetComponent<Renderer>();
        gameObjectRender.material.SetColor("_Color", BaseController.Owner.PlayerColor);
        BaseController.AddResourceCollector(this);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Timer += Time.deltaTime;

        if (Timer >= CooldownTime)
        {
            IncreaseFunds();
            Timer = 0f;
        }
    }

    public void IncreaseFunds() //Increase funds should be called in update of the object
    {
        BaseController.Owner.Resources += ProductionAmount;
    }


    public void AddBaseReference(BaseController baseController)
    {
        BaseController = baseController;
    }
}
