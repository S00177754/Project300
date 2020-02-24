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
    public SteamVR_Action_Boolean action;
    public VRPointer pointer;
    public VRRadialState rs;


    private void Start()
    {
        pointer.CollisionTrigger += MinimapInteraction;
    }

    void Update()
    {
        //ClickableCam();
    }

    //private void ClickableCam()
    //{
    //    if (Input.GetMouseButtonDown(0)) //Need to change to work with VR controllers, temporary solution for debugging
    //    {
    //        RaycastHit hit;
    //        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition); //Need to change to work with VR controllers, temporary solution for debugging


    //        if (Physics.Raycast(ray, out hit))
    //        {
    //            if(hit.collider.gameObject.tag == "Minimap") 
    //            {
    //                Debug.Log("minimap");
    //                var localPoint = hit.textureCoord;

    //                Ray portalRay = minimapCam.ScreenPointToRay(new Vector2(localPoint.x * minimapCam.pixelWidth, localPoint.y * minimapCam.pixelHeight));
    //                RaycastHit portalHit;

    //                if (Physics.Raycast(portalRay, out portalHit) && (portalHit.collider.gameObject.tag == "MapInteractable" || portalHit.collider.gameObject.tag == "Ground"))
    //                {
                        
    //                    if (portalHit.collider.gameObject.tag == "MapInteractable")
    //                    {
    //                        UnitDetails unitDetails = portalHit.collider.gameObject.GetComponentInParent<UnitDetails>();
    //                        if (unitDetails != null)
    //                        {
    //                            if (unitDetails.IsSelected)
    //                            {
    //                            unitDetails.IsSelected = false;
    //                            }
    //                            else if (!unitDetails.IsSelected)
    //                            {
    //                            unitDetails.IsSelected = true;
    //                            }
    //                        }
    //                    }
    //                    else if (portalHit.collider.gameObject.tag == "Ground")
    //                    {
    //                        List<UnitDetails> selectedUnits = Commander.Units.Where(u => u.IsSelected == true).ToList();
                            
    //                        for (int i = 0; i < selectedUnits.Count; i++)
    //                        {
    //                            Debug.Log("Moving");
    //                            selectedUnits[i].GetComponent<PlayerCharacterMover>().MoveTo(portalHit.point);
    //                            selectedUnits[i].GetComponent<UnitDetails>().IsSelected = false;
    //                        }
    //                    }

    //                    Debug.Log("Hit Object:" + portalHit.collider.gameObject);
    //                }
    //            }
    //        }
    //    }
    //}

    public void MinimapInteraction(RaycastHit hit)
    {
        if (rs.RadialState == "Move")
        {
            if (hit.collider.CompareTag("Minimap"))
                MoveCheck(hit);
        }
        else if(rs.RadialState == "BuildBarrack")
        {
            if (hit.collider.CompareTag("Minimap"))
                PlaceBarrack(hit);
        }
        else if (rs.RadialState == "Cancel")
        {

        }
        else if (rs.RadialState == "BuildCollector")
        {
            if(hit.collider.CompareTag("Minimap"))
                PlaceCollecter(hit);
        }
    }       
    
    public void PlaceBarrack(RaycastHit hit)
    {
        Debug.Log("Place Barrack");

        var localPoint = hit.textureCoord;

        Ray portalRay = minimapCam.ScreenPointToRay(new Vector2(localPoint.x * minimapCam.pixelWidth, localPoint.y * minimapCam.pixelHeight));
        RaycastHit portalHit;

        if (Physics.Raycast(portalRay, out portalHit) &&
           (portalHit.collider.gameObject.CompareTag("Ground")))
        {
            Commander.Base.Builder.BuildUnitBarracks(portalHit.point);
        }
    }

    public void PlaceCollecter(RaycastHit hit)
    {
        Debug.Log("Place Collecter");

        var localPoint = hit.textureCoord;

        Ray portalRay = minimapCam.ScreenPointToRay(new Vector2(localPoint.x * minimapCam.pixelWidth, localPoint.y * minimapCam.pixelHeight));
        RaycastHit portalHit;

        if (Physics.Raycast(portalRay, out portalHit) &&
           (portalHit.collider.gameObject.CompareTag("Ground")))
        {
            Commander.Base.Builder.BuildResourceCollector(portalHit.point);
        }
    }

    public void MoveCheck(RaycastHit hit)
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

                if (portalHit.collider.gameObject.GetComponent<IconController>().mapInteractable.TryGetComponent<UnitDetails>(out unitDetails))
                {
                    print("Unit map");

                    if (unitDetails != null)
                    {
                        if (unitDetails.IsSelected)
                        { unitDetails.IsSelected = false; }
                        else 
                        { unitDetails.IsSelected = true; }
                    }
                }
                else if (portalHit.collider.gameObject.GetComponent<IconController>().mapInteractable.TryGetComponent<CollectorScript>(out collectorScript))
                {
                    print("Collecter map");

                    if (collectorScript != null)
                    {
                        DeselectAll();

                        if (!collectorScript.IsSelected)
                        { collectorScript.IsSelected = true; }
                    }
                }
                else if (portalHit.collider.gameObject.GetComponent<IconController>().mapInteractable.TryGetComponent<BarracksScript>(out barracksScript))
                {
                    print("Barracks map");
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
        Commander.Units.Where(u => u != null).Where(u => u.IsSelected).ToList().ForEach(u => u.gameObject.GetComponent<PlayerCharacterMover>().MoveTo(position));
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


