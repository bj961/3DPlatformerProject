using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpingObject : MonoBehaviour
{
    public float power;

    private void OnCollisionEnter(Collision collision)
    {
        Rigidbody _rigidbody = collision.collider.GetComponentInChildren<Rigidbody>();

        if (_rigidbody != null)
        {
            Vector3 force = transform.up * power;
            _rigidbody.AddForce(force, ForceMode.Impulse);
        }
    }
}
