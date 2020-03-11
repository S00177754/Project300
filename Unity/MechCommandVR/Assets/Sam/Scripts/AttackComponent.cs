using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum UnitType
{
    Rock,
    Paper,
    Scissor,
    Lizard,
    Spock
}

public class AttackComponent : MonoBehaviour
{

    public static float[,] SignModifierArray = new float[5, 5]
    {
            { 1.0f,     1.25f,  0.85f,  0.85f,  1.25f },
            { 0.85f,    1.0f,   1.25f,  1.25f,  0.85f },
            { 1.25f,    0.85f,  1.0f,   0.85f,  1.25f },
            { 1.25f,    0.85f,  1.25f,  1.0f,   0.85f },
            { 0.85f,    1.25f,  0.85f,  1.25f,  1.0f  }
    };

    public static float[,] LevelModifierArray = new float[5, 5]
    {
            { 1.0f,     1.15f,  1.3f,   1.5f,   1.6f  },
            { 0.85f,    1.0f,   1.15f,  1.3f,   1.5f  },
            { 0.6f,     0.85f,  1.0f,   1.15f,  1.3f  },
            { 0.5f,     0.6f,   0.85f,  1.0f,   1.15f },
            { 0.3f,     0.5f,   0.6f,   0.85f,  1.0f  }
    };

    NavMeshMover meshMover;
    public UnitComponent Me;
    public GameObject AttackThis;
    UnitComponent AttackThisComp;
    string AttackTag = "Unit";
    [SerializeField]
    float AttackDelay;
    float timer;
    //multiple targets
    public List<GameObject> AttackThese;

    AudioManager SoundsManager;
    public AudioClip AttackClip;
    public AudioSource AttackSource;

    public bool CanSeeTarget;
    public bool CanAttackTarget;
    [SerializeField]
    float AttackDistance;

    bool IsAttacking = false;

    void Start()
    {
        SoundsManager = GetComponentInParent<AudioManager>();
        AttackSource = SoundsManager.AttackSource;
        Me = gameObject.GetComponent<UnitComponent>();
        AttackThese = new List<GameObject>();
        //AttackDistance = 2f;
        //Using Player1 and Player2 for future implementation of multiplayer
        if (gameObject.CompareTag("Player1"))
            AttackTag = "Player2";
        if (gameObject.CompareTag("Player2"))
            AttackTag = "Player1";

        meshMover = gameObject.GetComponent<NavMeshMover>();
    }

    public void Move()
    {
        if (AttackThis != null)
        {
            if (meshMover != null && Vector3.Distance(transform.position, AttackThis.transform.position) <= AttackDistance)
                meshMover.MoveTo(AttackThis.transform.position);
        }
    }

    #region Attack
    void Attack()
    {

        Debug.Log("Attack called");

        AttackSource.pitch = Random.Range(0.75f, 1.251f);
        AttackClip = AttackSource.clip;
        if (AttackSource.TryGetComponent(out AttackClip))
            AttackSource.PlayOneShot(AttackClip);

        if (AttackThese.Count > 0)
            AttackThis = AttackThese[0];
        else
            AttackThis = null;

        UnitComponent attackUnit;
        BasePowerController attackBase;

        if (Me != null && AttackThis != null)
        {
            gameObject.transform.LookAt(AttackThis.transform);
            UnitAnimSet((UnitAnimationTriggers)Random.Range(2, 4));

            if (AttackThis.TryGetComponent<UnitComponent>(out attackUnit))
            {
                Me.details.AttackModifier = SignModifier(attackUnit.details.myType) * LevelModifier(attackUnit);


                attackUnit.details.Health -= Me.details.AttackPower * Me.details.AttackModifier;
                attackUnit.gameObject.GetComponent<AttackComponent>().UnitAnimSet(UnitAnimationTriggers.Damaged);
                timer = 0f;
                Debug.Log(Me.ToString());
                Debug.Log(AttackThis.ToString());
            }
            else if (AttackThis.TryGetComponent<BasePowerController>(out attackBase))
            {
                attackBase.Health -= Me.details.AttackPower * Me.details.AttackModifier;
            }
        }

        if (AttackThis == null)
        {
            IsAttacking = false;
            CanSeeTarget = false;
        }
    }
    #endregion

