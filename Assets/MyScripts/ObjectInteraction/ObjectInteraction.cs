using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ObjectInteraction : MonoBehaviour
{

    ActionButton actionButton;

    private void Awake()
    {
        actionButton = FindObjectOfType<ActionButton>();
    }



    private void Start()
    {
        
    }

    private void Update()
    {
        if (actionButton.actionHeldDown)
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            Physics.IgnoreCollision(player.GetComponent<Collider>(), GetComponent<Collider>(), false);
        }
        else
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            Physics.IgnoreCollision(player.GetComponent<Collider>(), GetComponent<Collider>());
        }
    }


}
