using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;

[System.Serializable]
public class BaseEnemy : MonoBehaviour
{
    [SerializeField]
    protected NavMeshAgent agent;
    [SerializeField]
    protected GameObject player;
    [SerializeField]
    protected GlobalVariables.EnemyStates enemyState;

    protected Vector3 wanderTarget = Vector3.zero;

    [SerializeField]
    protected float wanderRadius;

    [SerializeField]
    protected float wanderDistance;

    [SerializeField]
    protected float wanderJitter;

    protected virtual void Start()
    {
        agent = GetComponent<NavMeshAgent>();

        player = GameObject.FindWithTag("Player");

        enemyState = GlobalVariables.EnemyStates.Wander;
    }

    protected virtual void Update()
    {
        EnemyStates();
    }

    void EnemyStates()
    {
        switch (enemyState)
        {
            case GlobalVariables.EnemyStates.Wander:
                Wander();
                break;
        }
    }

    void Pursue()
    {
        Vector3 targetDir = player.transform.position - transform.position;

        float relativeHeading = Vector3.Angle(transform.forward, transform.TransformVector(player.transform.forward));
        float toTarget = Vector3.Angle(transform.forward, transform.TransformVector(targetDir));

        if(toTarget > 90 && relativeHeading < 20 || player.GetComponent<PlayerMovement>().speed < 0.01f)
        {
            Seek(player.transform.position);
            return;
        }

        float lookAhead = targetDir.magnitude / (agent.speed + player.GetComponent<PlayerMovement>().speed);
        Seek(player.transform.position + player.transform.forward * lookAhead);
    }

    void Evade()
    {
        Vector3 targetDir = player.transform.position - transform.position;
        float lookAhead = targetDir.magnitude / (agent.speed + player.GetComponent<PlayerMovement>().speed);
        Flee(player.transform.position + player.transform.forward * lookAhead);
    }

    void Flee(Vector3 location)
    {
        Vector3 fleeVector = location - transform.position;
        agent.SetDestination(transform.position - fleeVector);
    }

    void Seek(Vector3 location)
    {
        agent.SetDestination(location);
    }

    void Wander()
    {
        wanderTarget += new Vector3(Random.Range(-1.0f, 1.0f) * wanderJitter, 0, Random.Range(-1.0f, 1.0f) * wanderJitter);

        wanderTarget.Normalize();
        wanderTarget *= wanderRadius;

        Vector3 targetLocal = wanderTarget + new Vector3(0, 0, wanderDistance);
        Vector3 targetWorld = gameObject.transform.InverseTransformVector(targetLocal);
        targetWorld = Random.insideUnitSphere * wanderRadius;

        Seek(targetWorld);
    }

    protected virtual void OnTriggerEnter(Collider other)
    {
        //TODO this is where the info for the charmed effect can go
    }

    //protected virtual float CalculatingEnemyHealth()
    //{
    //    return (curEnemyHealth / maxEnemyHealth);
    //}
}
