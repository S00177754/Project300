using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class VRDefaultActionSetter : MonoBehaviour
{
    public SteamVR_ActionSet defaultActionSet = null;

    private void Start()
    {
        defaultActionSet.Activate();
    }
}
