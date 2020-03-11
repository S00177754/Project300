using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class UnitDetails : MonoBehaviour
{

    public CommanderController Commander;

    AudioManager SoundManager;
    AudioSource destroySource;
    AudioClip destroyClip;

    [Header("Unit Info")]
    public int UnitId;
    public string Name;

    public UnitType myType;
    UnitState unitState;
    public int Level;

    [Header("Unit Stats")]
    public float MaxHealth = 1;
    public float Health = 1;

    public float AttackPower = 0.1f;
    public float AttackModifier = 1;

    public float DespawnTime = 3f;
    private float despawnTimer = 0f;

    [Header("Unit States")]
    public bool IsSelected;
    public bool IsControlled;

    [Header("Icons")]
    public GameObject MinimapIcon;
    public GameObject HighlightedIcon;

    void Start()
    {
        var gameObjectRender = MinimapIcon.GetComponent<Renderer>();
        gameObjectRender.material.SetColor("_" +
            "Color", Commander.PlayerColor);

        IsSelected = false;
        IsControlled = false;
        Health = MaxHealth;
        unitState = UnitState.IDLE;

        Commander.Units.Add(this);
        Debug.Log("Unit " + Name + " Added");

        SoundManager = GetComponentInParent<AudioManager>();
        destroySource = SoundManager.DestreoySource;
        destroyClip = destroySource.clip;
    }

    private void Update()
    {
        DeathCheck();

        HighlightedIcon.SetActive(IsSelected);

    }

    public void SetDetails(CommanderController commander, int unitId, float maxHealth, float attackPwr, float attackMod)
    {
        Commander = commander;
        UnitId = unitId;
        MaxHealth = maxHealth;
        AttackPower = attackPwr;
        AttackModifier = attackMod;
        Name = "Phantom_" + Random.Range(1, 1000);
        myType = (UnitType)Random.Range(0, 5);

        if (Commander.Base.CommandHUB != null)
        {
            Commander.Base.CommandHUB.UniCamSwitch.notificationController.SendNotification(Name + " has been created!", NotificationSprites.Build, Color.cyan);
        }

    }


    private void DeathCheck()
    {
        if (Health <= 0)
        {
            //Play deatch clip

            despawnTimer += Time.deltaTime;
            UnitAnimationController anim = GetUnitAnimationController();
            if (anim != null)
            {
                anim.Death();
            }

        }

        if (despawnTimer >= DespawnTime)
        {
            Destroy(gameObject);

            if (Commander.Base.CommandHUB != null)
            {
                Commander.Base.CommandHUB.UniCamSwitch.notificationController.UnitDestroyed(Name);
            }

        }

    }

    private UnitAnimationController GetUnitAnimationController()
    {
        UnitAnimationController unitAnimController;
        if (gameObject.TryGetComponent(out unitAnimController))
        {
            return unitAnimController;
        }
        return null;
    }

}
