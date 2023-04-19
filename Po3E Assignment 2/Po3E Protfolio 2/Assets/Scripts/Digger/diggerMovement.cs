using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class diggerMovement : MonoBehaviour
{
    public float speedDampTime = 0.01f;
    public float sensitivityX = 1.0f;
    public float animationSpeed = 1.5f;

    private Animator anim;
    private DiggerHashIDs hash;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        hash = GameObject.FindGameObjectWithTag("DiggerController").GetComponent<DiggerHashIDs>();
        anim.SetLayerWeight(1, 1f);
        anim.SetLayerWeight(2, 1f);
        anim.SetLayerWeight(3, 1f);
    }

    private void FixedUpdate()
    {
        float v = Input.GetAxis("Vertical");
        float h = Input.GetAxis("Horizontal");
        bool spinUp = Input.GetButton("Spin Up");
        bool spinDown = Input.GetButton("Spin Down");
        MovementManager(v, h, spinUp, spinDown);
    }

    void MovementManager(float vertical , float horizontal, bool spinUp, bool spinDown) 
    {
        if (spinUp) 
        {
            anim.SetBool(hash.diggingBool, true);
        }
        if (spinDown) 
        {
            anim.SetBool(hash.diggingBool, false);
        }
        
        if(vertical > 0) 
        {
            anim.SetFloat(hash.speedFloat, animationSpeed, speedDampTime, Time.deltaTime);
            Rigidbody ourBody = this.GetComponent<Rigidbody>();
            
            Vector3 moveForward = new Vector3(0f, 0f, 0.075f);
            
            moveForward = ourBody.transform.TransformDirection(moveForward);
            ourBody.transform.position += moveForward;
            anim.SetBool("Backward", false);

        }
        if(vertical < 0) 
        {
            anim.SetFloat(hash.speedFloat, -1.5f, speedDampTime, Time.deltaTime);
            anim.SetBool("Backward", true);

            Rigidbody ourBody = this.GetComponent<Rigidbody>();

            Vector3 moveBack = new Vector3(0.0f, 0.0f, -0.06f);
            moveBack = ourBody.transform.TransformDirection(moveBack);

            ourBody.transform.position += moveBack;
        }

        if(horizontal < 0) 
        {
            anim.SetBool(hash.turnLeftBool, true);
            anim.SetBool(hash.turnRightBool, false);

            Rigidbody ourBody = this.GetComponent<Rigidbody>();

            Quaternion deltaRotation = Quaternion.Euler(0f, horizontal * sensitivityX, 0f);
            ourBody.MoveRotation(ourBody.rotation * deltaRotation);

        }

        if(horizontal > 0) 
        {
            anim.SetBool(hash.turnRightBool, true);
            anim.SetBool(hash.turnLeftBool, false);

            Rigidbody ourBody = this.GetComponent<Rigidbody>();

            Quaternion deltaRotation = Quaternion.Euler(0f, horizontal * sensitivityX, 0f);
            ourBody.MoveRotation(ourBody.rotation * deltaRotation);
        }

        if(vertical ==0 && horizontal == 0) 
        {
            anim.SetFloat(hash.speedFloat, 0);
            anim.SetBool("Backward", false);
            anim.SetBool(hash.turnLeftBool, false);
            anim.SetBool(hash.turnRightBool, false);
        }
    }
}
