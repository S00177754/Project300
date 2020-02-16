using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class VRRadialMenu : MonoBehaviour
{
    [Header("Scene Transforms")]
    public Transform selectionTransform = null;
    public Transform cursorTransform = null;

    [Header("Radial Selection - Events")]
    public VRRadialSection top = null;
    public VRRadialSection bottom = null;
    public VRRadialSection right = null;
    public VRRadialSection left = null;

    private Vector2 touchPosition = Vector2.zero;
    private List<VRRadialSection> radialSections = null;
    private VRRadialSection highlightedSection = null;

    private readonly float degreeIncrement = 90.0f;



    //Monbehaviour Methods

    private void Awake()
    {
        SetupSections();
    }

    private void Start()
    {
        DisplayMenu(false);
    }

    private void Update()
    {
        Vector2 direction = Vector2.zero + touchPosition;
        float rotation = GetDegree(direction);

        SetCursorPos();
        SetSelectionRotation(rotation);
        SetSelectedEvent(rotation);
    }

    //Custom Methods

    public void DisplayMenu(bool value)
    {
        gameObject.SetActive(value);
    }

    private float GetDegree(Vector2 direction)
    {
        //Atan 1 didn't work properly
        float value = Mathf.Atan2(direction.x, direction.y);
        value *= Mathf.Rad2Deg;

        if(value < 0)
        {
            value += 360.0f;
        }

        return value;
    }

    private void SetCursorPos()
    {
        cursorTransform.localPosition = touchPosition;
    }

    public void SetTouchPos(Vector2 position)
    {
        touchPosition = position;
    }

    private void SetupSections()
    {
        radialSections = new List<VRRadialSection>()
        {
            top,
            right,
            bottom,
            left
        };

        foreach (VRRadialSection section in radialSections)
        {
            section.iconRenderer.sprite = section.icon;
        }
    }

    private void SetSelectionRotation(float rotation)
    {
        float snappedRotation = SnapRotation(rotation);
        snappedRotation += 45f;
        selectionTransform.localEulerAngles = new Vector3(0, 0, -snappedRotation);
    }

    private float SnapRotation(float rotation)
    {
        return GetNearestIncrement(rotation) * degreeIncrement;
    }

    private int GetNearestIncrement(float rotation)
    {
        return Mathf.RoundToInt(rotation / degreeIncrement);
    }

    private void SetSelectedEvent(float currentRotation)
    {
        int index = GetNearestIncrement(currentRotation);

        if (index == 360/degreeIncrement)
            index = 0;

        highlightedSection = radialSections[index];
    }

    public void ActivateHighlightedSection()
    {
        highlightedSection.onPress.Invoke();
    }
}
