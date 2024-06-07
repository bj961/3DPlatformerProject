using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCam : MonoBehaviour
{
    [Header("Look")]
    public Transform Camera;
    public Transform orientation;
    public float minXLook;
    public float maxXLook;
    float xRotation;
    float yRotation;
    public float lookSensitivityX;
    public float lookSensitivityY;


    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        float mouseX = Input.GetAxisRaw("Mouse X") * Time.deltaTime * lookSensitivityX;
        float mouseY = Input.GetAxisRaw("Mouse Y") * Time.deltaTime * lookSensitivityY;

        //Debug.Log(mouseDelta);

        //float mouseX = mouseDelta.x * Time.deltaTime * lookSensitivityX;
        //float mouseY = mouseDelta.y * Time.deltaTime * lookSensitivityY;


        yRotation += mouseX;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, minXLook, maxXLook);


        Camera.transform.rotation = Quaternion.Euler(xRotation, yRotation, 0);
        orientation.rotation = Quaternion.Euler(0, yRotation, 0);
    }
}
