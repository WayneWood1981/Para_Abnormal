using UnityEngine;
using UnityEngine.InputSystem;

public class PickingUpAndThrowing : MonoBehaviour
{




    // CLEAN UP THIS CODE!!!!!






    Controls controls;
    MovementRigidBody moveRB;
    Camera camera;
    ActionButton actionButton;
    AudioSource audioSource;

    public AudioClip[] throwSound;

    public Transform nearestObj;
    public float distanceSqr;
    public float impulseSpeed;
    private float maxImpulseStrength = 5;
    

    public float pickupSpeed = 2f;

    public bool attemptingToHold;
    public bool itemIsBeingHeld;
    public bool throwingObject;
    public bool actionHeldDown;
    private bool cancelItemBeingHeld;
    bool startStrengthTimer = false;

    // Start is called before the first frame update
    void Awake()
    {
        controls = new Controls();
        attemptingToHold = false;
        moveRB = GetComponent<MovementRigidBody>();
        camera = Camera.main;
        actionButton = GetComponent<ActionButton>();
        audioSource = GetComponent<AudioSource>();
    }

    private void OnEnable()
    {
        controls.GamePlay.PickUp.performed += HandlePickUp;
        controls.GamePlay.PickUp.canceled += HandlePickUp;
        controls.GamePlay.PickUp.Enable();
    }

    

    private void OnDisable()
    {
        controls.GamePlay.PickUp.performed -= HandlePickUp;
        controls.GamePlay.PickUp.canceled -= HandlePickUp;
        controls.GamePlay.PickUp.Disable();
    }

    private void Update()
    {
        if (startStrengthTimer)
        {
            impulseSpeed += Time.deltaTime;
            if(impulseSpeed >= maxImpulseStrength)
            {
                impulseSpeed = maxImpulseStrength;
            }
        }

        if (actionHeldDown)
        {

            PlaceInsideTransform(nearestObj);


        }
        else
        {
            DropObject();
        }

    }


    
    // Update is called once per frame
    void LateUpdate()
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

    public void HandlePickUp(InputAction.CallbackContext context)
    {
        if (context.started)
        {

            
            if (actionButton.actionHeldDown == false)
            {
                Debug.Log("Started");
                GetClosestSmallObject();
                actionHeldDown = true;
                startStrengthTimer = true;
            }
            
            
        }
        


        if (context.canceled)
        {

            Debug.Log("Canceled");
            if (actionButton.actionHeldDown == false)
            {
                if (itemIsBeingHeld)
                {
                    ThrowObject();
                    actionHeldDown = false;

                }
                else
                {
                    DropObject();
                    actionHeldDown = false;

                }
            }



        }
    }

    

    private void ThrowObject()
    {
        
        if (nearestObj != null)
        {
            Rigidbody objectsRB = nearestObj.GetComponent<Rigidbody>();
            objectsRB.useGravity = true;
            if (moveRB.desiredMoveDirection != Vector3.zero)
            {
                objectsRB.AddForce(moveRB.desiredMoveDirection * impulseSpeed * 100);
                startStrengthTimer = false;
                impulseSpeed = 0;
                
                if (audioSource)
                {
                    
                    audioSource.PlayOneShot(throwSound[Random.Range(0, throwSound.Length)]);

                }
                
            }
            else
            {
                DropObject();
            }
            
            
        }
        
        
    }

    private void DropObject()
    {
        if (nearestObj != null)
        {
            Rigidbody objectsRB = nearestObj.GetComponent<Rigidbody>();
            objectsRB.useGravity = true;
            
        }

        
    }

    public void GetClosestSmallObject()
    {
        
        var nearestDistanceSqr = Mathf.Infinity;
        var taggedGameObjects = GameObject.FindGameObjectsWithTag("SmallObject");
        //nearestObj = null;
        foreach (var obj in taggedGameObjects)
        {
            Vector3 objectPos = obj.transform.position;
            distanceSqr = (objectPos - transform.position).sqrMagnitude;

            if (distanceSqr < nearestDistanceSqr)
            {
                nearestObj = obj.transform;
                
                nearestDistanceSqr = distanceSqr;

                
            }
        }

    }

    

    void PlaceInsideTransform(Transform item)
    {
        
        item.transform.position = Vector3.MoveTowards(item.transform.position, transform.position, pickupSpeed * Time.deltaTime);
        item.transform.Rotate(1.0f, 1.0f, 1.0f, Space.World);
        nearestObj.GetComponent<Rigidbody>().useGravity = false;



        if (item.transform.position == transform.position)
        {
            
            itemIsBeingHeld = true;
            
            
        }
        
        
        

        
        
    }

    
}
