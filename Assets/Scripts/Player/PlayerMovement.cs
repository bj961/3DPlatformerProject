using System.Collections;
using System.Collections.Generic;
using System.Data;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public enum MovementState
{
    walking,
    sprinting,
    crouching,
    air
}

public class PlayerMovement : MonoBehaviour
{
    Rigidbody _rigidbody;
    Animator animator;
    /*
    [Header("Look")]
    public Transform cameraContainer;
    public float minXLook;
    public float maxXLook;
    private float camCurXRot;
    public float lookSensivity;
    private Vector2 mouseDelta;
    */
    [Header("Movement")]
    public float moveSpeed;
    public float walkSpeed;
    public float sprintSpeed;
    public MovementState state;
    public float groundDrag;
    private Vector2 curMovementInput;
    private Vector3 beforeDirection;


    [Header("Jump")]
    public float jumpForce;
    public float jumpCooltime;
    public float airMultiplier = 0.4f;
    bool readyToJump;


    [Header("Crouching")]
    public float crouchSpeed;
    // TODO : ���� �ִϸ��̼� ���� collider�� ���� �������°�? Ȯ���غ��� scale ���� ���� �Ǵ� ����
    public float crouchYScale;
    private float startYScale;

    [Header("Ground Check")]
    public float playerHeight; // TODO : ���� �ڵ�� �ֱ�
    public LayerMask groundLayerMask;
    [SerializeField] bool isGrounded;
        

    [Header("Dash")]
    public float dashCoolTime;


    public Transform orientation;
    
    float horizontalInput;
    float verticalInput;
    Vector3 moveDirection;


    void Start()
    {
        orientation = transform.Find("Orientation").GetComponent<Transform>();
        //cameraContainer = transform.Find("CameraContainer").GetComponent<Transform>();
        _rigidbody = GetComponent<Rigidbody>();
        _rigidbody.freezeRotation = true;
        //TryGetComponent(out animator);

        readyToJump = true;

        startYScale = transform.localScale.y;
    }

    private void Update()
    {
        GroundCheck();

        SpeedControl();
        StateHandler();

        if (isGrounded)
            _rigidbody.drag = groundDrag;
        else
            _rigidbody.drag = 0;

    }

    private void FixedUpdate()
    {
        Move();
    }

    private void LateUpdate()
    {
        //CameraLook();
    }

    //void CameraLook()
    //{
    //    //���콺 �¿�� �����̸� ���콺��Ÿ�� x�� �����?? ĳ���Ͱ� �¿�� �����̷��� ���� y���� ������ ��
    //    // �׷��� ������ �޴°�, ���콺��Ÿx�� y�� �־��ְ� y�� x�� �־���� ���ϴ� ����� �� �� ����
    //    // -> ����嵨Ÿx���� ���콺 �ΰ��� ���ؼ� y�࿡ �־���
    //    camCurXRot += mouseDelta.y * lookSensivity;
    //    camCurXRot = Mathf.Clamp(camCurXRot, minXLook, maxXLook); // Clamp() : �ּҰ����� �۾����� �ִ밪 ��ȯ, �ִ밪���� Ŀ���� �ּҰ� ��ȯ
    //    cameraContainer.localEulerAngles = new Vector3(-camCurXRot, 0, 0); // - �ٿ��� ���� : ���콺 �̵��� ȸ���� �ݴ�� �׷� (���� �����̼� �� �ٲ����)

    //    transform.eulerAngles += new Vector3(0, mouseDelta.x * lookSensivity, 0);
    //}

    //public void OnLook(InputAction.CallbackContext context)
    //{
    //    mouseDelta = context.ReadValue<Vector2>();
    //}


    public void OnMove(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed) //Started�� Ű ���� �������� �۵�
        {
            curMovementInput = context.ReadValue<Vector2>();
            //animator.SetBool("Moving", true);
            //animator.SetFloat("zAxis", curMovementInput.y);
            //animator.SetFloat("xAxis", curMovementInput.x);
        }
        else if (context.phase == InputActionPhase.Canceled)
        {
            curMovementInput = Vector2.zero;
            //animator.SetBool("Moving", false);
            //animator.SetFloat("zAxis", curMovementInput.y);
            ////animator.SetFloat("xAxis", curMovementInput.x);
        }
    }

    void Move()
    {
        moveDirection = orientation.forward * curMovementInput.y + orientation.right * curMovementInput.x;

        if (isGrounded)
        {
            _rigidbody.AddForce(moveDirection.normalized * moveSpeed * 10f, ForceMode.Force);
        }
        else
        {
            _rigidbody.AddForce(moveDirection.normalized * moveSpeed * 10f * airMultiplier, ForceMode.Force);
        }

        /*
        if (moveDirection != Vector3.zero)
        {
            _rigidbody.velocity = moveDirection;
            beforeDirection = moveDirection;
        }
        else
        {
            if (moveDirection != beforeDirection)
            {
                _rigidbody.velocity = moveDirection;
                beforeDirection = moveDirection;
            }
        }
        */
    }

    private void SpeedControl()
    {
        Vector3 flatVel = new Vector3(_rigidbody.velocity.x, 0f, _rigidbody.velocity.z);

        if(flatVel.magnitude > moveSpeed)
        {
            Vector3 limitedVel = flatVel.normalized * moveSpeed;
            _rigidbody.velocity = new Vector3(limitedVel.x, _rigidbody.velocity.y, limitedVel.z);
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

    // TODO : ���� �Ǹ� �޸��� �߿��� ���¹̳� �Ҹ�ǵ���
    // �ƴϸ� �ƿ� �޸��� ����� �����ϰ� �뽬�� �������
    public void OnSprint(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            Debug.Log("Sprinting");
            //animator.SetBool("isRunning", true);
            state = MovementState.sprinting;
            moveSpeed = sprintSpeed;
        }

        if (context.phase == InputActionPhase.Canceled)
        {
            //animator.SetBool("isRunning", false);
            state = MovementState.walking;
            moveSpeed = walkSpeed;
        }
    }


    public void OnCrouch(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed && isGrounded)
        {
            state = MovementState.crouching;
            transform.localScale = new Vector3(transform.localScale.x, crouchYScale, transform.localScale.z);
            _rigidbody.AddForce(Vector3.down * 5f, ForceMode.Impulse);
            moveSpeed = crouchSpeed;
        }

        if (context.phase == InputActionPhase.Canceled && isGrounded)
        {
            state = MovementState.walking;
            transform.localScale = new Vector3(transform.localScale.x, startYScale, transform.localScale.z);
            moveSpeed = walkSpeed;
        }
    }

    private void StateHandler()
    {
        if (state == MovementState.crouching)
        {
            moveSpeed = crouchSpeed;
        }
        else if (isGrounded && state == MovementState.sprinting)
        {
            moveSpeed = sprintSpeed;
        }
        else if (isGrounded)
        {
            state = MovementState.walking;
            moveSpeed = walkSpeed;
        }
        else
        {
            state = MovementState.air;
        }
    }
}
