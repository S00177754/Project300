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
    public Camera EventCamera;

    void Update()
    {
        //ClickableCam();
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
                            List<UnitDetails> selectedUnits = Commander.Units.Where(u => u.IsSelected == true).ToList();
                            
                            for (int i = 0; i < selectedUnits.Count; i++)
                            {
                                Debug.Log("Moving");
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

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("VRInput"))
        {

            RaycastHit hit;
            Ray ray = EventCamera.ScreenPointToRay(collision.gameObject.transform.position);

            if(Physics.Raycast(ray, out hit))
            {
                switch (hit.collider.gameObject.tag)
                {
                    case "Minimap":
                        break;

                    default:
                        break;
                }

            }

        }
    }

    

    public void MinimapCheck(RaycastHit hit)
    {
        Debug.Log("Minimap Collision");

        var localPoint = hit.textureCoord;

        Ray portalRay = minimapCam.ScreenPointToRay(new Vector2(localPoint.x * minimapCam.pixelWidth, localPoint.y * minimapCam.pixelHeight));
        RaycastHit portalHit;

        if(Physics.Raycast(portalRay, out portalHit) &&
           (portalHit.collider.gameObject.CompareTag("MapInteractable") 
           || portalHit.collider.gameObject.CompareTag("Ground")) )
        {
            //Need to change it up so it accounts for buildings as well
            if (portalHit.collider.CompareTag("MapInteractable"))
            {
                UnitDetails unitDetails;
                CollectorScript collectorScript;
                BarracksScript barracksScript;

                if (portalHit.collider.TryGetComponent<UnitDetails>(out unitDetails))
                {
                    if (unitDetails != null)
                    {
                        if (unitDetails.IsSelected)
                        { unitDetails.IsSelected = false; }
                        else 
                        { unitDetails.IsSelected = true; }
                    }
                }
                else if (portalHit.collider.TryGetComponent<CollectorScript>(out collectorScript))
                {
                    if (collectorScript != null)
                    {
                        DeselectAll();

                        if (!collectorScript.IsSelected)
                        { collectorScript.IsSelected = true; }
                    }
                }
                else if (portalHit.collider.TryGetComponent<BarracksScript>(out barracksScript))
                {
                    if (barracksScript != null)
                    {
                        DeselectAll();

                        if (!barracksScript.IsSelected)
                        { barracksScript.IsSelected = true; }
                    }
                }

            }
            else if (portalHit.collider.gameObject.tag == "Ground")
            {
                MoveSelectedUnits(portalHit.point);
                DeselectAllUnits();
            }

            Debug.Log("Hit Object:" + portalHit.collider.gameObject);

        }
    }

    public void MoveSelectedUnits(Vector3 position)
    {
        Commander.Units.ForEach(u => u.gameObject.GetComponent<PlayerCharacterMover>().MoveTo(position));
    }

    public void DeselectAllUnits()
    {
        Commander.Units.ForEach(u => u.IsSelected = false);
    }

    public void DeselectAll()
    {
        Commander.Base.ResourceCollectors.ForEach(rc => rc.IsSelected = false);
        Commander.Base.UnitBarracks.ForEach(rc => rc.IsSelected = false);
        DeselectAllUnits();
    }

}