    private bool GetHealth(GameObject AttackingThis)
    {
        if (AttackingThis != null)
        {
            UnitComponent attackUnit;
            BasePowerController attackbase;
            if (AttackingThis.TryGetComponent<UnitComponent>(out attackUnit))
            {
                return attackUnit.details.Health <= 0;
            }
            else if (AttackingThis.TryGetComponent<BasePowerController>(out attackbase))
            {
                return attackbase.Health <= 0;
            }
        }
        return false;
    }

    void Update()
    {
        //Check if in range of attack
        if (AttackThis != null && Vector3.Distance(transform.position, AttackThis.transform.position) <= AttackDistance)
            CanAttackTarget = true;
        else
            CanAttackTarget = false;

        //Removes enemy from list if enemy 'dies'
        if (AttackThis != null && GetHealth(AttackThis))
        {
            AttackThese.Remove(AttackThis);


            //Need to call a death method instead
            //Destroy(AttackThis.gameObject);
            //Handled in unit details





            if (AttackThese.Count <= 0)
                CanSeeTarget = false;
        }
        if (AttackThis == null)//When HP reaches 0, object is destroyed which will leave null
        {
            if (AttackThese.Count <= 0)//Check if units in range
            {

                return;
            }
            else //set target to be first element of Enemy list
                AttackThis = AttackThese[0];
        }

        //Timer
        if (CanAttackTarget)
        {
            if (AttackThese.Count > 0)
            {
                if (timer > 0f)
                    timer -= Time.deltaTime;

                if (timer <= 0f) //&& CanAttackTarget)
                {
                    Attack();
                    timer = AttackDelay;
                }
            }
        }
    }

    #region Trigger Methods: Enter and Exit
    //Trigger Methods
    private void OnTriggerEnter(Collider collision)
    {
        //Debug.Log("OnTriggerEnter");
        GameObject newUnit = collision.gameObject;
        if (newUnit != null)
        {
            //Debug.Log("newUnit not null");
            if (collision.gameObject.tag != null)
                if (collision.gameObject.CompareTag(AttackTag))
                {
                    CanSeeTarget = true;
                    AttackThese.Add(newUnit);
                }

        }
        else
            CanSeeTarget = false;
    }

    //This method removes a 'still alive' enemy when it is out of sight range
    private void OnTriggerExit(Collider collision)
    {
        //Check if player or enemy
        if (collision.gameObject.CompareTag(AttackTag))
        {   //Comparing AttackTag ensures unit details are correct since Srat() will determine which tag to attack
            GameObject removeUnit = collision.gameObject;
            AttackThese.Remove(removeUnit);

            if (AttackThese.Count == 0)//Check if enemies still in range
            {
                CanSeeTarget = false;
                AttackThis = null;
            }
            else if (AttackThese.Count > 0)
                AttackThis = AttackThese[0];
        }
        //check reverse trigger is with unittype
    }

    private void OnCollisionEnter(Collision collision)
    {
        Projectile projectileComp;
        if (collision.gameObject.TryGetComponent<Projectile>(out projectileComp))
        {
            Me.details.Health -= projectileComp.Damage;
            Destroy(projectileComp.gameObject);
        }
    }

    #endregion

