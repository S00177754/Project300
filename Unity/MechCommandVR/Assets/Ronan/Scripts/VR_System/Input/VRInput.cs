using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Valve.VR;

// https://docs.unity3d.com/2018.3/Documentation/ScriptReference/EventSystems.BaseInputModule.html Base Input Module Unity Documentation

public class VRInput : BaseInputModule
{
    //Pointer System
    [SerializeField] private VRPointer pointer = null;
    public PointerEventData Data { get; private set; } = null;

    protected override void Start()
    {
        Data = new PointerEventData(eventSystem);
        Data.position = new Vector2(pointer.Camera.pixelWidth / 2, pointer.Camera.pixelHeight / 2);
    }

    protected void Press()
    {
        Data.pointerPressRaycast = Data.pointerCurrentRaycast;
        Data.pointerPress = ExecuteEvents.GetEventHandler<IPointerClickHandler>(Data.pointerPressRaycast.gameObject);
        Data.pointerDrag = ExecuteEvents.GetEventHandler<IDragHandler>(Data.pointerPressRaycast.gameObject);
        
        ExecuteEvents.Execute(Data.pointerPress, Data, ExecuteEvents.pointerDownHandler);
        ExecuteEvents.Execute(Data.pointerDrag, Data, ExecuteEvents.beginDragHandler);
    }

    protected void Release()
    {
        //Get pointer object
        GameObject Pointer_Release = ExecuteEvents.GetEventHandler<IPointerClickHandler>(Data.pointerCurrentRaycast.gameObject);

        if (Data.pointerPress == Pointer_Release)
            ExecuteEvents.Execute(Data.pointerPress, Data, ExecuteEvents.pointerClickHandler);

        ExecuteEvents.Execute(Data.pointerPress, Data, ExecuteEvents.pointerUpHandler);
        ExecuteEvents.Execute(Data.pointerDrag, Data, ExecuteEvents.endDragHandler);

        //Clear the data
        Data.pointerPress = null;
        Data.pointerDrag = null;
        Data.pointerCurrentRaycast.Clear();
    }

    //Acts like update according to documentation
    public override void Process()
    {
        Data.Reset();
        Data.position = new Vector2(pointer.Camera.pixelWidth / 2, pointer.Camera.pixelHeight / 2);

        eventSystem.RaycastAll(Data, m_RaycastResultCache);
        Data.pointerCurrentRaycast = FindFirstRaycast(m_RaycastResultCache);

        HandlePointerExitAndEnter(Data, Data.pointerCurrentRaycast.gameObject);
        ExecuteEvents.Execute(Data.pointerDrag, Data, ExecuteEvents.dragHandler);
    }
}
