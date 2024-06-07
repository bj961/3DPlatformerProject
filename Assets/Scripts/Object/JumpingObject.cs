using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpingObject : MonoBehaviour
{
    public float power;

    void Start()
    {
        power = 150f;
    }


    private void OnCollisionEnter(Collision collision)
    {
        Rigidbody _rigidbody = collision.collider.GetComponent<Rigidbody>();

        if (_rigidbody != null)
        {
            Debug.Log("Jumpimg Object Collision");
            Vector3 force = transform.up * power;
            _rigidbody.AddForce(force, ForceMode.Impulse);
        }
    }
}