    #region Modifier Methods
    public float SignModifier(UnitType enemySign)
    {
        return SignModifierArray[(int)Me.details.myType, (int)enemySign];

        #region Old Modifier code
        /*
        switch (Me.details.myType)
        {
            case UnitType.Rock:
                switch (enemySign)
                {
                    case UnitType.Rock:
                        return 1.0f; //Draw
                    case UnitType.Paper:
                        return 0.75f; //Loose
                    case UnitType.Scissor:
                        return 1.25f; //Win
                    case UnitType.Lizard:
                        return 1.25f; //Win
                    case UnitType.Spock:
                        return 0.75f; //Loose
                    default:
                        return 0;
                }
            case UnitType.Paper:
                switch (enemySign)
                {
                    case UnitType.Rock:
                        return 1.25f; //Win
                    case UnitType.Paper:
                        return 1.0f; //Draw
                    case UnitType.Scissor:
                        return 0.75f; //Loose
                    case UnitType.Lizard:
                        return 0.75f; //Loose
                    case UnitType.Spock:
                        return 1.25f; //Draw
                    default:
                        return 0;
                }
            case UnitType.Scissor:
                switch (enemySign)
                {
                    case UnitType.Rock:
                        return 0.75f; //Loose
                    case UnitType.Paper:
                        return 1.25f; //Win
                    case UnitType.Scissor:
                        return 1.0f; //Draw
                    case UnitType.Lizard:
                        return 1.25f; //Win
                    case UnitType.Spock:
                        return 0.75f; //Loose
                    default:
                        return 0;
                }
            case UnitType.Lizard:
                switch (enemySign)
                {
                    case UnitType.Rock:
                        return 0.75f; //Loose
                    case UnitType.Paper:
                        return 1.25f; //Win
                    case UnitType.Scissor:
                        return 0.75f; //Loose
                    case UnitType.Lizard:
                        return 1.0f; //Draw
                    case UnitType.Spock:
                        return 1.25f; //Win
                    default:
                        return 0;
                }
            case UnitType.Spock:
                switch (enemySign)
                {
                    case UnitType.Rock:
                        return 1.25f; //Win 
                    case UnitType.Paper:
                        return 0.75f; //Loose
                    case UnitType.Scissor:
                        return 1.25f; //Win
                    case UnitType.Lizard:
                        return 0.75f; //Loose
                    case UnitType.Spock:
                        return 1.0f; //Draw
                    default:
                        return 0;
                }
            default:
                return 0;
                }
                */
        #endregion
    }

    public float LevelModifier(UnitComponent enemy)
    {
        return LevelModifierArray[Me.details.Level - 1, enemy.details.Level - 1];

        #region Old Code
        /*
        switch (Me.details.Level)
        {
            case 1:
                switch (enemy.details.Level)
                {
                    case 1:
                        return 1;
                    case 2:
                        return 0.85f;
                    case 3:
                        return 0.6f;
                    case 4:
                        return 0.5f;
                    default:
                        return 0.3f;
                }
            case 2:
                switch (enemy.details.Level)
                {
                    case 1:
                        return 1.15f;
                    case 2:
                        return 1f;
                    case 3:
                        return 0.85f;
                    case 4:
                        return 0.6f;
                    default:
                        return 0.5f;
                }
            case 3:
                switch (enemy.details.Level)
                {
                    case 1:
                        return 1.3f;
                    case 2:
                        return 1.15f;
                    case 3:
                        return 1f;
                    case 4:
                        return 0.85f;
                    default:
                        return 0.6f;
                }
            case 4:
                switch (enemy.details.Level)
                {
                    case 1:
                        return 1.5f;
                    case 2:
                        return 1.3f;
                    case 3:
                        return 1.15f;
                    case 4:
                        return 1f;
                    default:
                        return 0.85f;
                }
            case 5:
                switch (enemy.details.Level)
                {
                    case 1:
                        return 1.6f;
                    case 2:
                        return 1.5f;
                    case 3:
                        return 1.3f;
                    case 4:
                        return 1.15f;
                    default:
                        return 1f;
                }
            default:
                return 1;
        }
        */
        #endregion
    }
    #endregion

    public void UnitAnimSet(UnitAnimationTriggers state)
    {
        UnitAnimationController unitAnimController;
        if (gameObject.TryGetComponent(out unitAnimController))
        {
            unitAnimController.IsWalking = false;
            unitAnimController.SetTrigger(state);
        }
    }



}





