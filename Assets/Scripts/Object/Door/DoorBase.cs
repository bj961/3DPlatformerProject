using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class DoorBase : MonoBehaviour, ITriggerable
{
    public bool isOpened;

    void Start()
    {
        isOpened = false;
    }

    public void Trigger()
    {
        if(isOpened == false) 
        {
            Open();
        }
        else
        {
            Close();
        }
    }

    public abstract void Open();
    public abstract void Close();
}
