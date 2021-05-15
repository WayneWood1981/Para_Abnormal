using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class ActionButton : MonoBehaviour
{
    Controls controls;
    Camera camera;
    PickingUpAndThrowing pickButton;
    Rigidbody rb;
    
    public bool actionHeldDown;

    private void Awake()
    {
        controls = new Controls();
        actionHeldDown = false;
        camera = Camera.main;
        pickButton = GetComponent<PickingUpAndThrowing>();
        rb = GetComponent<Rigidbody>();
    }

    
    private void OnEnable()
    {
        controls.GamePlay.Action.performed += handleAction;
        controls.GamePlay.Action.canceled += handleAction;
        controls.GamePlay.Action.Enable();
    }

    private void Disable()
    {
        controls.GamePlay.Action.performed -= handleAction;
        controls.GamePlay.Action.canceled -= handleAction;
        controls.GamePlay.Action.Disable();
    }


    private void Update()
    {
        if (actionHeldDown)
        {
            
            Physics.IgnoreLayerCollision(3, 10, false);
            Physics.IgnoreLayerCollision(3, 9, false);
            Physics.IgnoreLayerCollision(3, 11, false);
            Physics.IgnoreLayerCollision(3, 13, false);
            Rigidbody t = GetComponent<Rigidbody>();
            rb.detectCollisions = true;
            rb.mass = 0.01f;

            
            // Remove all layers from player so they can be moved
        }
        else
        {
            
            
            Physics.IgnoreLayerCollision(3, 6, true);
            Physics.IgnoreLayerCollision(3, 7, true);
            Physics.IgnoreLayerCollision(3, 8, true);
            Physics.IgnoreLayerCollision(3, 9, true);
            Physics.IgnoreLayerCollision(3, 10, true);
            Physics.IgnoreLayerCollision(3, 11, true);
            Physics.IgnoreLayerCollision(3, 13, false);

            //rb.detectCollisions = false;


        }

    }

    private void LateUpdate()
    {
        if (actionHeldDown)
        {
            camera.cullingMask &= ~(1 << LayerMask.NameToLayer("Player"));
        }
        else
        {
            camera.cullingMask |= 1 << LayerMask.NameToLayer("Player");
        }
    }

    void StartShake()
    {
        


    }

    public void handleAction(InputAction.CallbackContext context)
    {

        if (context.started)
        {
            if (pickButton.actionHeldDown == false)
            {
                actionHeldDown = true;
            }
            
            
        }
        if (context.performed)
        {
            
            
        }
        

        if (context.canceled)
        {
            if (pickButton.actionHeldDown == false)
            {
                actionHeldDown = false;
            }
                
            

        }
        
        
    }

    

    
}
