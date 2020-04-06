using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileSpawner : MonoBehaviour
{
    [Tooltip("Add the projectile prefab here")]
    public GameObject projectile;

    private float timeBetweenSpawns; //the amount that is set and equals to the startTimeBetweenSpawns

    private float startTimeBetweenSpawns; //the amount that will count down

    private Transform spawnPoint; //this is the spawn point for the projectile

    [Tooltip("What state is the spawner in right now")]
    public GlobalVariables.SpawnStates spawnStates;

    [Tooltip("Is the spawner waiting?")]
    public bool waiting = true;

    [Tooltip("Add a min amount of time that the projectile will spawn")]
    public int minRange;

    [Tooltip("Add a max amount of time that the projectile will spawn")]
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
                //Debug.Log("Counting Down");
                CountingDown();
                break;
            case GlobalVariables.SpawnStates.Spawning:
                //Debug.Log("Spawning");
                Spawning();
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
        else
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
}
