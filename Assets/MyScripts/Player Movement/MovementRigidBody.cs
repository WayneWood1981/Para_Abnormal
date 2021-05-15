using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MovementRigidBody : MonoBehaviour
{
    Controls controls;
    Rigidbody rigidBody;
    Camera camera;


    public Vector3 desiredMoveDirection;
    float elevateInput;
    public float elevateSpeed = 3;


    Vector2 moveAxis;

    public float movementSpeed = 10;
    

    private void Awake()
    {
        rigidBody = GetComponent<Rigidbody>();
        camera = Camera.main;

        controls = new Controls();


    }

    private void Update()
    {

        

        if (transform.position.y >= 8.9f)
        {
            Vector3 newPosition = new Vector3(transform.position.x, 1.9f, transform.position.z);
            transform.position = newPosition;
        }
        ElevatePlayer();

    }

    private void FixedUpdate()
    {
        movePlayer();
        
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
        
        rigidBody.velocity = (desiredMoveDirection * movementSpeed * Time.deltaTime);

    }

    public void ElevatePlayer()
    {
        

        if (elevateInput > 0)
        {
            rigidBody.AddForce(Vector3.up * elevateSpeed,ForceMode.Acceleration);
        }
        else if (elevateInput < 0)
        {
            rigidBody.AddForce(-Vector3.up * elevateSpeed, ForceMode.Acceleration);
        }


    }
}
