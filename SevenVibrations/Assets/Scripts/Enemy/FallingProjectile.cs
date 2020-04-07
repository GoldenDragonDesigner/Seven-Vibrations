using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingProjectile : MonoBehaviour
{
    [Tooltip("This will be the projectile Game Objects Transform")]
    public Transform projectileDeathLocation;

    [Tooltip("This is the death effect for the projectile")]
    public GameObject projectileDeathEffect;

    [Tooltip("This is the projectile Game Object itself")]
    public GameObject Projectile;

    //[Tooltip("This is the speed at which the projectile will fall to the ground.  It is for reference only")]
    private float speed;
    [Header("Speed")]
    [Tooltip("This is the minimum amount the speed with be set to in Random.Range.  It needs to be set")]
    public int minRange;

    [Tooltip("This is the maximum amount the speed with be set to in Random.Range.  It needs to be set")]
    public int maxRange;

    [Header("Damage")]
    [Tooltip("This is the minimum amount of damage the projectile will inflict in Random.Range.  It needs to be set")]
    public float minDamageRange;

    [Tooltip("This is the maximum amount of damage the projectile will inflict in Random.Range.  It needs to be set")]
    public float maxDamageRange;

    //[Tooltip("This is the amount of damage that this particular Instaniated projectile will do.  It is for reference only")]
    private float damage;

    [Tooltip("The player game object goes here.  It is set at start and is for reference only")]
    public GameObject player;

    public bool hasHit;

    // Start is called before the first frame update
    void Start()
    {
        speed = Random.Range(minRange, maxRange);
        //Debug.Log(speed);

        player = GameObject.FindGameObjectWithTag("Player");
        //Debug.Log("Getting the player");

        damage = Random.Range(minDamageRange, maxDamageRange);
        //Debug.Log(damage);

        hasHit = false;
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
    }

    void Movement()
    {
        transform.position += new Vector3(0, -1 * speed * Time.deltaTime, 0);
        //Debug.Log("Projectile moving");
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Ground" || other.gameObject.tag == "DeathBarrier")
        {
            hasHit = true;
            //Debug.Log("Destorying the projectile");
            Explosion();
        }
        if(other.gameObject.tag == "Player")
        {
            hasHit = true;

            player.GetComponent<Player>().Damage(damage);
            //Debug.Log("Damaging the player with " + damage + " amount");
            Explosion();
        }
    }

    public void Explosion()
    {
        if(hasHit == true)
        {
            //Debug.Log("Instantiating the explosion");
            GameObject explodeFX = Instantiate(projectileDeathEffect, projectileDeathLocation.transform.position, projectileDeathLocation.transform.rotation);
            Destroy(explodeFX, 3f);
            Destroy(gameObject);
            //Debug.Log("Projectile destroyed");
        }
    }
}
