using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damage : MonoBehaviour
{
    [Tooltip("Enter the min amount of damage you want this to do")]
    public float minDamage;

    [Tooltip("Enter the max amount of damage you this to do")]
    public float maxDamage;

    private float damage;

    private GameObject player;

    private void Start()
    {
        player = GameObject.FindWithTag("Player");
        damage = Random.Range(minDamage, maxDamage);
        Debug.Log("the damage amount is " + damage);
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            player.GetComponent<Player>().Damage(damage);
            //Debug.Log("Damaging the player for " + damage + " damage");
        }
    }

}
