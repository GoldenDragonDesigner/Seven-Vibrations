using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Movement pm; //This is the variable for the player movement script
    public PlayerFollow pf;
    

    public int numberOfObjectsGlowing; //this is the variable for the player voice

    public float curHeatlh = 100;
    public float maxHealth = 100;

    private void Awake()
    {
        GlobalVariables.PLAYER = this;
    }

    public void SavePlayer()
    {
        SaveSystem.SavePlayer(this);
    }

    public void LoadPlayer()
    {
        PlayerData data = SaveSystem.LoadPlayer();

        numberOfObjectsGlowing = data.savedNumOfObjects;
        curHeatlh = data.health;

        Vector3 position;
        position.x = data.position[0];
        position.y = data.position[1];
        position.z = data.position[2];

        transform.position = position;
    }
}
