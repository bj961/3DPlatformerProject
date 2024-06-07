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



    //void CameraLook() //강의자료
    //{
    //    //마우스 좌우로 움직이면 마우스델타의 x가 변경됨?? 캐릭터가 좌우로 움직이려면 축을 y축을 돌려야 함
    //    // 그래서 실제로 받는값, 마우스델타x는 y에 넣어주고 y는 x에 넣어줘야 원하는 결과를 낼 수 있음
    //    // -> 마우드델타x값에 마우스 민감도 곱해서 y축에 넣어줌
    //    camCurXRot += mouseDelta.y * lookSensitivity;
    //    camCurXRot = Mathf.Clamp(camCurXRot, minXLook, maxXLook); // Clamp() : 최소값보다 작아지면 최대값 반환, 최대값보다 커지면 최소값 반환

    //    Debug.Log("mouseDelta : " + mouseDelta + "\ncamCurXRot : " + camCurXRot);

    //    cameraContainer.localEulerAngles = new Vector3(-camCurXRot, 0, 0); // - 붙여준 이유 : 마우스 이동과 회전이 반대라서 그럼 (실제 로테이션 값 바꿔봐라)
    //    transform.eulerAngles += new Vector3(0, mouseDelta.x * lookSensitivity, 0);

    //}

    public void OnLook(InputAction.CallbackContext context)
    {
        mouseDelta = context.ReadValue<Vector2>();
    }
}
