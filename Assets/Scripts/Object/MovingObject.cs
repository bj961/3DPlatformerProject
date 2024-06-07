using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingObject : MonoBehaviour
{
    [SerializeField]
    Transform platform;
    private Rigidbody _rigidbody;

    public Vector3 direction;
    Vector3 targetPosition;
    [SerializeField]
    Vector3[] movePoint;
    public float moveSpeed = 5f;
    [SerializeField]
    bool directionSwap = true;

    private void Start()
    {
        platform = transform.Find("Platform").GetComponent<Transform>();

        _rigidbody = GetComponentInChildren<Rigidbody>();
        movePoint = new Vector3[2];
        movePoint[0] = transform.Find("MovePoint1").GetComponent<Transform>().position;
        movePoint[1] = transform.Find("MovePoint2").GetComponent<Transform>().position;

        targetPosition = movePoint[1];

        direction = (movePoint[1] - movePoint[0]);
    }

    

    void FixedUpdate()
    {
        //_rigidbody.MovePosition(Vector3.MoveTowards(_rigidbody.position, targetPosition, moveSpeed * Time.deltaTime));

        Movement(transform);

        //if (Vector3.Distance(_rigidbody.position, targetPosition) < 0.01f)
        if (Vector3.Distance(platform.position, targetPosition) < 0.01f)
        {
            directionSwap = !directionSwap;
            targetPosition = directionSwap ? movePoint[0] : movePoint[1];
            direction = directionSwap ? movePoint[1] - movePoint[0] : movePoint[0] - movePoint[1];
        }

        

        //Movement(transform);
    }

    void Movement(Transform targetTransform)
    {
        targetTransform.position = targetTransform.position + direction.normalized * moveSpeed * Time.deltaTime;
        //targetTransform.position = targetTransform.position + targetPosition.normalized * moveSpeed * Time.deltaTime;
    }

    private void OnCollisionStay(Collision collision)
    {
        Movement(collision.transform);
    }
}
