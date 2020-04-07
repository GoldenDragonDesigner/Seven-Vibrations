using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            PickUpItem();
        }
    }

    void PickUpItem()
    {
        Destroy(gameObject);
    }
}
