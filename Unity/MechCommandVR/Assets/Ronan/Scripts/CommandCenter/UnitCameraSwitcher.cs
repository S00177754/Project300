using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using TMPro;

public class UnitCameraSwitcher : MonoBehaviour
{
    public NotificationController notificationController;

    public RenderTexture CameraRenderTex;
    public List<Camera> DroneCameras;
    public TextMeshProUGUI TMPtext;

    void Start()
    {
        SetCameraToDrone(0);
    }

    public void SetCameraToDrone(int number)
    {
        DroneCameras.ForEach(dc => dc.enabled = false);

        DroneCameras[number].enabled = true;
        CameraRenderTex = DroneCameras[number].targetTexture;
        CameraRenderTex.Create();

        TMPtext.text = "Drone " + (number + 1);
    }

    
}
