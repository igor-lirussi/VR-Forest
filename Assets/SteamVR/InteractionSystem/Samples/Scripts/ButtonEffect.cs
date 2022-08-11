//======= Copyright (c) Valve Corporation, All rights reserved. ===============

using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using System;

namespace Valve.VR.InteractionSystem.Sample
{
    public class ButtonEffect : MonoBehaviour
    {
        public GameObject objectSpawned;
        public void OnButtonDown(Hand fromHand)
        {
            ColorSelf(Color.cyan);
            fromHand.TriggerHapticPulse(1000);
            InvokeRepeating("SpawnRandomTrash", 2f, 3f);
        }

        public void OnButtonUp(Hand fromHand)
        {
            ColorSelf(Color.white);
        }

        private void ColorSelf(Color newColor)
        {
            Renderer[] renderers = this.GetComponentsInChildren<Renderer>();
            for (int rendererIndex = 0; rendererIndex < renderers.Length; rendererIndex++)
            {
                renderers[rendererIndex].material.color = newColor;
            }
        }

        private void SpawnRandomTrash() 
        {
            Debug.Log("********* spawn called");
            float randomX = UnityEngine.Random.Range(-5,5);
            float randomZ = UnityEngine.Random.Range(-5,5);

            Vector3 randomSpawn = new Vector3(randomX, 10f, randomZ);

            Instantiate(objectSpawned, randomSpawn, Quaternion.identity);
        }
    }
}