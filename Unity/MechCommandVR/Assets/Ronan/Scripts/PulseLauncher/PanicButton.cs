using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanicButton : MonoBehaviour
{
    public GameObject Gun;

    public void Panic()
    {
        Gun.SetActive(true);
    }
}
