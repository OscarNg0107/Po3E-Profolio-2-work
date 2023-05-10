using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public AudioClip wavingClip;
    public float speedDampTime = 0.01f;
    public float sensitivityX = 1.0f;
    public float animationSpeed = 1.5f;
    public AudioSource on_rock_walk_audio;
    public AudioSource on_rock_run_audio;
    public Camera follow_camera;
    public float rotateSpeed = 1;

    Vector3 direction;

    private float elapsedTime = 0;
    private bool noBackMov = true;
    private float desiredDuration = 0.5f;

    private Animator anim;
    private HashIDs hash;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Awake()
    {
        anim = GetComponent<Animator>();
        anim.SetLayerWeight(1, 1f);
        hash = GameObject.FindGameObjectWithTag("GameController").GetComponent<HashIDs>();
    }

    private void FixedUpdate()
    {
        elapsedTime += Time.deltaTime;
        float v = Input.GetAxis("Vertical");
        float h = Input.GetAxis("Horizontal");
        bool sneak = Input.GetButton("Sneak");
        bool run = Input.GetButton("Run");
        float turn = Input.GetAxis("Turn");
        bool jump = Input.GetButton("Jump");
        MovementManagement(v, h, sneak, run, jump);
    }

    void Update()
    {
        bool wave = Input.GetButtonDown("Wave");
        anim.SetBool(hash.wavingBool, wave);
        AudioManagement(wave);
    }

    void MovementManagement(float vertical, float horizontal, bool sneaking, bool running, bool jumping) 
    {
        anim.SetBool(hash.sneakingBool, sneaking);

        anim.SetBool(hash.runningBool, running);

        anim.SetBool(hash.jumpingBool, jumping);
        
        if (vertical == 0 && horizontal == 0)
        {
            anim.SetFloat(hash.speedFloat, 0);
            noBackMov = true;
        }

        else
        {
            Rigidbody ourBody = this.GetComponent<Rigidbody>();
            Quaternion rotation;
            if (vertical > 0)
            {
                anim.SetFloat(hash.speedFloat, animationSpeed, speedDampTime, Time.deltaTime);
                rotation = Quaternion.Euler(0, follow_camera.transform.eulerAngles.y, 0);
                ourBody.transform.rotation = Quaternion.Lerp(ourBody.transform.rotation, rotation, Time.deltaTime * rotateSpeed);
            }
            if (vertical < 0)
            {
                anim.SetFloat(hash.speedFloat, animationSpeed, speedDampTime, Time.deltaTime);
                rotation = Quaternion.Euler(0, follow_camera.transform.eulerAngles.y + 180, 0);
                ourBody.transform.rotation = Quaternion.Lerp(ourBody.transform.rotation, rotation, Time.deltaTime * rotateSpeed);
            }
            if (horizontal > 0)
            {
                anim.SetFloat(hash.speedFloat, animationSpeed, speedDampTime, Time.deltaTime);
                rotation = Quaternion.Euler(0, follow_camera.transform.eulerAngles.y + 90, 0);
                ourBody.transform.rotation = Quaternion.Lerp(ourBody.transform.rotation, rotation, Time.deltaTime * rotateSpeed);
            }
            if (horizontal < 0)
            {
                anim.SetFloat(hash.speedFloat, animationSpeed, speedDampTime, Time.deltaTime);
                rotation = Quaternion.Euler(0, follow_camera.transform.eulerAngles.y - 90, 0);
                ourBody.transform.rotation = Quaternion.Lerp(ourBody.transform.rotation, rotation, Time.deltaTime * rotateSpeed);
            }

            Vector3 moveForward = new Vector3(0f, 0f, 0.015f);
            if (running)
            {
                moveForward = new Vector3(0f, 0f, 0.075f);
            }
            moveForward = ourBody.transform.TransformDirection(moveForward);
            ourBody.transform.position += moveForward;
            noBackMov = true;
        }

    }

    void AudioManagement(bool wave) 
    {
        if (anim.GetCurrentAnimatorStateInfo(0).IsName("Walk_front"))
        {
            if (!on_rock_walk_audio.isPlaying)
            {
                on_rock_walk_audio.Play();
            }
        }
        else 
        {
            on_rock_walk_audio.Stop();
        }

        if (anim.GetCurrentAnimatorStateInfo(0).IsName("Run_forward")) 
        {
           if(!on_rock_run_audio.isPlaying) 
            {
                on_rock_run_audio.Play();
            }
        }
        else 
        {
            on_rock_run_audio.Stop();
        }

        if (wave) 
        {
            AudioSource.PlayClipAtPoint(wavingClip, transform.position);
        } 
    }
}
