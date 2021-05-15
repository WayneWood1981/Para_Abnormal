using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sink : MonoBehaviour
{

    [SerializeField] GameObject tap;
    [SerializeField] Transform LeftDoor;
    [SerializeField] Transform RightDoor;

    AudioSource audioSource;

    public AudioClip successClip;
    public AudioClip waterRunning;

    private bool doorsOpen;

    float movementSpeed = 5;
    float currentTime;

    private Quaternion leftRotation;
    private Quaternion rightRotation;

    private void Awake()
    {

        audioSource = GetComponent<AudioSource>();
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "Player")
        {

            tap.SetActive(true);
            doorsOpen = true;
            leftRotation = Quaternion.AngleAxis(-90, transform.up);
            rightRotation = Quaternion.AngleAxis(90, transform.up);
            audioSource.Play();
            Invoke("PlaySuccess", 2);
        }
    }

    private void Update()
    {
        if (doorsOpen)
        {
            LeftDoor.transform.rotation = Quaternion.Lerp(LeftDoor.transform.rotation, leftRotation, movementSpeed * Time.deltaTime);

            RightDoor.transform.rotation = Quaternion.Lerp(RightDoor.transform.rotation, rightRotation, movementSpeed * Time.deltaTime);
        }
    }

    void PlaySuccess()
    {
        audioSource.PlayOneShot(successClip);

    }
    
}
