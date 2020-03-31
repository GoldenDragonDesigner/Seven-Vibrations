using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sampledamagescript : MonoBehaviour
{
    GameObject player;
    public float timer;
    bool timeup;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;
        timer = Mathf.Clamp(timer, 0, 10);
        if (timer == 0)
            timeup = true;
        else timeup = false;
        DealDamage(1);
    }

    void DealDamage(int damage)
    {
            if(timeup == true)
        {
            player.GetComponent<HealthScript>().Damage(1);
            timer = 10;
        }
    }

}
