using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
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
        //planting: 
        //https://www.youtube.com/watch?v=qo-9CmcKWlY
        //Destroy(gameObject,3f);
        rb = GetComponent<Rigidbody>(); //rigid body of this game object
        //playerAudioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.Rotate(0, 1, 0, Space.Self);
        
        if (Input.GetKeyDown(KeyCode.T))
        {
            Debug.Log("********* T pressed ");
			Invoke( "StartTutorial", 5.0f );
        }
        
        if (Input.GetKeyDown(KeyCode.R))
        {
            Debug.Log("********* R pressed ");
			SceneManager.LoadScene("PrimaryScene 1");
        }
    }

    void onMouseDown() 
    {
        Debug.Log("********* Mouse pressed ");
        Destroy(gameObject,3f);
    }

    void OnAttachedToHand() 
    {
        StartTutorial();
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

    void StartTutorial() 
    {
        //Destroy(gameObject,3f); //second parameter is seconds after gets destroyed
        Debug.Log("********* Tutorial started");
        Color color = new Color(0,0,255);
        tutorialText.color = color;
        tutorialText.text = "Started";
        //playerAudioSource.clip = ???;
        //playerAudioSource.PlayOneShot(clipInitial,volume);
    }
}
