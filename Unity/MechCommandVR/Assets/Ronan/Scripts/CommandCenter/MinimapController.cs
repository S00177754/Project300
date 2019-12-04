using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using Valve.VR;
using Valve.VR.InteractionSystem;

public class MinimapController : MonoBehaviour
{
    public Camera minimapCam;
    public CommanderController Commander;

    private SteamVR_Input_Sources handType; //Reference to hand
    public SteamVR_Action_Boolean grabAction; //referencer to action

    private bool MinimapTriggered;
    public GameObject controller;

    Ray ray;

    private void Start()
    {
        //leftController = GameObject.FindGameObjectsWithTag("VR_Hand").Where(e => e.GetComponent<Hand>().handType == SteamVR_Input_Sources.LeftHand).Single();
        //rightController = GameObject.FindGameObjectsWithTag("VR_Hand").Where(e => e.GetComponent<Hand>().handType == SteamVR_Input_Sources.RightHand).Single();

        grabAction.AddOnStateUpListener(TriggerUp, handType);
        handType = SteamVR_Input_Sources.LeftHand;
    }

    void Update()
    {
        //ClickableCam();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "VR_Hand")
        {
            MinimapTriggered = true;
            handType = other.gameObject.GetComponent<Hand>().handType;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "VR_Hand")
        {
            MinimapTriggered = false;

        }
    }

    private void ClickableCam()
    {
        if (MinimapTriggered) //Need to change to work with VR controllers, temporary solution for debugging
        {
            RaycastHit hit;
             ray = Camera.main.ScreenPointToRay(controller.transform.position); //Need to change to work with VR controllers, temporary solution for debugging
            //Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition); //Need to change to work with VR controllers, temporary solution for debugging


            if (Physics.Raycast(ray, out hit))
            {
                if(hit.collider.gameObject.tag == "Minimap") 
                {
                    Debug.Log("minimap");
                    var localPoint = hit.textureCoord;

                    Ray portalRay = minimapCam.ScreenPointToRay(new Vector2(localPoint.x * minimapCam.pixelWidth, localPoint.y * minimapCam.pixelHeight));
                    RaycastHit portalHit;

                    if (Physics.Raycast(portalRay, out portalHit) && (portalHit.collider.gameObject.tag == "MapInteractable" || portalHit.collider.gameObject.tag == "Ground"))
                    {
                        
                        if (portalHit.collider.gameObject.tag == "MapInteractable")
                        {
                            UnitDetails unitDetails = portalHit.collider.gameObject.GetComponentInParent<UnitDetails>();
                            if (unitDetails != null)
                            {
                                if (unitDetails.IsSelected)
                                {
                                unitDetails.IsSelected = false;
                                }
                                else if (!unitDetails.IsSelected)
                                {
                                unitDetails.IsSelected = true;
                                }
                            }
                        }
                        else if (portalHit.collider.gameObject.tag == "Ground")
                        {
                            Debug.Log("Ground");
                            List<UnitDetails> selectedUnits = Commander.Units.Where(u => u.IsSelected == true).ToList();
                            
                            for (int i = 0; i < selectedUnits.Count; i++)
                            {
                                Debug.Log("Moving"); //PlayerCharacterMover and Unit Details
                                selectedUnits[i].GetComponent<PlayerCharacterMover>().MoveTo(portalHit.point);
                                selectedUnits[i].GetComponent<UnitDetails>().IsSelected = false;
                            }
                        }

                        Debug.Log("Hit Object:" + portalHit.collider.gameObject);
                    }
                }
            }
        }
    }

    public void TriggerUp(SteamVR_Action_Boolean fromAction, SteamVR_Input_Sources fromSource)
    {
        Debug.Log("Trigger is up.");
        ClickableCam();
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.magenta;
        Gizmos.DrawRay(ray);
    }
}


