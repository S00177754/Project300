using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class VRRadialInput : MonoBehaviour
{
    [Header("Relevant Action Set")]
    public SteamVR_ActionSet actionSet;

    [Header("SteamVR Actions")]
    public SteamVR_Action_Boolean touch = null;
    public SteamVR_Action_Boolean press = null;
    public SteamVR_Action_Vector2 touchPosition = null;

    [Header("Radial Menu")]
    public VRRadialMenu radialMenu = null;

    private bool StateChanged = true;

    void Awake()
    {
        AddEvents();
    }

    void Start()
    {
        //actionSet.Activate();
    }

    private void Update()
    {

    }

    private void OnDestroy()
    {
        RemoveEvents();
    }

    private void AddEvents()
    {
        touch.onChange += Touch;
        press.onStateUp += PressRelease;
        touchPosition.onAxis += Position;
    }

    private void RemoveEvents()
    {
        touch.onChange -= Touch;
        press.onStateUp -= PressRelease;
        touchPosition.onAxis -= Position;
    }

    private void Position(SteamVR_Action_Vector2 fromAction,SteamVR_Input_Sources fromSource, Vector2 axis, Vector2 delta)
    {
        radialMenu.SetTouchPos(axis / 10);
    }

    private void Touch(SteamVR_Action_Boolean fromAction, SteamVR_Input_Sources fromSource, bool newState)
    {
        radialMenu.DisplayMenu(newState);
        radialMenu.SetTouchPos(Vector2.zero);
    }

    private void PressRelease(SteamVR_Action_Boolean fromAction, SteamVR_Input_Sources fromSource)
    {
        radialMenu.ActivateHighlightedSection();
    }
}
