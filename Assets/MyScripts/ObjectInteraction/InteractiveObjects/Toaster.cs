using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Toaster : MonoBehaviour
{
    
    [SerializeField] GameObject smoke;
    [SerializeField] GameObject sparks;


    AudioSource audioSource;

    public AudioClip successSound;



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
            
            smoke.SetActive(true);
            sparks.SetActive(true);

            audioSource.PlayOneShot(successSound);
            

            

        }
    }

    
}
