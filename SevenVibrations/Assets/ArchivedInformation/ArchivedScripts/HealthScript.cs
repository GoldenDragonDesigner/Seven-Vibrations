using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthScript : MonoBehaviour
{
    public float maxHealth;
    public float curHealth;
    public bool canhurt = true;

    void Start()
    {
        curHealth = maxHealth;
        Debug.Log(curHealth);
    }

    public void Damage(float damage)
    {
        if (canhurt)
        {
            curHealth -= damage;
            Debug.Log("took " + damage + " damage.");
            Debug.Log("Unit has " + curHealth + " health remaining.");
        }
        else return;
    }

    void Update()
    {
        //curHealth = Mathf.Clamp(curHealth, 0, maxHealth);
        CalculatingHealth();
    }

    float CalculatingHealth()
    {
        return curHealth / maxHealth;
    }
}
