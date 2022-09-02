using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionRightTrash : MonoBehaviour
{
    public string tagRight;
    public AudioSource playerAudioSource;
    public float volume=0.5f;

    public AudioClip clipGood;
    public AudioClip clipBad;
    // Start is called before the first frame update
    void Start()
    {
        //playerAudioSource = GetComponent<AudioSource>();
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter(Collision collision) 
    {
        Debug.Log($"********* Collision started with {collision.gameObject.tag}"); 
        
        if(!string.Equals(collision.gameObject.tag,"Untagged")) 
        {   
            if(collision.gameObject.tag == tagRight) 
            {   
                Debug.Log("********* right one");
                playerAudioSource.clip = clipGood;
                playerAudioSource.PlayOneShot(clipGood,volume);
                Destroy(collision.gameObject,3f);
            } else {
                Debug.Log("********* bad one");
                playerAudioSource.clip = clipBad;
                playerAudioSource.PlayOneShot(clipBad,volume);
            }
        }
    }
}
