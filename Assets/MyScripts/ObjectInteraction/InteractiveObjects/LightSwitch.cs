using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightSwitch : MonoBehaviour
{

    [SerializeField] GameObject light;

    bool isOn = true;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "Player")
        {
            Debug.Log("Registering");
            if (isOn)
            {
                ToggleLightSwitch(false);
                isOn = false;
            }
            else
            {
                ToggleLightSwitch(true);
                isOn = true;
            }

        }
    }

    void ToggleLightSwitch(bool isOn)
    {
        light.SetActive(isOn);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
