using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfDisappearObject : PhysicalSwitch, ITriggerable
{
    [SerializeField] private float disappearTime = 0f;

    private void Awake()
    {
        targetObjects.Add(transform);
    }

    public void Trigger()
    {
        StartCoroutine("DestoryObject");
    }

    private IEnumerator DestoryObject()
    {
        yield return new WaitForSeconds(disappearTime);
        Destroy(gameObject);
    }
}
