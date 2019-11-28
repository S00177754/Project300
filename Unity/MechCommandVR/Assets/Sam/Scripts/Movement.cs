using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public float Speed;
    Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.A))
        {
            rb.velocity = -transform.right * Speed;
        }
        if (Input.GetKey(KeyCode.D))
            rb.velocity = transform.right * Speed;
        if (Input.GetKey(KeyCode.W))
            rb.velocity = new Vector3(0, 0, 1) * Speed;
        if (Input.GetKey(KeyCode.S))
            rb.velocity = new Vector3(0, 0, -1) * Speed;
    }
}
