using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Spike : DamageObject, ITriggerable
{
    [SerializeField] public float moveSpeed = 2f;
    [SerializeField] private float activeTime = 10f;
    bool isTriggered = false;

    private void FixedUpdate()
    {
        if (isTriggered == true)
        {
            LaunchSpike();
        }
    }

    void LaunchSpike()
    {
        transform.position += transform.forward * moveSpeed * Time.deltaTime;
    }

    public void Trigger()
    {
        isTriggered = true;
        StartCoroutine(DestoryObject());
    }

    private IEnumerator DestoryObject()
    {
        yield return new WaitForSeconds(activeTime);
        Destroy(this.gameObject);
    }
}
