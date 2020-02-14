using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PathNode : MonoBehaviour
{
    public PathNode NextNode;
    public Color DebugColor;

    private void OnDrawGizmos()
    {
        Gizmos.color = DebugColor;
        Gizmos.DrawLine(transform.position, NextNode.transform.position);
    }
}
