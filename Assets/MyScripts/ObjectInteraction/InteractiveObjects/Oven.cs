using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oven : MonoBehaviour
{
    [SerializeField] Transform ovenDoor;

    [SerializeField] GameObject flame1;
    [SerializeField] GameObject flame2;
    [SerializeField] GameObject flame3;
    [SerializeField] GameObject flame4;
    [SerializeField] GameObject smoke;
    [SerializeField] GameObject sparks;

    AudioSource audioSource;
    public AudioClip successSound;

    float currentDegrees;
    float movementSpeed = 150f;


    bool openDoors = false;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }
    // Start is called before the first frame update
    void Start()
    {
        Vector3 topDoorV3 = ovenDoor.position;
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "Player")
        {
            openDoors = true;

            flame1.SetActive(true);
            flame2.SetActive(true);
            flame3.SetActive(true);
            flame4.SetActive(true);

            sparks.SetActive(true);

            Invoke("smoker", 2);

        }
    }

    void smoker()
    {
        smoke.SetActive(true);
        audioSource.PlayOneShot(successSound);
    }

    // Update is called once per frame
    void Update()
    {

        if (openDoors)
        {
            currentDegrees += Time.deltaTime;
            Debug.Log(currentDegrees);
            if (currentDegrees < 0.5f)
            {
                ovenDoor.Rotate(Vector3.right * movementSpeed * Time.deltaTime);
            }
            else
            {
                openDoors = false;
            }
            

        }
    }
}
