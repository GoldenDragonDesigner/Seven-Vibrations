using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField]
    protected float maxHealth = 10;
    float currentHealth;
    public DelOneParam onHurt = new DelOneParam();

    public DelOneParam onDeath = new DelOneParam();

    public bool Invulnerable { get; set; } = false;

    protected void Awake()
    {
        ResetHealth();
    }

    public virtual void TakeDamage(float _amount)
    {
        if (Invulnerable) return;
        if (currentHealth == 0) return;

        currentHealth = Mathf.Clamp(currentHealth - _amount, 0, maxHealth);

        if(currentHealth == 0)
        {
            onDeath.CallEvent(0);
        }
        else
        {
            onHurt.CallEvent(currentHealth / maxHealth);
        }
    }

    public virtual void ResetHealth()
    {
        currentHealth = maxHealth;
    }
}
