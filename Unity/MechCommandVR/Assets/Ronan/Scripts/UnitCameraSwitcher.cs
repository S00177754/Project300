using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class UnitCameraSwitcher : MonoBehaviour
{
    public RenderTexture CameraRender;
    public CommanderController Commander;

    void Awake()
    {
        
    }

    void Start()
    {
        if (Commander.Units.Count > 0)
        {
            CameraRender = new RenderTexture(Commander.Units.First().GetComponent<Camera>().targetTexture);
            CameraRender.Create();
        }
    }

    void Update()
    {
        
    }

    public void SetCameraToUnit(UnitDetails unitDetails)
    {
        DisableCameras();
        Camera cam = GameObject.FindGameObjectsWithTag("Unit").Where(ud => ud.GetComponent<UnitDetails>().UnitId == unitDetails.UnitId).Single().GetComponent<Camera>();
        cam.enabled = true;

        CameraRender = new RenderTexture(cam.targetTexture);
        CameraRender.Create();
    }

    private void DisableCameras()
    {
        List<GameObject> units = GameObject.FindGameObjectsWithTag("Unit").Where(ud => ud.GetComponent<UnitDetails>().Commander.ID == Commander.ID).ToList();

        for (int i = 0; i < units.Count; i++)
        {
            units[i].GetComponent<Camera>().enabled = false;
        }
    }
}
