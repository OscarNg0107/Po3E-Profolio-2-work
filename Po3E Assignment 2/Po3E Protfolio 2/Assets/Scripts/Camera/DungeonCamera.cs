using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonCamera : MonoBehaviour
{
    public GameObject target;
    public GameObject digger;
    Vector3 offset;
    public bool retroOrtho = false;
    public Camera thisCamera;
    public GetInAndOutVehicle InAndOutscript;

    private void Start()
    {
        offset = transform.position - target.transform.position;
    }

    private void LateUpdate()
    {
        if (InAndOutscript.GetinVehicleBool())
        {
            Vector3 desiredPosition = digger.transform.position + offset;
            desiredPosition = new Vector3(desiredPosition.x, desiredPosition.y + 20, desiredPosition.z);
            transform.position = desiredPosition;
            transform.LookAt(digger.transform.position);
        }

        else 
        { 
            Vector3 desiredPosition = target.transform.position + offset;
            transform.position = desiredPosition;
            transform.LookAt(target.transform.position);
        }
        

        if (retroOrtho) 
        {
            thisCamera.orthographic = true;
        }
        else 
        {
            thisCamera.orthographic = false;
        }
    }
}
