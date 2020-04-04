using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    //public Movement pM;

    public PlayerMovement playerMove;

    public PlayerHealth playerHealth;

    public GameObject forcefield;

    private void Awake()
    {
        GlobalVariables.PLAYER = this;
        playerHealth = GetComponent<PlayerHealth>();

        playerMove = GetComponent<PlayerMovement>();
        //pm = GetComponent<Movement>();
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
                playerHealth.canHurt = false;
                Debug.Log("Player taking no damage");
            }
        }
        if (Input.GetKeyDown(KeyCode.U))
        {
            forcefield.SetActive(false);
            playerHealth.canHurt = true;
        }
    }
}
