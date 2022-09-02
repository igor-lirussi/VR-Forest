using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerVolumeRightTray : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider collider) 
    {
        Debug.Log($"********* Trigger Collision started with {collider.gameObject.tag}");   
  
            if(collider.gameObject.tag == "organic") 
            {   
                Debug.Log("********* right one");
                GameObject.Find("Tutorial").GetComponent<TutScript>().playNowTeleport();
            }
        
    }
}
