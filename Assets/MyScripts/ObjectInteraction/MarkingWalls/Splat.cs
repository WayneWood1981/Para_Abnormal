using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Splat : MonoBehaviour
{

    private ContactPoint collisionPoint;

    AudioSource audioSource;
    MeshRenderer mrenderer;

    public AudioClip[] splats;


    public GameObject splatPrefab;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        mrenderer = GetComponent<MeshRenderer>();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Walls")
        {
            collisionPoint = collision.GetContact(0);

            if (audioSource)
            {
                audioSource.PlayOneShot(splats[Random.Range(0, splats.Length)]);
            }

            GameObject temporarySplatMarkHandler;

            temporarySplatMarkHandler = Instantiate(splatPrefab, collisionPoint.point, Quaternion.LookRotation(collisionPoint.normal)) as GameObject;

            temporarySplatMarkHandler.transform.Rotate(Vector3.right * 90);

            temporarySplatMarkHandler.transform.Translate(Vector3.up * 0.005f);

            Destroy(temporarySplatMarkHandler, 10.0f);

            mrenderer.enabled = false;

            Destroy(gameObject, 1);
        }
    }
}
