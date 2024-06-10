using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.GraphicsBuffer;

public enum CameraMode
{
    FirstPersonView,
    ThirdPersonView
}

public class PlayerCameraController : MonoBehaviour
{
    public Camera _camera;
    public CameraMode cameraMode;
    public GameObject crossHair;

    public LayerMask firstPersonLayerMask;
    public LayerMask thirdPersonLayerMask;

    Vector3 cameraPosition;
    Vector3 FPSView = new Vector3(0, -1.7f, -0.15f);
    Vector3 TPSView = new Vector3(0, -2f, 5f);

    [SerializeField] private float cameraTurnSpeed = 15f;
    

    void Start()
    {
        cameraMode = CameraMode.FirstPersonView;
        cameraPosition = FPSView;
        crossHair = UIManager.Instance.InGameUI.transform.Find("CrossHair").gameObject;
    }


    void LateUpdate()
    {
        CameraFollow();
    }


    void CameraFollow()
    {
        Vector3 Distance = cameraPosition;
        _camera.transform.position = transform.position - _camera.gameObject.transform.rotation * cameraPosition;// Distance;
        
    }

    void CameraChange()
    {
        switch (cameraMode)
        {
            case CameraMode.FirstPersonView:
                _camera.cullingMask = firstPersonLayerMask;
                cameraPosition = FPSView;
                crossHair.SetActive(true);
                break;
            case CameraMode.ThirdPersonView:
                _camera.cullingMask = thirdPersonLayerMask;
                cameraPosition = TPSView;
                crossHair.SetActive(false);
                break;
        }
    }

    public void OnCameraChange(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started)
        {
            switch (cameraMode)
            {
                case CameraMode.FirstPersonView:
                    cameraMode = CameraMode.ThirdPersonView;
                    break;
                case CameraMode.ThirdPersonView:
                    cameraMode = CameraMode.FirstPersonView;
                    break;
            }
            CameraChange();
        }
        
    }
}
