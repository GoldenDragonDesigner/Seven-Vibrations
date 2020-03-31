using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthScript : MonoBehaviour
{
    public int health;
    public bool canhurt = true;

    void Start()
    {
        health = 10;
    }

    public void Damage(int damage)
    {
        if (canhurt)
        {
            health -= damage;
            Debug.Log("took " + damage + " damage.");
            Debug.Log("Unit has " + health + " health remaining.");
        }
        else return;
    }

    void Update()
    {
        health = Mathf.Clamp(health, 0, 10);
    }
}
