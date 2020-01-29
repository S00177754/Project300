using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PausingMenuScript : MonoBehaviour
{
    private void OnEnable()
    {
        Time.timeScale = 1;
    }

    private void OnDisable()
    {
        Time.timeScale = 0;
    }


}
