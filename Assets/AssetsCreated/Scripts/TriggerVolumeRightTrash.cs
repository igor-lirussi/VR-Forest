using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerVolumeRightTrash : MonoBehaviour
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

    void OnTriggerEnter(Collider collider) 
    {
        Debug.Log($"********* Trigger Collision started with {collider.gameObject.tag}");   

        if(!string.Equals(collider.gameObject.tag,"Untagged")) 
        {   
            if(collider.gameObject.tag == tagRight) 
            {   
                Debug.Log("********* right one");
                playerAudioSource.clip = clipGood;
                playerAudioSource.PlayOneShot(clipGood,volume);
                Destroy(collider.gameObject,3f);
            } else {
                Debug.Log("********* bad one");
                playerAudioSource.clip = clipBad;
                playerAudioSource.PlayOneShot(clipBad,volume);
            }
        }
    }
}
