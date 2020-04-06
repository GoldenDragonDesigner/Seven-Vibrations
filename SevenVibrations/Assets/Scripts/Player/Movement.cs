using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    private float gravityMulitpiler = 1;
    private float lerpTime = 10f;

    private Vector3 moveDir = Vector3.zero;
    private Vector3 targetDir = Vector3.zero;

    private float fallVelocity = 0f;

    private CharacterController charController;

    private float disToGround = 0.1f;

    private bool isGrounded;

    private Collider myCollider;

    private void Awake()
    {
        charController = GetComponent<CharacterController>();
        myCollider = GetComponent<Collider>();
    }

    private void Start()
    {
        disToGround = myCollider.bounds.extents.y;

    }

    private void Update()
    {
        isGrounded = OnGroundCheck();

        moveDir = Vector3.Lerp(moveDir, targetDir, Time.deltaTime * lerpTime);
        moveDir.y = fallVelocity;

        charController.Move(moveDir * Time.deltaTime);

        if (!isGrounded)
        {
            fallVelocity -= 90f * gravityMulitpiler * Time.deltaTime;
        }
    }

    public bool OnGroundCheck()
    {
        RaycastHit hit;

        if (charController.isGrounded)
        {
            return true;
        }

        if(Physics.Raycast(myCollider.bounds.center, -Vector3.up, out hit, disToGround + 0.1f))
        {
            return true;
        }
        return false;
    }

    public void Move(Vector3 dir)
    {
        targetDir = dir;
    }

    public void Stop()
    {
        moveDir = Vector3.zero;
        targetDir = Vector3.zero;
    }

    public void Jump(float jumpSpeed)
    {
        if (isGrounded)
        {
            fallVelocity = jumpSpeed;
        }
    }




















    //public float moveSpeed = 4;
    //public float jumpForce;
    //public float gravity = 8;
    //public float rotSpeed;

    //Vector3 moveDir = Vector3.zero;

    //CharacterController controller;

    //public bool hit = false;
    //Animator anim;

    //private void Awake()
    //{
    //    this.enabled = true;
    //    Time.timeScale = 1f;
    //}

    //// Start is called before the first frame update
    //void Start()
    //{
    //    controller = GetComponent<CharacterController>();
    //    anim = GetComponent<Animator>();
    //    GlobalVariables.PLAYER.pm = this;
    //}

    //// Update is called once per frame
    //void Update()
    //{
    //    ForwardMovement();
    //    BackwardsMovement();
    //    controller.Move(moveDir * Time.deltaTime);
    //    moveDir.y -= gravity * Time.deltaTime;
    //}

    //void ForwardMovement()
    //{
    //    if (Input.GetKey(KeyCode.W))
    //    {
    //        //anim.SetInteger("Condition", 1);
    //        moveDir = new Vector3(0, 0, 1);
    //        moveDir *= moveSpeed;
    //        moveDir = transform.TransformDirection(moveDir);
    //        //controller.Move(moveDir * Time.deltaTime);
    //    }
    //    else
    //    {
    //        IdleCondition();
    //    }
    //}

    //void BackwardsMovement()
    //{
    //    if (Input.GetKey(KeyCode.S))
    //    {
    //        //anim.SetInteger("Condition", 2);
    //        moveDir = new Vector3(0, 0, -1);
    //        moveDir *= moveSpeed;
    //        moveDir = transform.TransformDirection(moveDir);
    //        //controller.Move(moveDir * Time.deltaTime);
    //    }
    //    else
    //    {
    //        IdleCondition();
    //    }
    //}

    //void IdleCondition()
    //{
    //    //anim.SetInteger("Condition", 0);
    //    moveDir = new Vector3(0,0,0);
    //}
    //var mouseInput = Input.GetAxis("Mouse X");

    //transform.Rotate(new Vector3(0, mouseInput * rotSpeed, 0), Space.Self);


    //if (Input.GetKey(KeyCode.W))//forward movement
    //{
    //    anim.SetInteger("Condition", 1);
    //    moveDir = new Vector3(0, 0, 1);
    //    moveDir *= moveSpeed;
    //    moveDir = transform.TransformDirection(moveDir);
    //}
    //else
    //{
    //    IdleCondition();
    //}

    //if (Input.GetKeyUp(KeyCode.W))
    //{
    //    anim.SetInteger("Condition", 0);
    //    moveDir = new Vector3(0, 0, 0);
    //}

    //if (Input.GetKey(KeyCode.A))
    //{
    //    anim.SetInteger("Condition", 2);
    //    moveDir = new Vector3(-1, 0, 0);
    //    moveDir *= moveSpeed;
    //    moveDir = transform.TransformDirection(moveDir);
    //    controller.Move(moveDir * Time.deltaTime);
    //}
    //if (Input.GetKeyUp(KeyCode.A))
    //{
    //    anim.SetInteger("Condition", 0);
    //    moveDir = new Vector3(0, 0, 0);
    //}

    //if (Input.GetKey(KeyCode.D))
    //{
    //    //anim.SetInteger("Condition", 6);
    //    moveDir = new Vector3(1, 0, 0);
    //    moveDir *= moveSpeed;
    //    moveDir = transform.TransformDirection(moveDir);
    //    controller.Move(moveDir * Time.deltaTime);
    //}
    //if (Input.GetKeyUp(KeyCode.D))
    //{
    //    //anim.SetInteger("Condition", 0);
    //    moveDir = new Vector3(0, 0, 0);
    //}

    //if (Input.GetKey(KeyCode.S))//backwards movement
    //{
    //    anim.SetInteger("Condition", 2);
    //    moveDir = new Vector3(0, 0, -1);
    //    moveDir *= moveSpeed;
    //    moveDir = transform.TransformDirection(moveDir);
    //}
    //else
    //{
    //    IdleCondition();
    //}
    //if (Input.GetKeyUp(KeyCode.S))
    //{
    //    anim.SetInteger("Condition", 0);
    //    moveDir = new Vector3(0, 0, 0);
    //}

    //if (Input.GetButton("Jump"))
    //{
    //    moveDir.y = jumpForce;
    //    //anim.SetInteger("Condition", 2);
    //}

    //if (Input.GetKeyUp(KeyCode.Space) && controller.isGrounded)
    //{
    //    moveDir.y = gravity;
    //    //anim.SetInteger("Condition", 0);
    //}
}
