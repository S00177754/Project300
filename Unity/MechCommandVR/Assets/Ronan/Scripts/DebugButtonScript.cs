using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugButtonScript : MonoBehaviour
{

    public void TestClick()
    {
        Debug.Log("GameObject:" + gameObject.name + ", has been clicked.");
    }
}
