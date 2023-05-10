using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetInAndOutVehicle : MonoBehaviour
{
    public GameObject character = null;
    public PlayerMovement characterController;
    public Camera PlayerCamera;
    public FollowCamera PlayerFollowCamera;
    public GameObject charaterUI;

    public diggerMovement DiggerController;
    public GameObject exit = null;
    public Camera DiggerCamera;
    public GameObject getInTrigger;
    public Animator Digger_anim;
    private DiggerHashIDs hash;
    ParticleSystem[] dusttrails;
    public GameObject dust;
    public FollowCamera DiggerFollowCamera;
    public GameObject diggerUI;

    public GameObject buttonHint;

    public ParticleSystem particle;

    private bool inVehicle;

    private void Start()
    {
        dusttrails = dust.GetComponentsInChildren<ParticleSystem>();
    }
    private void Awake()
    {
        hash = GameObject.FindGameObjectWithTag("DiggerController").GetComponent<DiggerHashIDs>();
    }
    // Update is called once per frame
    void Update()
    {
        if(Vector3.Distance(character.transform.position, exit.transform.position) < 0.5f && inVehicle != true) 
        {
            buttonHint.SetActive(true);
        }
        else 
        {
            buttonHint.SetActive(false);
        }
        bool getInAndOut = Input.GetButtonDown("InAndOutVehicle");
        if (getInAndOut) 
        {
            if (!inVehicle && Vector3.Distance(character.transform.position, exit.transform.position) < 0.5f) 
            {
                character.SetActive(!character.activeSelf);
                GetIn();
                buttonHint.SetActive(false);
            }
            else if(inVehicle) 
            {
                character.SetActive(!character.activeSelf);
                GetOut();
            }
        }
    }

    void GetIn() 
    {
        Digger_anim.SetBool(hash.startBool, true);
        DiggerCamera.transform.rotation = Quaternion.Euler(0, 0, 0);
        DiggerCamera.enabled = true;
        PlayerCamera.enabled = false;
        DiggerFollowCamera.enabled = true;
        PlayerFollowCamera.enabled = false;
        character.SetActive(false);
        characterController.enabled = false;
        DiggerController.enabled = true;
        inVehicle = true;
        particle.Stop();
        charaterUI.SetActive(false);
        diggerUI.SetActive(true);
    }
    void GetOut() 
    {
        Digger_anim.SetBool(hash.startBool, false);
        DiggerCamera.enabled = false;
        PlayerCamera.enabled = true;
        DiggerFollowCamera.enabled = false;
        PlayerFollowCamera.enabled = true;
        character.SetActive(true);
        characterController.enabled = true;
        DiggerController.enabled = false;
        inVehicle = false;
        Digger_anim.SetBool(hash.diggingBool, false);
        particle.Play();
        charaterUI.SetActive(true);
        diggerUI.SetActive(false);

        Digger_anim.SetFloat(hash.speedFloat, 0);
        Digger_anim.SetBool("Backward", false);
        Digger_anim.SetBool(hash.turnLeftBool, false);
        Digger_anim.SetBool(hash.turnRightBool, false);
        Digger_anim.SetFloat(hash.cuttingWheelRotationSpeedFloat, 0f);

        foreach (ParticleSystem dusttrail in dusttrails)
        {
            if (dusttrail.isPlaying)
            {
                dusttrail.Stop();
            }
        }

        character.transform.position = exit.transform.position;
    }
}
