using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using Valve.VR.InteractionSystem;
using Valve.VR.InteractionSystem.Sample;
using Valve.VR;


public class TutScript : MonoBehaviour
{

    public bool playOnStart = false;
    public AudioSource playerAudioSource;
    public AudioClip clipInitial; //Welcome to the tutorial. Look around, feel free to explore your surroundings moving your head.  
    public AudioClip clipPick; //Now look at your hands, you can pick and release objects with your index finger
    public AudioClip clipTeleport; //Awesome! You can also teleport with your thumbs. Keep your thumb pressed, aim at a point you want to reach. And then release it.
    public AudioClip clipFinal; //Great! the tutorial is finished, feel free to explore the surroundings!
    public float volume=0.7f;

    public string nextSceneName;

    bool tutİnProgress=false;
    bool waitingPickRelease=false;
    bool waitingTeleport=false;

    // Start is called before the first frame update
    void Start()
    {
        //Tutorial Scripting
        //https://www.youtube.com/watch?v=9tMvzrqBUP8
        
        Debug.Log("********* Tutorial can start if playOnStart is true, or pressing keyboard commnand, or if function is called by external game objects (like button press o cube grab) ");
        Debug.Log("********* Keyboard commands: ");
        Debug.Log("********* T : Start Tutorial");
        Debug.Log("********* F : Finish Tutorial");
        Debug.Log("********* N : Next Scene");
        if(playOnStart)
        {
			Invoke( "StartTutorial", 2.0f );
        }
    }

    // Update is called once per frame
    void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.T))
        {
            Debug.Log("********* T pressed ");
            Debug.Log("********* Start Tutorial");
			Invoke( "StartTutorial", 2.0f );
        }

        if (Input.GetKeyDown(KeyCode.F))
        {
            Debug.Log("********* F pressed ");
            Debug.Log("********* Finishing tutorial. ");
            FinishTutorial();
        }
        
        if (Input.GetKeyDown(KeyCode.N))
        {
            Debug.Log("********* N pressed ");
            if (nextSceneName!=null && !nextSceneName.Equals("")){
                Debug.Log("********* Next Scene "+nextSceneName);
                SceneManager.LoadScene(nextSceneName);
            } else{
                Debug.LogWarning("********* Next Scene not set in tutorial script!");
            }
        }

        //checking if player teleports
        if(waitingTeleport)
        {
            foreach ( Hand hand in Player.instance.hands )
            {
                //if telport activated
                if (Teleport.instance.teleportAction.GetStateUp(hand.handType))
                {
                    waitingTeleport=false;
                    FinishTutorial();
                }
            }

        }
    }


    public bool TutorialInProgress()
    {
        return tutİnProgress;
    }


    public void StartTutorial() 
    {
        if (!tutİnProgress) 
        {
            //initial settings
            Debug.Log("********* Tutorial started");
            tutİnProgress = true;
            GameObject.Find("CubeTutorial").GetComponent<CubeTutScript>().ShowTutorialStarted();
            GameObject.Find("Teleporting").GetComponent<Teleport>().enabled = false;
            
            //playerAudioSource.clip = clipInitial
            playerAudioSource.PlayOneShot(clipInitial,volume);
            Invoke( "playNowPick", 10.0f );
        }
    }

    private void playNowPick() 
    {
        playerAudioSource.PlayOneShot(clipPick,volume);
        waitingPickRelease=true;
            Debug.Log("********* Waiting for player to pick object and realease");
        //hide hints on hands
        HideAllHints();
        //show hint for each hand
        foreach ( Hand hand in Player.instance.hands )
        {
            ISteamVR_Action_In action = SteamVR_Input.actionsIn[0];
            if (action.GetActive(hand.handType))
            {
                ControllerButtonHints.ShowTextHint(hand, action, action.GetShortName());
            }
        }
    }

    //an external trigger volume (ex tray collider) should call this 
    public void playNowTeleport() 
    {
        if(waitingPickRelease)
        {
            playerAudioSource.PlayOneShot(clipTeleport,volume);
            waitingPickRelease=false;
            Debug.Log("********* Waiting for player to teleport");
            GameObject.Find("Teleporting").GetComponent<Teleport>().enabled = true;
            //hide hints on hands
            HideAllHints();
            //show hint
            foreach ( Hand hand in Player.instance.hands )
            {
                ISteamVR_Action_In action = SteamVR_Input.actionsIn[1];
                if (action.GetActive(hand.handType))
                {
                    ControllerButtonHints.ShowTextHint(hand, action, action.GetShortName());
                }
            }
            waitingTeleport=true;
        }
    }
    
    public void FinishTutorial() 
    {
        if(tutİnProgress)
        {
            playerAudioSource.Stop();
            playerAudioSource.PlayOneShot(clipFinal,volume);
            GameObject.Find("Teleporting").GetComponent<Teleport>().enabled = true;
            //hide hints on hands
            HideAllHints();
            //final settingss
            tutİnProgress = false;
            GameObject.Find("CubeTutorial").GetComponent<CubeTutScript>().ShowTutorialEnded();
            Debug.Log("********* Tutorial finished");
            //spawn object in random position  to celebrate
            InvokeRepeating("SpawnRandomObject", 2f, 3f);
        }
    }


    public void HideController()
    {
        for (int handIndex = 0; handIndex < Player.instance.hands.Length; handIndex++)
        {
            Hand hand = Player.instance.hands[handIndex];
            if (hand != null)
            {
                hand.HideController(true);
            }
        }
    }

    public void ShowController()
    {
        for (int handIndex = 0; handIndex < Player.instance.hands.Length; handIndex++)
        {
            Hand hand = Player.instance.hands[handIndex];
            if (hand != null)
            {
                hand.ShowController(true);
            }
        }
    }

    public void AnimateHandWithoutController()
    {
        for (int handIndex = 0; handIndex < Player.instance.hands.Length; handIndex++)
        {
            Hand hand = Player.instance.hands[handIndex];
            if (hand != null)
            {
                hand.SetSkeletonRangeOfMotion(Valve.VR.EVRSkeletalMotionRange.WithoutController);
            }
        }
    }

    public void HideAllHints()
    {
        //hide hints
        foreach ( Hand hand in Player.instance.hands )
        {
            ControllerButtonHints.HideAllButtonHints( hand );
            ControllerButtonHints.HideAllTextHints( hand );
            hand.ResetTemporarySkeletonRangeOfMotion();
        }
    }


    private void SpawnRandomObject() 
    {
        Debug.Log("********* SpawnRandomObject called");
        float randomX = UnityEngine.Random.Range(-2,2);
        float randomZ = UnityEngine.Random.Range(-2,2);
        Vector3 randomSpawn = new Vector3(randomX, 10f, randomZ);

        Instantiate(GameObject.Find("Apple"), randomSpawn, Quaternion.identity);
    }
}
