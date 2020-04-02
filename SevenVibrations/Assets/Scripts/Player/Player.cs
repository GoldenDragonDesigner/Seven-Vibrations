using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Movement pm; //This is the variable for the player movement script
    public PlayerFollow pf;

    public HealthScript playerHealth;

    public GameObject forcefield;

    private void Awake()
    {
        GlobalVariables.PLAYER = this;
        playerHealth = GetComponent<HealthScript>();
    }

    private void Start()
    {
        forcefield.SetActive(false);
    }

    public void SavePlayer()
    {
        SaveSystem.SavePlayer(this);
    }

    public void LoadPlayer()
    {
        PlayerData data = SaveSystem.LoadPlayer();

        //curHeatlh = data.health;

        Vector3 position;
        position.x = data.position[0];
        position.y = data.position[1];
        position.z = data.position[2];

        transform.position = position;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            forcefield.SetActive(true);
            if(forcefield == true)
            {
                playerHealth.canhurt = false;
                Debug.Log("Player taking no damage");
            }
        }
        if (Input.GetKeyDown(KeyCode.U))
        {
            forcefield.SetActive(false);
            playerHealth.canhurt = true;
        }
    }
}
