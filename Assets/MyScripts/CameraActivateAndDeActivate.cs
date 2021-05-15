using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraActivateAndDeActivate : MonoBehaviour
{

    public GameObject currentCam;

    public GameObject nextCam;


    private void OnTriggerEnter(Collider other)
    {

        if (other.tag == "Player")
        {
            Transform[] allChildren = currentCam.GetComponentsInChildren<Transform>();
            foreach (Transform child in allChildren)
            {
                child.gameObject.SetActive(false);
                currentCam.SetActive(true);
                nextCam.SetActive(true);
            }

            
        }

        

        

    }

}
