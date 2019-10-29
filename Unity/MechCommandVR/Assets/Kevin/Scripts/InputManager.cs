using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class  InputManager: MonoBehaviour
{

    public float panSpeed = 5f;

    private float panDetect = 15f;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        MoveCamera();
    }

    void MoveCamera()
    {
        float moveX = Camera.main.transform.position.x;
        float moveZ = Camera.main.transform.position.z;

        float xPos = Input.mousePosition.x;
        float yPos = Input.mousePosition.y;

        if (Input.GetKey(KeyCode.A))
        {
            moveX -= panSpeed;
            
        }
        else if (Input.GetKey(KeyCode.D))
        {
            moveX += panSpeed;
        }

        if (Input.GetKey(KeyCode.W))
        {
            moveZ += panSpeed;
        }
        else if(Input.GetKey(KeyCode.S))
        {
            moveZ -= panSpeed;
        }

        Vector3 newPosition = new Vector3(moveX, 4, moveZ);

        Camera.main.transform.position = newPosition;
    }
}
