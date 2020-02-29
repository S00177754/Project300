using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCharacterMover : NavMeshMover
{
    public GameObject PlayerController;

    public override void Start()
    {
        //PlayerController.GetComponent<PlayerMouseInput>().Selected += PlayerCharacterMover_Selected;
        base.Start();
    }

    private void PlayerCharacterMover_Selected(RaycastHit selectionData)
    {

        MoveTo(selectionData.point);
    }

    public override void MoveTo(Vector3 position)
    {
        

        base.MoveTo(position);
    }
}