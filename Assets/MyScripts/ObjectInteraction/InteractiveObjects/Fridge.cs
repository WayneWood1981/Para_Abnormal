using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fridge : MonoBehaviour
{

    [SerializeField] Transform topDoor;
    [SerializeField] Transform hinges;
    [SerializeField] Transform bottomDoor;
    [SerializeField] GameObject spark;
    [SerializeField] GameObject smoke;
    [SerializeField] GameObject sparks;

    AudioSource audioSource;

    public AudioClip successSound;

    float currentDegrees;
    float movementSpeed = 10f;

    
    bool openDoors = false;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Start is called before the first frame update
    void Start()
    {
        Vector3 topDoorV3 = topDoor.position;
        Vector3 bottomDoorV3 = bottomDoor.position;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "Player")
        {
            openDoors = true;
            spark.SetActive(true);
            smoke.SetActive(true);
            sparks.SetActive(true);

            Invoke("PlayClip", 2);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
        if (openDoors)
        {
            currentDegrees += Time.deltaTime;
            Debug.Log(currentDegrees);
            if (currentDegrees < 1.5)
            {
                topDoor.RotateAround(hinges.position, Vector3.up, 8f * movementSpeed * Time.deltaTime);
            }
            if (currentDegrees > 0.5 && currentDegrees < 2)
            {
                bottomDoor.RotateAround(hinges.position, Vector3.up, 11f * movementSpeed * Time.deltaTime);
            }
            
        }
    }

    void PlayClip()
    {
        audioSource.PlayOneShot(successSound);
    }
}
