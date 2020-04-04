using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class BaseHealth : MonoBehaviour
{
    [SerializeField]
    protected float curHealth;

    [SerializeField]
    protected float maxHealth;

    [SerializeField]
    protected Slider healthSlider;

    [SerializeField]
    protected bool canHurt = true;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        curHealth = maxHealth;
        Debug.Log(curHealth);
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        healthSlider.value = CalculatingHealth();
    }

    protected virtual float CalculatingHealth()
    {
        return (curHealth / maxHealth);
    }

    public void Damage(float damage)
    {
        if (canHurt)
        {
            curHealth -= damage;
            Debug.Log("took " + damage + " damage.");
            Debug.Log("Unit has " + curHealth + " health remaining.");
        }
    }
}
