using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileSpawner : MonoBehaviour
{
    public GameObject projectile;

    public float timeBetweenSpawns;

    public float startTimeBetweenSpawns;

    public Transform spawnPoint;

    public GlobalVariables.SpawnStates spawnStates;

    public bool waiting = true;

    public int minRange;

    public int maxRange;

    // Start is called before the first frame update
    void Start()
    {
        startTimeBetweenSpawns = timeBetweenSpawns = Random.Range(minRange, maxRange);
        spawnStates = GlobalVariables.SpawnStates.CountingDown;
        spawnPoint = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        SpawningStates();
    }

    void SpawningStates()
    {
        switch (spawnStates)
        {
            case GlobalVariables.SpawnStates.CountingDown:
                Debug.Log("Counting Down");
                CountingDown();
                break;
            case GlobalVariables.SpawnStates.Spawning:
                Debug.Log("Spawning");
                Spawning();
                break;
            case GlobalVariables.SpawnStates.Waiting:
                Debug.Log("Waiting");
                //Waiting();
                break;
        }
    }

    void CountingDown()
    {
        if(timeBetweenSpawns <= 0)
        {
            startTimeBetweenSpawns = timeBetweenSpawns = Random.Range(minRange, maxRange);

            waiting = false;

            spawnStates = GlobalVariables.SpawnStates.Spawning;
        }
        else //if(timeBetweenSpawns >= 0)
        {
            timeBetweenSpawns -= Time.deltaTime;
        }

    }

    void Spawning()
    {
        if(waiting == false)
        {
            Instantiate(projectile, new Vector3(spawnPoint.position.x, spawnPoint.position.y, spawnPoint.position.z), Quaternion.identity);
            waiting = true;

            spawnStates = GlobalVariables.SpawnStates.CountingDown;
        }
    }

    //void Waiting()
    //{
    //    if(spawnPoint != null)
    //    {
    //        if(timeBetweenSpawns <= 0)
    //        {
    //            waiting = false;
    //            spawnStates = GlobalVariables.SpawnStates.Spawning;
    //            timeBetweenSpawns = startTimeBetweenSpawns = Random.Range(minRange, maxRange);
    //        }
    //        else
    //        {
    //            waiting = true;
    //            spawnStates = GlobalVariables.SpawnStates.CountingDown;
    //        }
    //    }
    //}
}
