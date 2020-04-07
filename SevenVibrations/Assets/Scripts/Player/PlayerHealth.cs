using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    //[Tooltip("Can the Player be hurt?")]
    //public bool canHurt = true;

    //[Tooltip("This is the current health of the player.  For reference only")]
    //public float curHealth;

    //[SerializeField]
    //[Tooltip("This is the Units Max Health.")]
    //public float maxHealth;

    //[SerializeField]
    //[Tooltip("Add the units Health Bar here")]
    //public Slider healthSlider;

    //// Start is called before the first frame update
    //private void Start()
    //{
    //    curHealth = maxHealth;
    //    healthSlider.value = CalculatingHealth();
    //}

    //// Update is called once per frame
    //private void Update()
    //{
    //    healthSlider.value = CalculatingHealth();
    //}

    //public float CalculatingHealth()
    //{
    //    return (curHealth / maxHealth);
    //}




    //public void Damage(float damage)
    //{
    //    if (canHurt)
    //    {
    //        curHealth -= damage;
    //    }
    //    else return;
    //}

    //public void SavePlayer()
    //{
    //    SaveSystem.SavePlayer(this);
    //}

    //public void LoadPlayer()
    //{
    //    PlayerData data = SaveSystem.LoadPlayer();

    //    curHealth = data.health;

    //    Vector3 position;
    //    position.x = data.position[0];
    //    position.y = data.position[1];
    //    position.z = data.position[2];

    //    transform.position = position;
    //}
}
