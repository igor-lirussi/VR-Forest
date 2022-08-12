using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;


public class TutScript : MonoBehaviour
{

    public AudioSource playerAudioSource;
    public AudioClip clipInitial;
    public AudioClip clipTeleport;
    public AudioClip clipPick;
    public float volume=0.5f;

    bool tutİnProgress=false;

    // Start is called before the first frame update
    void Start()
    {
        //Tutorial Scripting
        //https://www.youtube.com/watch?v=9tMvzrqBUP8
        //planting: 
        //https://www.youtube.com/watch?v=qo-9CmcKWlY
    }

    // Update is called once per frame
    void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.T))
        {
            Debug.Log("********* T pressed ");
			Invoke( "StartTutorial", 2.0f );
        }
        
        if (Input.GetKeyDown(KeyCode.R))
        {
            Debug.Log("********* R pressed ");
			SceneManager.LoadScene("PrimaryScene 1");
        }
    }

    public bool TutorialİnProgress()
    {
        return tutİnProgress;
    }


    public void StartTutorial() 
    {
        if (!tutİnProgress) 
        {
            Debug.Log("********* Tutorial started");
            tutİnProgress = true;
            GameObject.Find("CubeTutorial").GetComponent<CubeTutScript>().ShowTutorialStarted();
            //playerAudioSource.clip = ???;
            //playerAudioSource.PlayOneShot(clipInitial,volume);
            
            Invoke( "EndTutorial", 5.0f );
        }
    }

    
    public void EndTutorial() 
    {
        tutİnProgress = false;
        GameObject.Find("CubeTutorial").GetComponent<CubeTutScript>().ShowTutorialEnded();
    }
}
