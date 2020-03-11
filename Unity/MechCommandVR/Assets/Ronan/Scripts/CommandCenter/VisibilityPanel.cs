using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisibilityPanel : MonoBehaviour
{
    public List<VisibilityButton> visibilityButtons;

    public void ShowAll()
    {
        visibilityButtons.ForEach(vb => vb.SetVisibility(true));
    }

    public void HideAll()
    {
        visibilityButtons.ForEach(vb => vb.SetVisibility(false));
    }
}
