using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class VRRadialActionSetSwitcher : MonoBehaviour
{
    [Header("Action Sets")]
    public SteamVR_ActionSet defaultActionSet;
    public SteamVR_ActionSet minimapActionSet;

    public void SwitchToDefaultActions()
    {
        defaultActionSet.Activate();
        print("Default Active");
    }

}
