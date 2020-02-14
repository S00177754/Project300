using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIPathFollower : MeshMover
{
    public PathNode CurrentNode;

    public override void Start()
    {
        base.Start();
        //MoveToPathNode();
        //Wait for call to start moving
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
