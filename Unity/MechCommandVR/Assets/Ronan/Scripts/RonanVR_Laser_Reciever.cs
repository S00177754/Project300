using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.Extras;

public class RonanVR_Laser_Reciever : MonoBehaviour
{
    public SteamVR_LaserPointer laserPointer;

    void Awake()
    {
        laserPointer.PointerIn += PointerInside;  
        laserPointer.PointerOut += PointerOutside;  
        laserPointer.PointerClick += PointerClick;  
    }

    //Pointer Interaction Event Handlers
    //Remember to attatch a box collider to UI elements so the laser hits it, collide can be set to trigger
    private void PointerClick(object sender, PointerEventArgs e)
    {
        if (e.target.name == "TestButton")
        {
            Debug.Log("TestButton was clicked");
        }
    }

    private void PointerOutside(object sender, PointerEventArgs e)
    {
        if (e.target.name == "TestButton")
        {
            Debug.Log("TestButton was exited");
        }
    }

    private void PointerInside(object sender, PointerEventArgs e)
    {
        if (e.target.name == "TestButton")
        {
            Debug.Log("TestButton was entered");
        }
    }


    void Start()
    {
        
    }

    void Update()
    {
        
    }
}
