using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    public GameObject target;
    Vector3 offset;
    Vector3 LocalRight;

    //public float mouseSensitivity = 100f;

    private float mouseX;
    private float mouseY;
    private float mouseZ;

    public float maxYAngle = 140;
    public float minYAngle = 90;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        offset = transform.position - target.transform.position;
    }

    private void LateUpdate()
    {
        transform.position = target.transform.position + offset;
        transform.LookAt(target.transform);

        mouseX = Input.GetAxis("Mouse X");
        mouseY = Input.GetAxis("Mouse Y");
        mouseZ = Input.GetAxis("Mouse ScrollWheel");

      
        offset = Quaternion.Euler(0, mouseX, 0) * offset;

        float angleBetween = Vector3.Angle(Vector3.up, transform.forward);
        float dist = Vector3.Distance(target.transform.position, transform.position);
        //Debug.Log(angleBetween);

        //if(angleBetween == Mathf.Clamp(angleBetween, minYAngle, maxYAngle) && mouseY !=0) 
        if(((angleBetween > minYAngle) && (mouseY < 0)) || ((angleBetween < maxYAngle) && (mouseY > 0)))
        {
            offset = Quaternion.AngleAxis(mouseY, transform.right) * offset;
        }

        if (mouseZ > 0)
        {
            offset = Vector3.Scale(offset, new Vector3(1.05f, 1.05f, 1.05f));
        }

        if (mouseZ < 0)
        {
            offset = Vector3.Scale(offset, new Vector3(0.95f, 0.95f, 0.95f));
        }
    }

}
