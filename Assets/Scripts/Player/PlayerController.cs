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
    public float playerHeight; // TODO : 추후 코드로 넣기
    public LayerMask groundLayerMask;
    [SerializeField] bool isGrounded;


    [Header("Crouching")]
    public float crouchSpeed;
    // TODO : 에셋 애니메이션 쓰면 collider도 같이 낮아지는가? 확인해보고 scale 변수 조정 또는 삭제
    public float crouchYScale;
    private float startYScale;

    [Header("Look")]
    public Transform cameraContainer;
    public float minXLook;
    public float maxXLook;
    private float camCurXRot;
    public float lookSensivity;
    private Vector2 mouseDelta;
    public bool canLook = true; //인벤토리 켰을 때 화면 움직이지 않고 커서 나오게 하기 위함

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

        // 이 부분을 수정해야 함.
        // 점프 시 떨어지는 시간을 계산하여 점프 중에는 move를 동작 안하게 하는게 가장 간단한 방법.
        direction = transform.forward * curMovementInput.y + transform.right * curMovementInput.x; // 입력이 없을 때 이 부분이 0
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
        if (context.phase == InputActionPhase.Performed) //Started는 키 눌린 순간에만 작동
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
        //마우스 좌우로 움직이면 마우스델타의 x가 변경됨?? 캐릭터가 좌우로 움직이려면 축을 y축을 돌려야 함
        // 그래서 실제로 받는값, 마우스델타x는 y에 넣어주고 y는 x에 넣어줘야 원하는 결과를 낼 수 있음
        // -> 마우드델타x값에 마우스 민감도 곱해서 y축에 넣어줌
        camCurXRot += mouseDelta.y * lookSensivity;
        camCurXRot = Mathf.Clamp(camCurXRot, minXLook, maxXLook); // Clamp() : 최소값보다 작아지면 최대값 반환, 최대값보다 커지면 최소값 반환

        //Debug.Log("mouseDelta : " + mouseDelta + "\ncamCurXRot : " + camCurXRot);

        cameraContainer.localEulerAngles = new Vector3(-camCurXRot, 0, 0); // - 붙여준 이유 : 마우스 이동과 회전이 반대라서 그럼 (실제 로테이션 값 바꿔봐라)

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
            // TODO : 코루틴으로 변경
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
        // BoxCast로 바꾸는 것 고려해보기
        // BoxCast 참고 : https://velog.io/@nagi0101/Unity-%EC%99%84%EB%B2%BD%ED%95%9C-CharacterController-%EA%B8%B0%EB%B0%98-Player%EB%A5%BC-%EC%9C%84%ED%95%B4

        Ray[] rays = new Ray[4]
        {
            new Ray(transform.position + (transform.forward * 0.2f) + (transform.up * 0.01f), Vector3.down),
            new Ray(transform.position + (-transform.forward * 0.2f) + (transform.up * 0.01f), Vector3.down),
            new Ray(transform.position + (transform.right * 0.2f) + (transform.up * 0.01f), Vector3.down),
            new Ray(transform.position + (-transform.right * 0.2f) + (transform.up * 0.01f), Vector3.down)
        };

        float rayMaxDistance = playerHeight * 0.5f + 0.2f; // 0.5f는 캐릭터 키 절반(Raycast 시작 지점이 캐릭터 중앙), 0.2f는 추가 값

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
