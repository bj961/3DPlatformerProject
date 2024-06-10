using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalActive : MonoBehaviour, ITriggerable
{
    void Start()
    {
        gameObject.SetActive(false);
    }

    public void Trigger()
    {
        gameObject.SetActive(true);
    }
}
