using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clock : MonoBehaviour
{
    [SerializeField] GameObject bigHand;
    [SerializeField] GameObject littleHand;

    AudioSource audioSource;

    public AudioClip successSound;

    float movementSpeed = 100;

    private bool clockStartsToMove;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "Player")
        {

            clockStartsToMove = true;

            audioSource.Play();

            Invoke("PlaySuccess", 2);

        }
    }


    private void Update()
    {
        if (clockStartsToMove)
        {
            bigHand.transform.Rotate(Vector3.forward * movementSpeed * Time.deltaTime);

            littleHand.transform.Rotate(-Vector3.forward * (movementSpeed * 2) * Time.deltaTime);
        }

        
    }

    void PlaySuccess()
    {
        audioSource.PlayOneShot(successSound);
    }
}
