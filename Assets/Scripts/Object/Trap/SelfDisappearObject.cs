using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfDisappearObject : PhysicalSwitch, ITriggerable
{
    private void Awake()
    {
        targetObjects.Add(transform);
    }

    public void Trigger()
    {
        Destroy(gameObject);
    }
}
