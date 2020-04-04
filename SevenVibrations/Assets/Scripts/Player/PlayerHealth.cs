using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : BaseHealth
{
    [Tooltip("Can the Player be hurt?")]
    public bool canHurt = true;

    protected override void Start()
    {
        base.Start();
        Debug.Log(curHealth);
    }

    protected override void Update()
    {
        base.Update();

    }

    public void Damage(float damage)
    {
        if (canHurt)
        {
            curHealth -= damage;
            Debug.Log("took " + damage + " damage.");
            Debug.Log("Unit has " + curHealth + " health remaining.");
        }
        else return;
    }
}
