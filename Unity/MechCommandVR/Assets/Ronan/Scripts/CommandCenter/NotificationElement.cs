using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class NotificationElement : MonoBehaviour
{
    public Image NotificationPanel;
    public TextMeshProUGUI TMP_Text;
    public Image NotificationIcon;

    void Awake()
    {
        
    }

    void Start()
    {
        Invoke("Destroy",5);
    }

    void Update()
    {
        
    }

    public void SetNotification(string text,Sprite iconImage,Color panelColor)
    {
        TMP_Text.text = text;
        NotificationIcon.sprite = iconImage;

        Color color = panelColor;
        color.a = 0.6f;
        NotificationPanel.color = color;
    }

    private void Destroy()
    {
        Destroy(gameObject);
    }
}
