using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Movement motor;
    public float moveMagnitude = 0.05f;
    public float speed = 0.7f;
    public float turnSpeed = 10f;
    public float speedJump = 20f;

    private float speedMoveMultiplier = 1f;

    private Vector3 direction;

    private Animator anim;
    private Camera mainCamera;

    private string ParamterState = "State";

    private void Awake()
    {
        motor = GetComponent<Movement>();
        anim = GetComponent<Animator>();
    }

    // Start is called before the first frame update
    void Start()
    {
        //anim.applyRootMotion = false;
        mainCamera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        MovementAndJumping();
    }

    private Vector3 MoveDirection
    {
        get { return direction; }
        set { direction = value * speedMoveMultiplier;

            if(direction.magnitude > 0.1f)
            {
                var newRotation = Quaternion.LookRotation(direction);

                transform.rotation = Quaternion.Slerp(transform.rotation, newRotation, Time.deltaTime * turnSpeed);
            }

            direction *= speed * (Vector3.Dot(transform.forward, direction) + 1f) * 5f;
            motor.Move(direction);

            //AnimationMove(motor.charcontroller.velocity.magnitude * 0.1f);

        }

    }

    void Moving(Vector3 dir, float mult)
    {
        speedMoveMultiplier = 1 * mult;
        MoveDirection = dir;
    }

    void Jump()
    {
        motor.Jump(speedJump);
    }

    void MovementAndJumping()
    {
        Vector3 moveInput = Vector3.zero;
        Vector3 forward = Quaternion.AngleAxis(-90, Vector3.up) * mainCamera.transform.right;

        moveInput += forward * Input.GetAxis("Vertical");
        //print(Input.GetAxis("Vertical"));
        moveInput += mainCamera.transform.right * Input.GetAxis("Horizontal");

        moveInput.Normalize();
        Moving(moveInput.normalized, 1f);

        if (Input.GetKey(KeyCode.Space) || Input.GetButton("Jump"))
        {
            Jump();
        }
    }
}
