using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public interface ITriggerable
{
    public void TriggerObject();
}


public class SwitchObject : MonoBehaviour
{
    // TriggableObject targetObject;
    private bool isSwitchOn;

    // Start is called before the first frame update
    void Start()
    {
        isSwitchOn = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!isSwitchOn) // && 상호작용 가능한 오브젝트(=ITriggable) 이면
        {
            isSwitchOn = !isSwitchOn;
            //targetObject.Trigger();
        }
    }
}
