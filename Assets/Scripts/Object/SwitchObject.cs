using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;



public class SwitchObject : MonoBehaviour
{
    public ITriggerable targetObject;
    private bool isSwitchOn;


    // Start is called before the first frame update
    void Start()
    {
        isSwitchOn = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (isSwitchOn == false) // && 상호작용 가능한 오브젝트(=ITriggable) 이면
        {
            isSwitchOn = !isSwitchOn;
            targetObject.TriggerObject();
        }
    }
}
