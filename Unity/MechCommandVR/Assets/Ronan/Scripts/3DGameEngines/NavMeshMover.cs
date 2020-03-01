using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class NavMeshMover : MonoBehaviour
{
    protected NavMeshAgent agent;
    private Vector3 targetPos;

    public virtual void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        if(agent.remainingDistance <= 0)
        {
            UnitAnimWalk(false);
        }
    }

    public virtual void MoveTo(Vector3 position)
    {
        UnitAnimWalk(true);

        Debug.Log("Moving - NavMeshMover");
        if (agent.SetDestination(position))
        {
            targetPos = position;
        }
    }

    public void MoveTo(GameObject gameObject)
    {
        MoveTo(gameObject.transform.position);
    }

    public void Stop()
    {
        agent.isStopped = true;
    }

    private void UnitAnimWalk(bool value)
    {
        UnitAnimationController unitAnimController;
        if (gameObject.TryGetComponent(out unitAnimController))
        {
            unitAnimController.IsWalking = value;
        }
    }

    public Color DebugColor;
    private void OnDrawGizmos()
    {
        if (agent != null)
        {
            for (int i = 0; i < agent.path.corners.Length; i++)
            {
                if (i + 1 < agent.path.corners.Length)
                {
                    Gizmos.color = DebugColor;
                    Gizmos.DrawLine(agent.path.corners[i], agent.path.corners[i + 1]);
                }
            }
        }
    }
}
