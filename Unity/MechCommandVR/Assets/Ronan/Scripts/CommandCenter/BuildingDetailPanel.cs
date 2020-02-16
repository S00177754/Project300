using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class BuildingDetailPanel : MonoBehaviour
{
    public TMP_Text BuildingName;
    public TMP_Text BuildingCount;
    public Image BuildingSprite;

   public void SetInfo(string name, string count, Sprite sprite)
   {
        BuildingSprite.sprite = sprite;
        BuildingName.text = name;
        BuildingCount.text = "X" + count;
   }

    public void UpdateCount(string count)
    {
        BuildingCount.text = "Count: X" + count;
    }


}
