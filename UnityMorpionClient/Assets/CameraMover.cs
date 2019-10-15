using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMover : MonoBehaviour
{
    float rotationSpeed = 100;
    Vector3 rotationCenter = new Vector3(2, 2, 2);

    // Update is called once per frame
    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector3 rotationAxis = new Vector3(vertical, 0,horizontal);
        Vector3 rotationCenter = new Vector3(2, 2, 2);

        

        transform.RotateAround(rotationCenter, rotationAxis, Time.deltaTime * rotationSpeed);
    }
}
