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

    public ParticleSystem particle;

    private bool inVehicle;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void FixedUpdate()
    {
       
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
        particle.Play();

        character.transform.position = exit.transform.position;
    }
}
