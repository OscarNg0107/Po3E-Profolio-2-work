using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetUpCamera : MonoBehaviour

{
    public Camera PiPCam;
    // Start is called before the first frame update
    void Start()
    {
        GameObject PlayerCharacter = GameObject.FindGameObjectWithTag("Player");
        PiPCam.enabled = false;
    }

    private void Update()
    {
        if (Input.GetKeyUp("t")) 
        {
            PiPCam.enabled = !PiPCam.enabled;
        }
    }

}
