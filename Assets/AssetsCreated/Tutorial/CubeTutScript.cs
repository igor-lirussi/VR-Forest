using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;


public class CubeTutScript : MonoBehaviour
{
    public TextMeshPro tutorialText;

    TutScript tutScript = null;

    // Start is called before the first frame update
    void Start()
    {
        //tutorialText = GetComponent<TextMeshPro>();
        //get the object
        GameObject tutorial = GameObject.Find("Tutorial");
        //get the component from it and store it
        tutScript = tutorial.GetComponent<TutScript>();
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.Rotate(0, 1, 0, Space.Self);
    }


    void OnAttachedToHand() 
    {
        //Start Tutorial
        tutScript.StartTutorial();
    }

    public void ShowTutorialStarted()
    {
        Color color = new Color(0,0,255);
        tutorialText.color = color;
        tutorialText.text = "Started";
    }

    public void ShowTutorialEnded()
    {
        Color color = new Color(0,255,0);
        tutorialText.color = color;
        tutorialText.text = "Tutorial";
    }



}
