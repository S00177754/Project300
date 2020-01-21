using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class VRPointer : MonoBehaviour
{
    public float DefaultLength = 5.0f;
    public GameObject PointerEnd;
    public VRInput VR_Input;

    private LineRenderer LineRenderer = null;

    void Awake()
    {
        LineRenderer = GetComponent<LineRenderer>();
    }

    void Update()
    {
        UpdateLineRenderer();
    }

    private void UpdateLineRenderer()
    {
        //Check for distance from input if none then use default
        PointerEventData data = VR_Input.GetData();
        //float targetLength = DefaultLength;
        float targetLength = data.pointerCurrentRaycast.distance == 0 ? DefaultLength : data.pointerCurrentRaycast.distance;

        //Raycast
        RaycastHit hit = CreateRaycast(targetLength);

        //Default
        Vector3 endPosition = transform.position + (transform.forward * targetLength);

        //Or based on hit
        if(hit.collider != null)
        {
            endPosition = hit.point;
        }

        //Set position of pointer end
        PointerEnd.transform.position = endPosition;

        //Set linerender
        LineRenderer.SetPosition(0, transform.position);
        LineRenderer.SetPosition(1, endPosition);
    }

    private RaycastHit CreateRaycast(float length)
    {
        RaycastHit hit;
        Ray ray = new Ray(transform.position, transform.forward);
        Physics.Raycast(ray, out hit, DefaultLength);

        return hit;
    }
}
