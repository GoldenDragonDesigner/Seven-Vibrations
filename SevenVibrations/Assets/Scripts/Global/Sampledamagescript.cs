using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sampledamagescript : MonoBehaviour
{
    GameObject player;

    public float damage;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
    }

    public void DealDamage()
    {
        player.GetComponent<HealthScript>().Damage(damage);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject == player)
        {
            DealDamage();
        }
    }
}
