using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class TutScript : MonoBehaviour
{

    public AudioSource playerAudioSource;
    Rigidbody rb;
    public TextMeshPro tutorialText;
    public AudioClip clipInitial;
    public AudioClip clipTeleport;
    public AudioClip clipPick;
    public float volume=0.5f;

    // Start is called before the first frame update
    void Start()
    {
        //https://www.youtube.com/watch?v=9tMvzrqBUP8
        //Destroy(gameObject,3f);
        rb = GetComponent<Rigidbody>();
        playerAudioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.Rotate(0, 1, 0, Space.Self);
    }

    void onMouseDown() 
    {
        Destroy(gameObject,3f);
    }

    void OnAttachedToHand() 
    {
        //Destroy(gameObject,3f);
        Debug.Log("********* AttachedToHand started");
        Color color = new Color(0,0,255);
        tutorialText.color = color;
        tutorialText.text = "Started";
        //playerAudioSource.clip = 0;
        playerAudioSource.PlayOneShot(clipInitial,volume);
    }

    void ChangeScene()
    {
        Debug.Log("***** Change Scene");
    }
    void ChangeSceneAction()
    {
        Debug.Log("***** Change Scene Action");
    }
    void TeleportPlayer()
    {
        Debug.Log("***** TeleportPlayer ");
    }
}
