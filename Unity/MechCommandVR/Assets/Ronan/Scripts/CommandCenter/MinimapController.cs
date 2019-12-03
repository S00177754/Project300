using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using Valve.VR;

public class MinimapController : MonoBehaviour
{
    public Camera minimapCam;
    public CommanderController Commander;

    void Update()
    {
        ClickableCam();
    }

    private void ClickableCam()
    {
        if (Input.GetMouseButtonDown(0)) //Need to change to work with VR controllers, temporary solution for debugging
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition); //Need to change to work with VR controllers, temporary solution for debugging


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
}


