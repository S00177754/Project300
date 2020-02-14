using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(MeshMover))]
public class MeshMover : MonoBehaviour
{
    protected NavMeshAgent agent;


    public virtual void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    public void MoveTo(Vector3 position)
    {
        agent.SetDestination(position);
    }

    public void MoveTo(GameObject gameObject)
    {
        MoveTo(gameObject.transform.position);
    }

    public void Stop()
    {
        agent.isStopped = true;
    }



}
