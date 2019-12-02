using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void PlayerSingleSelect(RaycastHit selectionData);

public class PlayerMouseInput : MonoBehaviour
{
    public event PlayerSingleSelect Selected;

    Ray ray;
    RaycastHit hitResult;

    public LayerMask LayerMask;

    void Update()
    {
        //if (Input.GetMouseButton(0))
        //{
        //    ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        //    if (Physics.Raycast(ray, out hitResult, Mathf.Infinity, LayerMask))
        //    {
        //        if (Selected != null)
        //            Selected(hitResult);
        //    }
        //}
    }
}
