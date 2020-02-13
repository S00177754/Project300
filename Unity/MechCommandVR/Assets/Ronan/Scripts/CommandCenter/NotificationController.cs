using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NotificationController : MonoBehaviour
{
    public RectTransform Content;
    public GameObject NotificationPrefab;
    public Sprite WarningIcon;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void TestNotification()
    {
        GameObject notification = Instantiate(NotificationPrefab, Content);
        notification.GetComponent<NotificationElement>().SetNotification("CRITICAL DAMAGE",WarningIcon,Color.red);
    }
}
