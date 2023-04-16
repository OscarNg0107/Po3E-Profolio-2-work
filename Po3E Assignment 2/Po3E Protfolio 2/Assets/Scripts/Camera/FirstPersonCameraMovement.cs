using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstPersonCameraMovement : MonoBehaviour
{
    public float mouseSensitivity = 100f;
    public Transform ourBody;

    float xRotation = 0f;


    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }
    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime; 
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 67f);

        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        ourBody.Rotate(Vector3.up * mouseX);
    }
}
