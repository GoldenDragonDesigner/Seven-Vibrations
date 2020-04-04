using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Sampledamagescript : MonoBehaviour
{
    GameObject player;

    public float damage;

    public float minRange;

    public float maxRange;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");

        damage = Random.Range(minRange, maxRange);
    }

    public void DealDamage()
    {
        player.GetComponent<HealthScript>().Damage(damage);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            DealDamage();
        }
    }
}
