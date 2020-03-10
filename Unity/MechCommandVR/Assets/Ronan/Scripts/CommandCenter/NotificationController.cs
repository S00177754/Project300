using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NotificationController : MonoBehaviour
{
    public RectTransform Content;
    public GameObject NotificationPrefab;
    public Sprite WarningIcon;

    public void UnitDestroyed(string unitName)
    {
        GameObject notification = Instantiate(NotificationPrefab, Content);
        notification.GetComponent<NotificationElement>().SetNotification(unitName+" Destroyed",WarningIcon,Color.red);
    }

    public void SendNotification(string message, Sprite sprite, Color color)
    {
        GameObject notification = Instantiate(NotificationPrefab, Content);
        notification.GetComponent<NotificationElement>().SetNotification(message, sprite, color);
    }
}
