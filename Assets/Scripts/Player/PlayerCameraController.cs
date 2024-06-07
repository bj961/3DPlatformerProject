using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerCameraController : MonoBehaviour
{
    [Header("Look")]
    public Transform cameraContainer;
    public float minXLook;
    public float maxXLook;
    private float camCurXRot;
    public float lookSensitivity;
    private Vector2 mouseDelta;


    [Header("Look")]
    public Transform Camera;
    public Transform orientation;
    //public float minXLook;
    //public float maxXLook;
    float xRotation;
    float yRotation;
    public float lookSensitivityX;
    public float lookSensitivityY;





    void Start()
    {
        cameraContainer = transform.Find("CameraContainer").GetComponent<Transform>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }


    private void LateUpdate()
    {
        CameraLook();
    }


    void CameraLook()
    {
        float mouseX = Input.GetAxisRaw("Mouse X") * Time.deltaTime * lookSensitivityX;
        float mouseY = Input.GetAxisRaw("Mouse Y") * Time.deltaTime * lookSensitivityY;

        //Debug.Log(mouseDelta);

        //float mouseX = mouseDelta.x * Time.deltaTime * lookSensitivityX;
        //float mouseY = mouseDelta.y * Time.deltaTime * lookSensitivityY;


        yRotation += mouseX;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, minXLook, maxXLook);


        Camera.rotation = Quaternion.Euler(xRotation, yRotation, 0);
        orientation.rotation = Quaternion.Euler(0, yRotation, 0);
        //transform.rotation = Quaternion.Euler(0, yRotation, 0);
    }



    //void CameraLook() //�����ڷ�
    //{
    //    //���콺 �¿�� �����̸� ���콺��Ÿ�� x�� �����?? ĳ���Ͱ� �¿�� �����̷��� ���� y���� ������ ��
    //    // �׷��� ������ �޴°�, ���콺��Ÿx�� y�� �־��ְ� y�� x�� �־���� ���ϴ� ����� �� �� ����
    //    // -> ����嵨Ÿx���� ���콺 �ΰ��� ���ؼ� y�࿡ �־���
    //    camCurXRot += mouseDelta.y * lookSensitivity;
    //    camCurXRot = Mathf.Clamp(camCurXRot, minXLook, maxXLook); // Clamp() : �ּҰ����� �۾����� �ִ밪 ��ȯ, �ִ밪���� Ŀ���� �ּҰ� ��ȯ

    //    Debug.Log("mouseDelta : " + mouseDelta + "\ncamCurXRot : " + camCurXRot);

    //    cameraContainer.localEulerAngles = new Vector3(-camCurXRot, 0, 0); // - �ٿ��� ���� : ���콺 �̵��� ȸ���� �ݴ�� �׷� (���� �����̼� �� �ٲ����)
    //    transform.eulerAngles += new Vector3(0, mouseDelta.x * lookSensitivity, 0);

    //}

    public void OnLook(InputAction.CallbackContext context)
    {
        mouseDelta = context.ReadValue<Vector2>();
    }
}
