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

    bool tutİnProgress=false;
    bool waitingPickRelease=false;
    bool waitingTeleport=false;

    // Start is called before the first frame update
    void Start()
    {
        //Tutorial Scripting
        //https://www.youtube.com/watch?v=9tMvzrqBUP8
        //planting: 
        //https://www.youtube.com/watch?v=qo-9CmcKWlY
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
			Invoke( "StartTutorial", 2.0f );
        }
        
        if (Input.GetKeyDown(KeyCode.R))
        {
            Debug.Log("********* R pressed ");
			SceneManager.LoadScene("PrimaryScene 1");
        }

        if (Input.GetKeyDown(KeyCode.F))
        {
            Debug.Log("********* F pressed ");
            FinishTutorial();
        }
        
        if(waitingTeleport)
        {
            foreach ( Hand hand in Player.instance.hands )
            {
                //telport activated
                if (Teleport.instance.teleportAction.GetStateDown(hand.handType))
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
            
            //playerAudioSource.clip = clipInitial
            playerAudioSource.PlayOneShot(clipInitial,volume);
            Invoke( "playNowPick", 10.0f );
        }
    }

    private void playNowPick() 
    {
        playerAudioSource.PlayOneShot(clipPick,volume);
        waitingPickRelease=true;
        //hide hints on hands
        HideAllHints();
        //show hint
        foreach ( Hand hand in Player.instance.hands )
        {
            ISteamVR_Action_In action = SteamVR_Input.actionsIn[0];
            if (action.GetActive(hand.handType))
            {
                ControllerButtonHints.ShowTextHint(hand, action, action.GetShortName());
            }
        }
    }

    public void playNowTeleport() 
    {
        if(waitingPickRelease)
        {
            playerAudioSource.PlayOneShot(clipTeleport,volume);
            waitingPickRelease=false;
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
            playerAudioSource.PlayOneShot(clipFinal,volume);
            //hide hints on hands
            HideAllHints();
            //final settingss
            tutİnProgress = false;
            GameObject.Find("CubeTutorial").GetComponent<CubeTutScript>().ShowTutorialEnded();
            Debug.Log("********* Tutorial finished");
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
}
