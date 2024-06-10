using System.Collections;
using System.Collections.Generic;
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

    public LayerMask firstPersonLayerMask;
    public LayerMask thirdPersonLayerMask;

    Vector3 cameraPosition;
    Vector3 FPSView = new Vector3(0, -1.7f, -0.15f);
    Vector3 TPSView = new Vector3(0, -2f, 5f);

    [SerializeField] private float cameraTurnSpeed = 15f;
    private float mouseX;
    private float mouseY;


    void Start()
    {
        cameraMode = CameraMode.FirstPersonView;
        cameraPosition = FPSView;
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
                cameraPosition = FPSView;
                break;
            case CameraMode.ThirdPersonView:
                cameraPosition = TPSView;
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
                    _camera.cullingMask = firstPersonLayerMask;
                    cameraMode = CameraMode.ThirdPersonView;
                    break;
                case CameraMode.ThirdPersonView:
                    _camera.cullingMask = thirdPersonLayerMask;
                    cameraMode = CameraMode.FirstPersonView;
                    break;
            }
            CameraChange();
        }
        
    }
}
