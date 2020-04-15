using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;

[System.Serializable]
public class BaseEnemy : MonoBehaviour
{
    protected NavMeshAgent agent;

    protected GameObject player;

    [Tooltip("This is the current state the enemy is in")]
    [SerializeField]
    protected GlobalVariables.EnemyStates enemyState;

    protected Vector3 wanderTarget = Vector3.zero;

    [Tooltip("The radius of the enemy that they will wander in by picking a random point inside this radius")]
    [SerializeField]
    protected float wanderRadius;

    [Tooltip("The distance that the enemy will wander around.  Needs to be half of the wander radius")]
    [SerializeField]
    protected float wanderDistance;

    [Tooltip("This needs to be set very low so the enemy will choose only a few points to wander to.  Usually only between 1 to 3")]
    [SerializeField]
    protected float wanderJitter;

    [Tooltip("Has the enemy been alerted?")]
    [SerializeField]
    protected bool enemyAlerted;

    [Tooltip("This allows the enemy to finish a particular behaviour")]
    [SerializeField]
    protected bool coolDown = false;

    [Tooltip("This is an easy one.  Is the enemy dead or not?")]
    [SerializeField]
    protected bool isDead;

    [Tooltip("This is how far the enemy is from the player, FOR REFERENCE ONLY")]
    [SerializeField]
    protected float distanceFromPlayer;

    [Tooltip("Set a distance you want the enemy to be alerted from where the player is at")]
    [SerializeField]
    protected float safeDistanceFromPlayer;

    protected float curEnemyHealth;

    [Tooltip("Set how much health the enemy has")]
    [SerializeField]
    protected float maxEnemyHealth;

    protected Slider enemyHealthSlider;

    protected virtual void Start()
    {
        agent = GetComponent<NavMeshAgent>();

        player = GameObject.FindWithTag("Player");

        enemyState = GlobalVariables.EnemyStates.Wander;

        coolDown = false;

        enemyAlerted = false;

        isDead = false;

        enemyHealthSlider = GetComponentInChildren<Slider>();

        curEnemyHealth = maxEnemyHealth;

        enemyHealthSlider.value = CalculatingEnemyHealth();
    }

    protected virtual void Update()
    {
        enemyHealthSlider.value = CalculatingEnemyHealth();
        EnemyStates();
        //if (!coolDown)
        //{
        //    if (CanSeeTarget() && TargetCanSeePlayer())
        //    {
        //        CleverHide();
        //        coolDown = true;
        //        Invoke("BehaviourCoolDown", 5);
        //    }
        //    else
        //    {
        //        Pursue();
        //    }
        //}
    }

    void BehaviourCoolDown()
    {
        coolDown = false;
    }

    bool TargetCanSeePlayer()
    {
        Vector3 toPlayer = transform.position - player.transform.position;
        float lookingAngle = Vector3.Angle(player.transform.forward, toPlayer);

        if(lookingAngle < 60)
        {
            return true;
        }
        return false;
    }

    bool CanSeeTarget()
    {
        RaycastHit rayCastInfo;
        Vector3 rayToTarget = player.transform.position - transform.position;
        if(Physics.Raycast(transform.position, rayToTarget, out rayCastInfo))
        {
            if(rayCastInfo.transform.gameObject.tag == "Player")
            {
                return true;
            }
        }
        return false;
    }

    void EnemyStates()
    {
        if (!isDead)
        {
            switch (enemyState)
            {
                case GlobalVariables.EnemyStates.Wander:
                    Debug.Log("Wander Function");
                    Wander();
                    break;
                case GlobalVariables.EnemyStates.Pursue:
                    Debug.Log("Pursue Function");
                    Pursue();
                    break;
                case GlobalVariables.EnemyStates.Hide:
                    Debug.Log("Hide Function");
                    Hide();
                    break;
                case GlobalVariables.EnemyStates.CleverHide:
                    Debug.Log("Clever Hide Function");
                    CleverHide();
                    break;
                case GlobalVariables.EnemyStates.Evade:
                    Debug.Log("Evade function");
                    Evade();
                    break;
            }
        }
    }

    void Pursue()
    {
        distanceFromPlayer = Vector3.Distance(transform.position, player.transform.position);

        if (enemyAlerted == true)
        {
            if(distanceFromPlayer <= safeDistanceFromPlayer)
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
            if(distanceFromPlayer > safeDistanceFromPlayer)
            {
                enemyAlerted = false;
                enemyState = GlobalVariables.EnemyStates.Wander;
            }
        }
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
        distanceFromPlayer = Vector3.Distance(transform.position, player.transform.position);
        //Debug.Log(distanceFromPlayer);

        if (!enemyAlerted && distanceFromPlayer >= safeDistanceFromPlayer)
        {
            wanderTarget += new Vector3(Random.Range(-1.0f, 1.0f) * wanderJitter, 0, Random.Range(-1.0f, 1.0f) * wanderJitter);

            wanderTarget.Normalize();
            wanderTarget *= wanderRadius;

            Vector3 targetLocal = wanderTarget + new Vector3(0, 0, wanderDistance);
            Vector3 targetWorld = gameObject.transform.InverseTransformVector(targetLocal);
            targetWorld = Random.insideUnitSphere * wanderRadius;

            Seek(targetWorld);
        }
        else
        {
            enemyAlerted = true;
            enemyState = GlobalVariables.EnemyStates.Pursue;
        }
    }

    void Hide()
    {
        float dist = Mathf.Infinity;
        Vector3 chosenSpot = Vector3.zero;

        for(int i = 0; i < World.Instance.GetHidingSpots().Length; i++)
        {
            Vector3 hideDir = World.Instance.GetHidingSpots()[i].transform.position - player.transform.position;
            Vector3 hidePos = World.Instance.GetHidingSpots()[i].transform.position + hideDir.normalized * 10;

            if(Vector3.Distance(transform.position, hidePos) < dist)
            {
                chosenSpot = hidePos;
                dist = Vector3.Distance(transform.position, hidePos);
            }
        }
        Seek(chosenSpot);
    }

    void CleverHide()
    {
        float dist = Mathf.Infinity;
        Vector3 chosenSpot = Vector3.zero;
        Vector3 chosenDir = Vector3.zero;
        GameObject chosenGO = World.Instance.GetHidingSpots()[0];

        for (int i = 0; i < World.Instance.GetHidingSpots().Length; i++)
        {
            Vector3 hideDir = World.Instance.GetHidingSpots()[i].transform.position - player.transform.position;
            Vector3 hidePos = World.Instance.GetHidingSpots()[i].transform.position + hideDir.normalized * 10;

            if (Vector3.Distance(transform.position, hidePos) < dist)
            {
                chosenSpot = hidePos;
                chosenDir = hideDir;
                chosenGO = World.Instance.GetHidingSpots()[i];
                dist = Vector3.Distance(transform.position, hidePos);
            }
        }

        Collider hideCol = chosenGO.GetComponent<Collider>();
        Ray backRay = new Ray(chosenSpot, -chosenDir.normalized);
        RaycastHit info;
        float distance = 100.0f;
        hideCol.Raycast(backRay, out info, distance);
        Debug.DrawRay(player.transform.position, hideCol.transform.position, Color.red);

        Seek(info.point + chosenDir.normalized * 2);
    }

    protected virtual void OnTriggerEnter(Collider other)
    {
        //TODO this is where the info for the charmed effect can go and linked to the flee or hide function do not have that set up yet
    }


    protected virtual float CalculatingEnemyHealth()
    {
        return (curEnemyHealth / maxEnemyHealth);
    }
}
