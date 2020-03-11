using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum NotificationSprites { Warning, Build }

public class NotificationController : MonoBehaviour
{
    public RectTransform Content;
    public GameObject NotificationPrefab;

    [Header("Notification Sprites")]
    public Sprite WarningIcon;
    public Sprite BuildIcon;

    public void UnitDestroyed(string unitName)
    {
        GameObject notification = Instantiate(NotificationPrefab, Content);
        notification.GetComponent<NotificationElement>().SetNotification(unitName+" Destroyed",WarningIcon,Color.red);
    }

    public void SendNotification(string message, NotificationSprites value, Color color)
    {
        Sprite sprite;
        switch (value)
        {
            case NotificationSprites.Build:
                sprite = WarningIcon;
                break;

            default:
            case NotificationSprites.Warning:
                sprite = BuildIcon;
                break;
        }

        GameObject notification = Instantiate(NotificationPrefab, Content);
        notification.GetComponent<NotificationElement>().SetNotification(message, sprite, color);
    }
}
