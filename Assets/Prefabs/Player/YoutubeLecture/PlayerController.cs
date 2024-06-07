using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    public float defaultMoveSpeed;
    public float moveSpeed;
    public float jumpPower = 80f;
    public float dashSpeed = 10f;
    private Vector2 curMovementInput;
    [SerializeField] private Vector3 beforeDirection;
    public LayerMask groundLayerMask;
    public float jumpStamina;
    public float dashStamina = 10f;


    [Header("Look")]
    public Transform cameraContainer;
    public float minXLook;
    public float maxXLook;
    private float camCurXRot;
    public float lookSensivityX;
    public float lookSensivityY;
    private Vector2 mouseDelta;


    private Rigidbody _rigidbody;
    public Animator animator;

    private void Awake()
    {
        cameraContainer = transform.Find("CameraContainer").GetComponent<Transform>();
        _rigidbody = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
