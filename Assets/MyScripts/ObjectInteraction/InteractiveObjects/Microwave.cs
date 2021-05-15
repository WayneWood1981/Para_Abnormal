using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Microwave : MonoBehaviour
{
    [SerializeField] GameObject smoke
        ;
    [SerializeField] GameObject sparks;

    AudioSource audioSource;

    public AudioClip successSound;

    bool isOn = true;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "Player")
        {

            
            sparks.SetActive(true);
            Invoke("Sparky", 2);

        }
    }

    
    void Sparky()
    {
        smoke.SetActive(true);
        audioSource.PlayOneShot(successSound);
    }
}
