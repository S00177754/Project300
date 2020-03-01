using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UnitControlPanel : MonoBehaviour
{
    [Header("External Components")]
    public RenderTexture UnitCameraTexture;

    [Header("Panel Components")]
    public TMP_Text Name;
    public TMP_Text Type;
    public TMP_Text Health;
    public Slider HealthSlider;

    private UnitDetails unitDetails;
    private UnitCameraSwitcher camSwitcher;

    private void Update()
    {
            UpdateHealth(unitDetails.Health, unitDetails.MaxHealth);
    }

    public void Initialize(UnitDetails details, UnitCameraSwitcher switcher)
    {
        Name.text = details.Name;
        Type.text = details.myType.ToString();
        unitDetails = details;
        camSwitcher = switcher;
        
    }


    public void UpdateHealth(float health,float maxHealth)
    {
        if (health <= 0)
            Destroy(gameObject);

        Health.text = $"HP: {health}/{maxHealth}";
        HealthSlider.value = health / maxHealth;
        
    }

    public void ActivateCamera()
    {
        camSwitcher.SetCameraToUnit(unitDetails);
    }
    
    public void SelectUnit()
    {
        unitDetails.IsSelected = true;
    }

    public void HoldPosition()
    {
        unitDetails.gameObject.GetComponent<NavMeshMover>().MoveTo(unitDetails.gameObject.transform.position);
    }

    public void ReturnToBase()
    {

    }
}
