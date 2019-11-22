using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class UnitInfo : MonoBehaviour
{

    public bool isSelected { get; set; } = false;

    public bool isEnemy { get; set; }

    public float attackRange { get; set; }

    public float attackSpeed { get; set; }

    public float health { get; set; }

    public string objectName;

    private NavMeshAgent agent;
    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    void FixedUpdate()
    {
        if (Input.GetMouseButtonDown(1) && isSelected)
        {

            MoveUnit();
        }

       /* if (Vector3.Distance(this, )
        {

        }*/
        //Check if there is an enemy in range
        //if, yes attack
    }

    public void MoveUnit()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 100))
        {
            if (hit.collider.tag == "Ground")
            {
                agent.destination = hit.point;
            }
        }
    }

    public void attack()
    {
        //While in range, reduce tareget's health.
        //wait for the length of the attack speed.
        //Check if target is still in range
    }
}
