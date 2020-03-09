using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIPathFollower : MeshMover
{
    public PathNode CurrentNode;
    public AttackComponent attackComponent;

    public override void Start()
    {
        base.Start();
        //MoveToPathNode();
        //Wait for call to start moving
    }

    private void Update()
    {
        if (attackComponent.CanAttackTarget)
        {
            MoveTo(transform.position);
        }

        if (CurrentNode != null)
        if(!attackComponent.CanAttackTarget && agent.nextPosition != CurrentNode.transform.position)
        {
            MoveTo(CurrentNode.transform.position);
        }
    }

    void MoveToPathNode()
    {
        if (CurrentNode != null)
            MoveTo(CurrentNode.gameObject.transform.position);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Node"))
        {
            CurrentNode = CurrentNode.NextNode;
            MoveToPathNode();
        }
    }
}
