using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ITriggerable
{
    public void TriggerObject();
}


public abstract class ActivatableObjectBase : MonoBehaviour, ITriggerable
{
    public bool isActive;

    void Start()
    {
        isActive = false;
    }

    public void TriggerObject()
    {
        if(isActive == false) 
        {
            isActive = true;
        }
    }
}
