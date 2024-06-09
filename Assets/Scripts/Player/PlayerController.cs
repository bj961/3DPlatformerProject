using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEditor.Timeline.TimelinePlaybackControls;



public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    //public MovementState state;
    public float moveSpeed;
    public float walkSpeed;
    public float sprintSpeed;
    private Vector2 curMovementInput;
    [SerializeField] private Vector3 beforeDirection;
    //public float jumpStamina;
    //public float dashStamina = 10f;

    [Header("Jump")]
    public float jumpForce;
    public float jumpCooltime;
    public float airMultiplier = 0.4f;
    bool readyToJump;

    [Header("Ground Check")]
    public float playerHeight; // TODO : ���� �ڵ�� �ֱ�
    public LayerMask groundLayerMask;
    [SerializeField] bool isGrounded;


    [Header("Crouching")]
    public float crouchSpeed;
    // TODO : ���� �ִϸ��̼� ���� collider�� ���� �������°�? Ȯ���غ��� scale ���� ���� �Ǵ� ����
    public float crouchYScale;
    private float startYScale;

    [Header("Look")]
    public Transform cameraContainer;
    public float minXLook;
    public float maxXLook;
    private float camCurXRot;
    public float lookSensivity;
    private Vector2 mouseDelta;
    public bool canLook = true; //�κ��丮 ���� �� ȭ�� �������� �ʰ� Ŀ�� ������ �ϱ� ����

    public Action inventory;
    private Rigidbody _rigidbody;
    public PlayerInput playerInput;
    public Animator animator;


    bool isJumping = false;


    private void Awake()
    {
        cameraContainer = transform.Find("CameraContainer").GetComponent<Transform>();
        _rigidbody = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        playerInput = GetComponent<PlayerInput>();
    }

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        moveSpeed = walkSpeed;

        readyToJump = true;

        startYScale = transform.localScale.y;
    }

    private void Update()
    {
        GroundCheck();
        //StateHandler();
    }

    void FixedUpdate()
    {
        Move();
    }

    private void LateUpdate()
    {
        if (canLook)
        {
            CameraLook();
        }
    }



    private void Move()
    {
        Vector3 direction;

        // �� �κ��� �����ؾ� ��.
        // ���� �� �������� �ð��� ����Ͽ� ���� �߿��� move�� ���� ���ϰ� �ϴ°� ���� ������ ���.
        direction = transform.forward * curMovementInput.y + transform.right * curMovementInput.x; // �Է��� ���� �� �� �κ��� 0
        direction *= moveSpeed;
        direction.y = _rigidbody.velocity.y;

        //_rigidbody.velocity = direction;

        if (direction != Vector3.zero)
        {
            _rigidbody.velocity = direction;
            beforeDirection = direction;
        }
        else
        {
            if (direction != beforeDirection)
            {
                _rigidbody.velocity = direction;
                beforeDirection = direction;
            }
        }
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed) //Started�� Ű ���� �������� �۵�
        {
            curMovementInput = context.ReadValue<Vector2>();
            animator.SetBool("Moving", true);
            //animator.SetFloat("xAxis", curMovementInput.x);
            animator.SetFloat("zAxis", curMovementInput.y);
        }
        else if (context.phase == InputActionPhase.Canceled)
        {
            curMovementInput = Vector2.zero;
            animator.SetBool("Moving", false);
            //animator.SetFloat("xAxis", curMovementInput.x);
            animator.SetFloat("zAxis", curMovementInput.y);
        }
    }

    void CameraLook()
    {
        //���콺 �¿�� �����̸� ���콺��Ÿ�� x�� �����?? ĳ���Ͱ� �¿�� �����̷��� ���� y���� ������ ��
        // �׷��� ������ �޴°�, ���콺��Ÿx�� y�� �־��ְ� y�� x�� �־���� ���ϴ� ����� �� �� ����
        // -> ����嵨Ÿx���� ���콺 �ΰ��� ���ؼ� y�࿡ �־���
        camCurXRot += mouseDelta.y * lookSensivity;
        camCurXRot = Mathf.Clamp(camCurXRot, minXLook, maxXLook); // Clamp() : �ּҰ����� �۾����� �ִ밪 ��ȯ, �ִ밪���� Ŀ���� �ּҰ� ��ȯ

        //Debug.Log("mouseDelta : " + mouseDelta + "\ncamCurXRot : " + camCurXRot);

        cameraContainer.localEulerAngles = new Vector3(-camCurXRot, 0, 0); // - �ٿ��� ���� : ���콺 �̵��� ȸ���� �ݴ�� �׷� (���� �����̼� �� �ٲ����)

        transform.eulerAngles += new Vector3(0, mouseDelta.x * lookSensivity, 0);
    }

    public void OnLook(InputAction.CallbackContext context)
    {
        mouseDelta = context.ReadValue<Vector2>();
    }



    private void Jump()
    {
        _rigidbody.velocity = new Vector3(_rigidbody.velocity.x, 0f, _rigidbody.velocity.z);
        _rigidbody.AddForce(transform.up * jumpForce, ForceMode.Impulse);
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started && readyToJump && isGrounded)
        {
            readyToJump = false;
            Jump();
            // TODO : �ڷ�ƾ���� ����
            Invoke(nameof(ResetJump), jumpCooltime);
        }
    }

    private void ResetJump()
    {
        readyToJump = true;
    }



    public void OnSprint(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed && isGrounded)
        {
            //Debug.Log("Sprinting");
            //state = MovementState.sprinting;
            animator.SetBool("isRunning", true);
            moveSpeed = sprintSpeed;
        }

        if (context.phase == InputActionPhase.Canceled)
        {
            //state = MovementState.walking;
            animator.SetBool("isRunning", false);
            moveSpeed = walkSpeed;
        }
    }



    void GroundCheck()
    {
        // TODO :
        // BoxCast�� �ٲٴ� �� ����غ���
        // BoxCast ���� : https://velog.io/@nagi0101/Unity-%EC%99%84%EB%B2%BD%ED%95%9C-CharacterController-%EA%B8%B0%EB%B0%98-Player%EB%A5%BC-%EC%9C%84%ED%95%B4

        Ray[] rays = new Ray[4]
        {
            new Ray(transform.position + (transform.forward * 0.2f) + (transform.up * 0.01f), Vector3.down),
            new Ray(transform.position + (-transform.forward * 0.2f) + (transform.up * 0.01f), Vector3.down),
            new Ray(transform.position + (transform.right * 0.2f) + (transform.up * 0.01f), Vector3.down),
            new Ray(transform.position + (-transform.right * 0.2f) + (transform.up * 0.01f), Vector3.down)
        };

        float rayMaxDistance = playerHeight * 0.5f + 0.2f; // 0.5f�� ĳ���� Ű ����(Raycast ���� ������ ĳ���� �߾�), 0.2f�� �߰� ��

        for (int i = 0; i < rays.Length; i++)
        {
            if (Physics.Raycast(rays[i], rayMaxDistance, groundLayerMask))
            {
                isGrounded = true;
                return;
            }
        }

        isGrounded = false;
    }

    public void DisablePlayerInput()
    {
        Cursor.visible = true;
        ToggleCursor();
        playerInput.enabled = false;
    }

    //private void StateHandler()
    //{
    //    //if (state == MovementState.crouching)
    //    //{
    //    //    moveSpeed = crouchSpeed;
    //    //}
    //    //else 
    //    if (isGrounded && state == MovementState.sprinting)
    //    {
    //        moveSpeed = sprintSpeed;
    //    }
    //    else if (isGrounded)
    //    {
    //        state = MovementState.walking;
    //        moveSpeed = walkSpeed;
    //    }
    //    else
    //    {
    //        state = MovementState.air;
    //    }
    //}


    //public void OnCrouch(InputAction.CallbackContext context)
    //{
    //    if (context.phase == InputActionPhase.Performed && isGrounded)
    //    {
    //        state = MovementState.crouching;
    //        transform.localScale = new Vector3(transform.localScale.x, crouchYScale, transform.localScale.z);
    //        _rigidbody.AddForce(Vector3.down * 5f, ForceMode.Impulse);
    //        moveSpeed = crouchSpeed;
    //    }

    //    if (context.phase == InputActionPhase.Canceled && isGrounded)
    //    {
    //        state = MovementState.walking;
    //        transform.localScale = new Vector3(transform.localScale.x, startYScale, transform.localScale.z);
    //        moveSpeed = walkSpeed;
    //    }
    //}

    //public void OnInventory(InputAction.CallbackContext context)
    //{
    //    if (context.phase == InputActionPhase.Started)
    //    {
    //        inventory?.Invoke();
    //        ToggleCursor();
    //    }
    //}

    void ToggleCursor()
    {
        bool toggle = Cursor.lockState == CursorLockMode.Locked;
        Cursor.lockState = toggle ? CursorLockMode.None : CursorLockMode.Locked;
        canLook = !toggle;
    }
}
