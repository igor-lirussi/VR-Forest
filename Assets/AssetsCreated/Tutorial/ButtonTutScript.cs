//======= Copyright (c) Valve Corporation, All rights reserved. ===============

using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using System;
using Valve.VR.InteractionSystem;

    public class ButtonTutScript : MonoBehaviour
    {
        public void OnButtonDown(Hand fromHand)
        {
            //Start Tutorial
            GameObject.Find("Tutorial").GetComponent<TutScript>().StartTutorial();
        }

        public void OnButtonUp(Hand fromHand)
        {
            //nothing
        }

    }
