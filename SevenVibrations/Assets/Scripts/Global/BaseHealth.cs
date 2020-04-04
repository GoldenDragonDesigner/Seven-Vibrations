using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class BaseHealth : MonoBehaviour
{
    protected float curHealth;

    [SerializeField]
    [Tooltip("This is the Units Max Health.")]
    protected float maxHealth;

    [SerializeField]
    [Tooltip("Add the units Health Bar here")]
    protected Slider healthSlider;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        curHealth = maxHealth;
        healthSlider.value = CalculatingHealth();
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
}
