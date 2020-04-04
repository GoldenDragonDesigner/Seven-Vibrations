using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingProjectile : MonoBehaviour
{
    public Transform projectileDeathLocation;

    public GameObject projectileDeathEffect;

    public GameObject Projectile;

    public float speed;

    public int minRange;

    public int maxRange;

    public float minDamageRange;

    public float maxDamageRange;

    public float damage;

    public GameObject player;

    //public LayerMask ground;

    // Start is called before the first frame update
    void Start()
    {
        speed = Random.Range(minRange, maxRange);

        player = GameObject.FindGameObjectWithTag("Player");

        damage = Random.Range(minDamageRange, maxDamageRange);
        //LayerMask ground = LayerMask.GetMask("Ground");
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
    }

    void Movement()
    {
        transform.position += new Vector3(0, -1 * speed * Time.deltaTime, 0);
        Debug.Log("Projectile moving");
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Ground")
        {
            Destroy(gameObject);
        }
        if(other.gameObject.tag == "Player")
        {
            player.GetComponent<BaseHealth>().Damage(damage);
        }
    }
}
