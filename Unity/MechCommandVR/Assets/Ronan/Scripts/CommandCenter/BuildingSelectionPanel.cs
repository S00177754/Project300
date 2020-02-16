using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BuildingSelectionPanel : MonoBehaviour
{
    public RectTransform content;
    public GameObject BuildingPanelPrefab;
    public BaseController baseController;
    public TMP_Text resourceCount;

    [Header("Sprites")]
    public Sprite ResourceCollectorSprite;
    public Sprite BarracksSprite;
    public Sprite UnitSprite;

    private BuildingDetailPanel resourcePanel;
    private BuildingDetailPanel barracksPanel;
    private BuildingDetailPanel unitPanel;

    // Start is called before the first frame update
    void Start()
    {
        GenerateList();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateInfo();
    }

    private void GenerateList()
    {
        if (baseController != null)
        {
            resourceCount.text = "Resources: " + baseController.Owner.Resources;

            GameObject panel = Instantiate(BuildingPanelPrefab, content);
            resourcePanel = panel.GetComponent<BuildingDetailPanel>();
            resourcePanel.SetInfo("Resource Collectors", baseController.ResourceCollectors.Count.ToString(), ResourceCollectorSprite);

            panel = Instantiate(BuildingPanelPrefab, content);
            barracksPanel = panel.GetComponent<BuildingDetailPanel>();
            barracksPanel.SetInfo("Unit Barracks", baseController.UnitBarracks.Count.ToString(), BarracksSprite);

            panel = Instantiate(BuildingPanelPrefab, content);
            unitPanel = panel.GetComponent<BuildingDetailPanel>();
            unitPanel.SetInfo("Units", baseController.Owner.Units.Count.ToString(), UnitSprite);
        }
    }

    private void UpdateInfo()
    {
        if (baseController != null)
        {
            resourceCount.text = "Resources: " + baseController.Owner.Resources;
            resourcePanel.UpdateCount(baseController.ResourceCollectors.Count.ToString());
            barracksPanel.UpdateCount(baseController.UnitBarracks.Count.ToString());
            unitPanel.UpdateCount(baseController.Owner.Units.Count.ToString());
        }
    }
}
