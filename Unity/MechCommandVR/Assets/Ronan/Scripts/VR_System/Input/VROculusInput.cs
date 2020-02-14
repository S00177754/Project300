using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VROculusInput : VRInput
{
    /*
    public OVRInput.Controller Controller = OVRInput.Controller.RTrackedRemote;
    public OVRInput.Button ButtonClick = OVRInput.Button.Any;
    */
    public override void Process()
    {
        base.Process();

        /*
        // Press
        if (OVRInput.GetDown(ButtonClick, Controller))
            Press();

        // Release
        if (OVRInput.GetUp(ButtonClick, Controller))
            Release();
        */
    }
}
