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

    [Tooltip("This is the speed at which the projectile will fall to the ground.  It is for reference only")]
    public float speed;

    [Tooltip("This is the minimum amount the speed with be set to in Random.Range.  It needs to be set")]
    public int minRange;

    [Tooltip("This is the maximum amount the speed with be set to in Random.Range.  It needs to be set")]
    public int maxRange;

    [Tooltip("This is the minimum amount of damage the projectile will inflict in Random.Range.  It needs to be set")]
    public float minDamageRange;

    [Tooltip("This is the maximum amount of damage the projectile will inflict in Random.Range.  It needs to be set")]
    public float maxDamageRange;

    [Tooltip("This is the amount of damage that this particular Instaniated projectile will do.  It is for reference only")]
    public float damage;

    [Tooltip("The player game object goes here.  It is set at start and is for reference only")]
    public GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        speed = Random.Range(minRange, maxRange);

        player = GameObject.FindGameObjectWithTag("Player");

        damage = Random.Range(minDamageRange, maxDamageRange);
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
            player.GetComponent<PlayerHealth>().Damage(damage);
        }
    }
}
