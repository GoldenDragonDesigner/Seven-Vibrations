using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//for creating a SAVE & LOAD SYSTEM in unity by Brackeys
[System.Serializable]
public class PlayerData : MonoBehaviour
{
    public float health;
    public float[] position;


    public PlayerData(Player player)
    {
        health = GetComponent<PlayerHealth>().CalculatingHealth(); //need to figure out a way to save the players health

        position = new float[3];
        position[0] = GlobalVariables.PLAYER.transform.position.x;
        position[1] = GlobalVariables.PLAYER.transform.position.y;
        position[2] = GlobalVariables.PLAYER.transform.position.z;
    }

}
