using UnityEngine;

public class AutoInteract : MonoBehaviour
{

    Rigidbody rb;
    AudioSource audioSource;

    public AudioClip[] ghostClips;
    private int floatCount;

    private bool interacted;

    private bool floatingHasEnded;
    private bool objectHasReachedHeight = false;

    public float speed =  0.1f;
    private float originalYPos;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }
    // Start is called before the first frame update
    void Start()
    {
            floatCount = 0;

        originalYPos = transform.position.y;
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.transform.tag == "Player")
        {

            interacted = true;

            if (audioSource)
            {
                audioSource.PlayOneShot(ghostClips[Random.Range(0, ghostClips.Length)]);
            }

        }

    }

    
    private void Update()
    {
        if (interacted)
        {
            rb.isKinematic = true;

            if (floatingHasEnded == false)
            {
                if (objectHasReachedHeight == false)
                {
                    transform.localPosition += transform.up * speed * Time.deltaTime;
                    

                    if (transform.localPosition.y > originalYPos + 0.3)
                    {
                        objectHasReachedHeight = true;
                        floatCount += 1;
                        
                        
                    }
                }
                else
                {
                    transform.localPosition += -transform.up * speed * Time.deltaTime;

                    if (transform.localPosition.y < originalYPos + 0.2)
                    {
                        objectHasReachedHeight = false;
                    }
                }
            }



            if (floatCount == 3)
            {
                floatingHasEnded = true;
                transform.localPosition += -transform.up * speed * Time.deltaTime;
                
                if (transform.localPosition.y < originalYPos + 0.75)
                {
                    floatCount = 0;
                    floatingHasEnded = false;
                    interacted = false;
                }

            }
        }
        else
        {
            if (rb)
            {
                rb.isKinematic = false;
            }
            
        }

        if (interacted == true && floatingHasEnded == false && this.transform.tag == "SmallObject")
        {
            transform.Rotate(0, Random.Range(1, 5), 0, Space.World);
        }
        
    }

}
