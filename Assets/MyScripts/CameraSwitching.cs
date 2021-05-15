using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSwitching : MonoBehaviour
{

    [SerializeField] Transform camera;
    [SerializeField] Transform newCameraSpot;
    Vector3 move;

    public float moveCameraSpeed = 2f;
    public float rotationSpeed = 2f;

    public bool hasTriggered = false;

    private void Awake()
    {
        move = new Vector3(newCameraSpot.transform.position.x, newCameraSpot.transform.position.y, newCameraSpot.transform.position.z);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            
            hasTriggered = true;
            
        }
    }


    private void Update()
    {
        if (hasTriggered)
        {
            cameraMovePosition();
        }
        
    }

    private void cameraMovePosition()
    {

        camera.transform.position = Vector3.MoveTowards(camera.transform.position, newCameraSpot.position, Time.deltaTime * moveCameraSpeed);
        
        camera.transform.rotation = Quaternion.Lerp(camera.transform.rotation, newCameraSpot.rotation, rotationSpeed * Time.deltaTime);

        if (camera.transform.position == newCameraSpot.position)
        {
            hasTriggered = false;
        }
        
    }

    
}
