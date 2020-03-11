using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class VisibilityButton : MonoBehaviour
{
    public GameObject UI_Panel;
    public TMP_Text ButtonText;
    public Image VisibilityImage;

    [Header("Sprites")]
    public Sprite VisibleIcon;
    public Sprite HiddenIcon;

    private void Start()
    {
        SetSprite();
    }

    void SetSprite()
    {
        if (UI_Panel.activeSelf)
        {
            VisibilityImage.sprite = VisibleIcon;
        }
        else
        {
            VisibilityImage.sprite = HiddenIcon;
        }
    }

    public void ToggleVisiblity()
    {
        if (UI_Panel.activeSelf)
        {
            UI_Panel.SetActive(false);
        }
        else
        {
            UI_Panel.SetActive(true);
        }

        SetSprite();
    }

    public void SetVisibility(bool value)
    {
        UI_Panel.SetActive(value);
        SetSprite();
    }
}
