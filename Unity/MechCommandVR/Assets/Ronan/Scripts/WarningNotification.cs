using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WarningNotification : MonoBehaviour
{
    public TextMeshProUGUI tmpText;
    private UnitDetails unitDetails;

    private void Awake()
    {
        
    }

    void Start()
    {
        tmpText.text = unitDetails.Name + " CRITICAL DAMAGE";
    }

    void Update()
    {
        
    }

    public void SetDetails(UnitDetails details)
    {
        unitDetails = details;
    }
}
