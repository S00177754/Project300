using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using Valve.VR;

public class MinimapController : MonoBehaviour
{
    public Camera minimapCam;

    void Update()
    {
        ClickableCam();
    }

    private void ClickableCam()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);


            if (Physics.Raycast(ray, out hit))
            {
                Debug.Log("Hit Plane:" + hit.collider.gameObject);


                var localPoint = hit.textureCoord;

                Ray portalRay = minimapCam.ScreenPointToRay(new Vector2(localPoint.x * minimapCam.pixelWidth, localPoint.y * minimapCam.pixelHeight));
                RaycastHit portalHit;

                if (Physics.Raycast(portalRay, out portalHit) && (portalHit.collider.gameObject.tag == "MapInteractable" || portalHit.collider.gameObject.tag == "Ground"))
                {
                    if (portalHit.collider.gameObject.tag == "MapInteractable")
                    {
                        SelectAndMove selector = portalHit.collider.gameObject.GetComponentInParent<SelectAndMove>();
                        if (selector != null)
                        {
                            if (selector.isSelected)
                            {
                                selector.isSelected = false;
                            }
                            else if (!selector.isSelected)
                            {
                                selector.isSelected = true;
                            }
                        }
                    }
                    else if(portalHit.collider.gameObject.tag == "Ground")
                    {
                        List<GameObject> selectedList = GameObject.FindGameObjectsWithTag("MapInteractable").Where(g => g.GetComponent<SelectAndMove>().isSelected).ToList();

                        for (int i = 0; i < selectedList.Count; i++)
                        {
                            Debug.Log("Moving");
                            selectedList[i].GetComponentInParent<PlayerCharacterMover>().MoveTo(portalHit.point);
                            selectedList[i].GetComponent<SelectAndMove>().isSelected = false;
                        }
                    }

                    Debug.Log("Hit Object:" + portalHit.collider.gameObject);
                }
            }
        }
    }
}


