using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetInAndOutVehicle : MonoBehaviour
{
    public GameObject character = null;
    public PlayerMovement characterController;
    public Camera PlayerCamera;

    public diggerMovement DiggerController;
    public GameObject exit = null;
    public Camera DiggerCamera;
    public GameObject getInTrigger;
    public Animator Digger_anim;
    private DiggerHashIDs hash;
    ParticleSystem[] dusttrails;
    public GameObject dust;

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
        bool getInAndOut = Input.GetButtonDown("InAndOutVehicle");
        if (getInAndOut) 
        {
            if (!inVehicle && Vector3.Distance(character.transform.position, exit.transform.position) < 0.5f) 
            {
                character.SetActive(!character.activeSelf);
                GetIn();
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
        DiggerCamera.enabled = true;
        PlayerCamera.enabled = false;
        character.SetActive(false);
        characterController.enabled = false;
        DiggerController.enabled = true;
        inVehicle = true;
        particle.Stop();
    }
    void GetOut() 
    {
        DiggerCamera.enabled = false;
        PlayerCamera.enabled = true;
        character.SetActive(true);
        characterController.enabled = true;
        DiggerController.enabled = false;
        inVehicle = false;
        Digger_anim.SetBool(hash.diggingBool, false);
        particle.Play();

        Digger_anim.SetFloat(hash.speedFloat, 0);
        Digger_anim.SetBool("Backward", false);
        Digger_anim.SetBool(hash.turnLeftBool, false);
        Digger_anim.SetBool(hash.turnRightBool, false);

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
