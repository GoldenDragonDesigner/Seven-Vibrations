using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Forcefield : MonoBehaviour
{
    public Transform player;
    public GameObject forcefield;

    private GameObject cloneForcefield;

    private bool canSpawn = true;

    //need a variable for using vibration

    private void Update()
    {
        if(canSpawn && Input.GetKeyDown(KeyCode.P))//trying to make this so the player can put a forcefield over a plant to protect it
        {
            cloneForcefield = Instantiate(forcefield);
            cloneForcefield.transform.position = player.position;
            canSpawn = false;
        }

        if (!canSpawn)
        {

        }
    }
}
