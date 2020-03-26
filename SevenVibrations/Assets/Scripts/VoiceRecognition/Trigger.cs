using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trigger : MonoBehaviour
{
    public GameObject objectToActivate;

    private void Start()
    {
        objectToActivate.SetActive(false);
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            Debug.Log("Activating Object");
            objectToActivate.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        
    }
}
