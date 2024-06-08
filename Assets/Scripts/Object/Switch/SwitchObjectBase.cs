using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;



public abstract class SwitchObjectBase : MonoBehaviour
{
    //public Transform targetObject;
    //protected ITriggerable target;
    [SerializeField] protected bool isSwitchOn;

    protected void Start()
    {
        isSwitchOn = false;
    }
}
