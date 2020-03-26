using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFollow : MonoBehaviour
{
    public bool isControlable;
    private Vector3 screenPoint;
    public Transform target;
    public float distance = 5.0f;
    public float prevDistance;

    public float yMinLimit = -80f;
    public float yMaxLimit = 80f;

    public float distanceMin = .5f;
    public float distanceMax = 15f;

    public float smoothTime = 2f;

    public float rotationYAxis = 0.0f;
    public float offSet = 2;
    float rotationXAxis = 0.0f;

    float velocityX = 0.0f;
    float velocityY = 0.0f;
    Quaternion rotation;
    BoxCollider col;
    public Material Outline;

    public LayerMask layerMask;

    // Use this for initialization
    void Start()
    {
        GameManager.gm.playerFollow = this;
        //TODO Create global variable script for player settings
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        Vector3 angles = transform.eulerAngles;
        rotationYAxis = (rotationYAxis == 0) ? angles.y : rotationYAxis;
        rotationXAxis = angles.x;
        col = GetComponent<BoxCollider>();

        Rigidbody rigidbody = GetComponent<Rigidbody>();

        // Make the rigid body not change rotation
        if (rigidbody)
        {
            rigidbody.freezeRotation = true;
        }
    }

    void Update()
    {
        //if (GlobalVariables.PLAYER.pa.enemiesInRange.Count > 0)
        //{
        //    foreach (EnemyObject enemy in GlobalVariables.PLAYER.pa.enemiesInRange)
        //    {
        //        if (GlobalVariables.PLAYER.pa.selectedEnemy != null)
        //        {
        //            float nextDistance = Vector3.Distance(enemy.enemy.me.transform.position, GlobalVariables.PLAYER.transform.position);
        //            float currentDistance = Vector3.Distance(GlobalVariables.PLAYER.pa.selectedEnemy.enemy.me.transform.position, GlobalVariables.PLAYER.transform.position);
        //            if (nextDistance < currentDistance)
        //            {
        //                List<Material> matList = new List<Material>();
        //                matList.AddRange(GlobalVariables.PLAYER.pa.selectedEnemy.enemy.me.GetComponent<MeshRenderer>().materials);
        //                matList.Remove(GlobalVariables.PLAYER.pa.selectedEnemy.enemy.me.GetComponent<MeshRenderer>().materials[GlobalVariables.PLAYER.pa.selectedEnemy.enemy.me.GetComponent<MeshRenderer>().materials.Length - 1]);
        //                GlobalVariables.PLAYER.pa.selectedEnemy.enemy.me.GetComponent<MeshRenderer>().materials = matList.ToArray();

        //                GlobalVariables.PLAYER.pa.selectedEnemy = enemy;

        //                matList = new List<Material>();
        //                matList.AddRange(enemy.enemy.me.GetComponent<MeshRenderer>().materials);
        //                matList.Add(Outline);
        //                enemy.enemy.me.GetComponent<MeshRenderer>().materials = matList.ToArray();
        //            }
        //        }
        //        else
        //        {
        //            GlobalVariables.PLAYER.pa.selectedEnemy = enemy;

        //            List<Material> matList = new List<Material>();
        //            matList.AddRange(enemy.enemy.me.GetComponent<MeshRenderer>().materials);
        //            matList.Add(Outline);
        //            enemy.enemy.me.GetComponent<MeshRenderer>().materials = matList.ToArray();
        //        }
        //    }
        //}

        if (Input.GetKeyDown(KeyCode.Tab))
        {
            //GlobalVariables.ChangeMouse();
        }
        if (target)
        {
            if (isControlable)
            {   //GlobalVariables.HorizontalMouseSpeed *
                velocityX += 15 * Input.GetAxis("Mouse X") * Time.deltaTime;
                //GlobalVariables.VerticalMouseSpeed * 
                velocityY += 15 * Input.GetAxis("Mouse Y") * Time.deltaTime;

                if (distance > distanceMax)
                {
                    distance = distanceMax;
                }
                else if (distance < distanceMin)
                {
                    distance = distanceMin;
                }
            }
            rotationYAxis += velocityX;
            rotationXAxis -= velocityY;

            rotationXAxis = ClampAngle(rotationXAxis, yMinLimit, yMaxLimit);

            Quaternion toRotation = Quaternion.Euler(rotationXAxis, rotationYAxis, 0);
            rotation = toRotation;

            Vector3 negDistance = new Vector3(0.0f, 0.0f, -distance);

            transform.rotation = rotation;
            target.rotation = Quaternion.Euler(0, rotation.eulerAngles.y, 0);
            transform.position = CheckMask(rotation, negDistance);

            velocityX = Mathf.Lerp(velocityX, 0, Time.deltaTime * smoothTime);
            velocityY = Mathf.Lerp(velocityY, 0, Time.deltaTime * smoothTime);

            //col.center = new Vector3(col.center.x, col.center.y, (GlobalVariables.PLAYER.pa.ammoType.BaseRange / 2) + 1 + distance);
            //col.size = new Vector3(col.size.x, col.size.y, GlobalVariables.PLAYER.pa.ammoType.BaseRange);
        }
        else
        {
            target = GlobalVariables.PLAYER.transform;
        }
    }

    private void LateUpdate()
    {
        /*if (!GlobalVariables.PLAYER.anim.GetBool("Sliding"))
        {
            for (int i = 0; i < head.Length; i++)
            {
                head[i].rotation = Quaternion.Euler(head[i].eulerAngles.x, head[i].eulerAngles.y, -(rotationXAxis / 4) - 90);
            }
        }*/
    }

    bool prevSaved = false;
    Vector3 CheckMask(Quaternion rotation, Vector3 negDistance)
    {
        Vector3 newPosition = new Vector3(transform.position.x, -transform.up.y * transform.position.y - 2, transform.position.z);
        //Debug.DrawRay(newPosition, transform.forward * 10, Color.red);
        Vector3 targetPos = new Vector3(target.position.x, target.position.y + (target.GetComponent<CapsuleCollider>().height / 2),
            target.position.z);
        RaycastHit hit = new RaycastHit();
        if (Physics.Linecast(targetPos, transform.position, out hit, layerMask))
        {
            if (!prevSaved)
            {
                prevSaved = true;
                prevDistance = distance;
            }
            //the x and z coordinates are pushed away from the wall by hit.normal.
            //the y coordinate stays the same.
            Vector3 newT = new Vector3(target.position.x, target.position.y + offSet, target.position.z);
            float temp = Mathf.Clamp(distance - 1, distanceMin, distanceMax);
            distance = Mathf.Lerp(distance, temp, Time.deltaTime * 25);
            newPosition = rotation * negDistance + newT;
            //Debug.Log(hit.transform.name);
        }
        else if (Physics.Linecast(transform.position, transform.position - transform.forward * 2, out hit, layerMask))
        {
            Vector3 newT = new Vector3(target.position.x, target.position.y + offSet, target.position.z);
            newPosition = rotation * negDistance + newT;
        }
        else
        {
            Vector3 newT = new Vector3(target.position.x, target.position.y + offSet, target.position.z);
            newPosition = rotation * negDistance + newT;
            if (prevSaved)
            {
                distance = Mathf.Lerp(distance, prevDistance, Time.deltaTime * 15);
                if (distance + 0.5f >= prevDistance)
                {
                    distance = prevDistance;
                    prevSaved = false;
                }
            }
            if (isControlable)
                distance -= Input.GetAxis("Mouse ScrollWheel") * 5;
        }
        return newPosition;
    }

    public static float ClampAngle(float angle, float min, float max)
    {
        if (angle < -360F)
            angle += 360F;
        if (angle > 360F)
            angle -= 360F;
        return Mathf.Clamp(angle, min, max);
    }

    private void OnTriggerStay(Collider other)
    {
        //EnemyObject enemy = other.GetComponent<EnemyObject>();
        //if (enemy != null && !GlobalVariables.PLAYER.pa.enemiesInRange.Contains(enemy))
        //{
        //    GlobalVariables.PLAYER.pa.enemiesInRange.Add(enemy);
        //}
    }

    private void OnTriggerExit(Collider other)
    {
        //EnemyObject enemy = other.GetComponent<EnemyObject>();
        //if (enemy != null && GlobalVariables.PLAYER.pa.enemiesInRange.Contains(enemy))
        //{
        //    if (GlobalVariables.PLAYER.pa.selectedEnemy == enemy)
        //    {
        //        List<Material> matList = new List<Material>();
        //        matList.AddRange(GlobalVariables.PLAYER.pa.selectedEnemy.enemy.me.GetComponent<MeshRenderer>().materials);
        //        matList.Remove(GlobalVariables.PLAYER.pa.selectedEnemy.enemy.me.GetComponent<MeshRenderer>().materials[GlobalVariables.PLAYER.pa.selectedEnemy.enemy.me.GetComponent<MeshRenderer>().materials.Length - 1]);
        //        GlobalVariables.PLAYER.pa.selectedEnemy.enemy.me.GetComponent<MeshRenderer>().materials = matList.ToArray();

        //        GlobalVariables.PLAYER.pa.selectedEnemy = null;
        //    }
        //    GlobalVariables.PLAYER.pa.enemiesInRange.Remove(enemy);
        //}
    }
}
