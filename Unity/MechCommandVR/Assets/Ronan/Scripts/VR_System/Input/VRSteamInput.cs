using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class VRSteamInput : VRInput
{
    public SteamVR_Input_Sources InputSource = SteamVR_Input_Sources.RightHand;
    public SteamVR_Action_Boolean ButtonClick = null;

    public override void Process()
    {
        base.Process();

        // Press
        if (ButtonClick.GetStateDown(InputSource))
            Press();

        // Release
        if (ButtonClick.GetStateUp(InputSource))
            Release();
    }

}
