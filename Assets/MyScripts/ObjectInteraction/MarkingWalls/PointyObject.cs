using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointyObject : MonoBehaviour
{
    private ContactPoint collisionPoint;

    AudioSource audioSource;

    MeshRenderer mrenderer;

    public AudioClip[] hits;

    public float depth = 8.0f;

    public GameObject stabPrefab;

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

            

            GameObject temporarySplatMarkHandler;

            temporarySplatMarkHandler = Instantiate(stabPrefab, collisionPoint.point, Quaternion.LookRotation(collisionPoint.normal)) as GameObject;
            

            temporarySplatMarkHandler.transform.Rotate(Vector3.right / 70);
            temporarySplatMarkHandler.transform.Rotate(Vector3.forward * 90);
            temporarySplatMarkHandler.transform.Translate(Vector3.up * 0.005f);
            
            if (audioSource)
            {
                audioSource.PlayOneShot(hits[Random.Range(0, hits.Length)]);
            }

            

            temporarySplatMarkHandler.GetComponent<Rigidbody>().useGravity = false;
            temporarySplatMarkHandler.GetComponent<Rigidbody>().isKinematic = true;
            temporarySplatMarkHandler.GetComponent<MeshCollider>().enabled = false;
            temporarySplatMarkHandler.gameObject.tag = "Walls";




            mrenderer.enabled = false;
            Destroy(this.gameObject, 1);

        }
    }
}
