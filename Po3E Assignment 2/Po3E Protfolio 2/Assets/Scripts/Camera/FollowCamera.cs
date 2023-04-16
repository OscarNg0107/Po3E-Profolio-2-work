using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    public GameObject target;
    Vector3 offset;

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

        float desiredAngle = target.transform.eulerAngles.y;
        Quaternion rotation = Quaternion.Euler(0, desiredAngle, 0);
        transform.position = target.transform.position + (rotation * offset);
        transform.LookAt(target.transform);

        mouseX = Input.GetAxis("Mouse X");
        mouseY = Input.GetAxis("Mouse Y");
        mouseZ = Input.GetAxis("Mouse ScrollWheel");

      
        offset = Quaternion.Euler(0, mouseX, 0) * offset;

        float angleBetwwen = Vector3.Angle(Vector3.up, transform.forward);
        float dist = Vector3.Distance(target.transform.position, transform.position);

        if (((angleBetwwen > minYAngle) && (mouseY < 0)) || ((angleBetwwen < maxYAngle) && (mouseY > 0)))
        {
            Vector3 LocalRight = target.transform.worldToLocalMatrix.MultiplyVector(transform.right);
            offset = Quaternion.AngleAxis(mouseY, LocalRight) * offset;

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
