using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MovementNewInput : MonoBehaviour
{

    Controls controls;
    CharacterController characterController;
    Camera camera;

    
    Vector3 desiredMoveDirection;
    float elevateInput;
    

    Vector2 moveAxis;

    public float movementSpeed = 10;
    public double gravity;

    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
        camera = Camera.main;

        controls = new Controls();

        
    }

    private void Update()
    {
        movePlayer();
        ElevatePlayer();

        if (transform.position.y >= 1.9f)
        {
            Vector3 newPosition = new Vector3(transform.position.x, 2.4f, transform.position.z);
            transform.position = newPosition;
        }

       
    }

    private void FixedUpdate()
    {
        
    }



    private void OnEnable()
    {
        controls.GamePlay.Move.performed += getAxis;
        controls.GamePlay.Move.canceled += getAxis;
        controls.GamePlay.Elevate.performed += getElevateAxis;
        controls.GamePlay.Elevate.canceled += getElevateAxis;

        controls.GamePlay.Enable();
        
    }

    private void getElevateAxis(InputAction.CallbackContext obj)
    {
        elevateInput = controls.GamePlay.Elevate.ReadValue<float>();
    }

    private void getAxis(InputAction.CallbackContext context)
    {
        moveAxis = context.ReadValue<Vector2>();
    }

    private void OnDisable()
    {
        controls.GamePlay.Move.performed -= getAxis;
        controls.GamePlay.Elevate.performed -= getElevateAxis;
        controls.GamePlay.Move.canceled -= getAxis;
        controls.GamePlay.Elevate.canceled -= getElevateAxis;

        controls.GamePlay.Disable();
    }

    public void movePlayer()
    {
        //camera forward and right vectors:
        var forward = camera.transform.forward;
        var right = camera.transform.right;

        //project forward and right vectors on the horizontal plane (y = 0)
        forward.y = 0f;
        right.y = 0f;
        forward.Normalize();
        right.Normalize();

        //this is the direction in the world space we want to move:
        desiredMoveDirection = forward * moveAxis.y + right * moveAxis.x;
        gravity -= 9.81 * Time.deltaTime;
        characterController.Move(desiredMoveDirection * movementSpeed * Time.deltaTime);
    }

    public void ElevatePlayer()
    {

        if (elevateInput > 0)
        {
            characterController.Move(Vector3.up * movementSpeed / 2 * Time.deltaTime);
        }else if (elevateInput < 0)
        {
            characterController.Move(-Vector3.up * movementSpeed / 2 * Time.deltaTime);
        }
        
        
    }


}
