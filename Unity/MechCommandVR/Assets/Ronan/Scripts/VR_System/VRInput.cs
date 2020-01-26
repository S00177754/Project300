using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Valve.VR;

public class VRInput : BaseInputModule
{
    public Camera camera;
    public SteamVR_Input_Sources TargetSource;

    [Header("Actions")]
    public SteamVR_Action_Boolean ClickAction;

    [Header("Controller Systems")]
    public VRRadialMenu RadialMenu = null;

    //Pointer System
    private GameObject currentObject = null;
    private PointerEventData Data = null;

    protected override void Awake()
    {
        base.Awake();

        Data = new PointerEventData(eventSystem);
    }

    #region Pointer Relevant Methods
    public PointerEventData GetData()
    {
        return Data;
    }

    private void ProcessPress(PointerEventData data)
    {
        //Set raycast
        Data.pointerPressRaycast = data.pointerCurrentRaycast;

        //check for object hit
        //get down handler
        //call
        GameObject newPointerPress = ExecuteEvents.ExecuteHierarchy(currentObject, Data, ExecuteEvents.pointerDownHandler);

        if(newPointerPress == null)
        {
            newPointerPress = ExecuteEvents.GetEventHandler<IPointerClickHandler>(currentObject);
        }

        //set data
        data.pressPosition = data.position;
        data.pointerPress = newPointerPress;
        data.rawPointerPress = currentObject;

    }

    private void ProcessRelease(PointerEventData data)
    {
        //execute pointerUp event
        ExecuteEvents.Execute(data.pointerPress, data, ExecuteEvents.pointerUpHandler);

        //check for handler
        GameObject pointerUpHandler = ExecuteEvents.GetEventHandler<IPointerClickHandler>(currentObject);

        if(data.pointerPress == pointerUpHandler)
        {
            ExecuteEvents.Execute(data.pointerPress, data, ExecuteEvents.pointerClickHandler); //click check
        }

        eventSystem.SetSelectedGameObject(null);

        //reset
        data.pressPosition = Vector2.zero;
        data.pointerPress = null;
        data.rawPointerPress = null;

    }

    //Acts like update according to documentation
    public override void Process()
    {
        //reset data
        Data.Reset();

        //SetCamera
        Data.position = new Vector2(camera.pixelWidth / 2, camera.pixelHeight / 2);

        //Raycast using eventsystem
        eventSystem.RaycastAll(Data, m_RaycastResultCache);
        Data.pointerCurrentRaycast = FindFirstRaycast(m_RaycastResultCache); //getting first raycast back
        currentObject = Data.pointerCurrentRaycast.gameObject; //getting gameobject from raycast

        //clear ray
        m_RaycastResultCache.Clear();

        //handle hover
        HandlePointerExitAndEnter(Data, currentObject);

        //press
        if (ClickAction.GetStateDown(TargetSource))
        {
            ProcessPress(Data);
        }

        //release
        if (ClickAction.GetStateUp(TargetSource))
        {
            ProcessRelease(Data);
        }
    }

    #endregion


}
