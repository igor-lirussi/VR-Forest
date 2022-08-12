//======= Copyright (c) Valve Corporation, All rights reserved. ===============

using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using System;
using Valve.VR.InteractionSystem;

    public class ButtonTutScript : MonoBehaviour
    {
        public GameObject objectSpawned;
        public void OnButtonDown(Hand fromHand)
        {
            //Start Tutorial
            GameObject.Find("Tutorial").GetComponent<TutScript>().StartTutorial();
            //InvokeRepeating("SpawnRandomTrash", 2f, 3f);
        }

        public void OnButtonUp(Hand fromHand)
        {
            //
        }

        private void SpawnRandomTrash() 
        {
            Debug.Log("********* SpawnRandomTrash called");
            float randomX = UnityEngine.Random.Range(-5,5);
            float randomZ = UnityEngine.Random.Range(-5,5);

            Vector3 randomSpawn = new Vector3(randomX, 10f, randomZ);

            Instantiate(objectSpawned, randomSpawn, Quaternion.identity);
        }
    }
