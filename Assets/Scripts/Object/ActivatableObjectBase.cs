using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IActivatable
{
    public void ActivateObject();
}

public abstract class ActivatableObjectBase : MonoBehaviour, IActivatable
{
    public bool isActive;

    void Start()
    {
        isActive = false;
    }

    public void ActivateObject()
    {
        if(isActive == false) 
        {
            isActive = true;
        }
    }
}
