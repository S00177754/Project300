using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class HealthPanelController : MonoBehaviour
{
    public TMP_Text PlayerName;
    public Slider HealthSlider;

    public void SetName(string name)
    {
        PlayerName.text = name;
    }

    public void SetHealth(float health,float maxHealth)
    {

        HealthSlider.value = health / maxHealth; //Slider works from 0 -> 1.0
    }
}
