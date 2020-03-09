using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Valve.VR;

public delegate void VRPointerCollisionEventHandler(RaycastHit hit);

public class VRPointer : MonoBehaviour
{
    [SerializeField] private float DefaultLength = 5.0f; //Serialize field forces unity to display field in UI for variable

    [Header("External Refrences")]
    [SerializeField] private GameObject PointerEnd = null;
    public Camera Camera { get; private set; } = null;
    private VRInput VR_Input = null;

    private LineRenderer LineRenderer = null;

    public event VRPointerCollisionEventHandler CollisionTrigger;
    public SteamVR_Action_Boolean triggerClick;


    void Awake()
    {
        Camera = GetComponent<Camera>();
        Camera.enabled = false;
        LineRenderer = GetComponent<LineRenderer>();
    }

    private void Start()
    {
        VR_Input = EventSystem.current.gameObject.GetComponent<VRInput>();
    }

    void Update()
    {
        UpdateLineRenderer();
    }

    private void UpdateLineRenderer()
    {
        //Check for distance from input if none then use default
        PointerEventData data = VR_Input.Data;
        //Raycast
        RaycastHit hit = CreateRaycast();

        float CollisionDistance = hit.distance == 0 ? DefaultLength : hit.distance;
        //float CanvasDistance = data.pointerCurrentRaycast.distance == 0 ? DefaultLength : data.pointerCurrentRaycast.distance;
        float CanvasDistance = DefaultLength; 
        if(data != null)
        {
            if (data.pointerCurrentRaycast.distance != 0)
                CanvasDistance = data.pointerCurrentRaycast.distance;
        }

        float targetLength = Mathf.Min(CollisionDistance, CanvasDistance);

        //Default
        Vector3 endPosition = transform.position + (transform.forward * targetLength);


        //Set position of pointer end
        PointerEnd.transform.position = endPosition;

        //Set linerender
        LineRenderer.SetPosition(0, transform.position);
        LineRenderer.SetPosition(1, endPosition);

        RaiseCollsionEnter(hit);
    }

    private RaycastHit CreateRaycast()
    {
        RaycastHit hit;
        Ray ray = new Ray(transform.position, transform.forward);
        Physics.Raycast(ray, out hit, DefaultLength);

        return hit;
    }

    private void RaiseCollsionEnter(RaycastHit hit)
    {
        if(triggerClick.stateDown)
        if (CollisionTrigger != null)
            CollisionTrigger(hit);
    }
}
