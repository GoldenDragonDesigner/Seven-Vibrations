using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    [Tooltip("Can the Player be hurt?")]
    public bool canHurt = true;

    //[Tooltip("This is the current health of the player.  For reference only")]
    private float curHealth;

    [SerializeField]
    [Tooltip("This is the Units Max Health.")]
    public float maxHealth;

    private Slider healthSlider;

    private PlayerMovement playerMove;

    private GameObject forcefield;

    private void Awake()
    {
        GlobalVariables.PLAYER = this;

        playerMove = GetComponent<PlayerMovement>();

        healthSlider = GameObject.Find("PlayerCapsule").GetComponentInChildren<Slider>();

        forcefield = transform.GetChild(2).gameObject;
    }

    private void Start()
    {
        curHealth = maxHealth;
        healthSlider.value = CalculatingHealth();
        forcefield.SetActive(false);
    }

    public void SavePlayer()
    {
        SaveSystem.SavePlayer(this);
    }

    public void LoadPlayer()
    {
        PlayerData data = SaveSystem.LoadPlayer();

        curHealth = data.health; //need to figure out how to save the players health info

        Vector3 position;
        position.x = data.position[0];
        position.y = data.position[1];
        position.z = data.position[2];

        transform.position = position;
    }

    private void Update()
    {
        healthSlider.value = CalculatingHealth();

        if (Input.GetKeyDown(KeyCode.K))
        {
            forcefield.SetActive(true);
            if(forcefield == true)
            {
                canHurt = false;
                Debug.Log("Player taking no damage");
            }
        }
        if (Input.GetKeyDown(KeyCode.U))
        {
            forcefield.SetActive(false);
            canHurt = true;
        }
    }

    public float CalculatingHealth()
    {
        return (curHealth / maxHealth);
    }

    public void Damage(float damage)
    {
        if (canHurt)
        {
            curHealth -= damage;
        }
        else return;
    }
}
