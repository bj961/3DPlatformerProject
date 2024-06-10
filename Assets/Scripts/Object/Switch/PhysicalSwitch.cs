using System.Collections.Generic;
using UnityEngine;

public class PhysicalSwitch : SwitchObjectBase
{
    public List<Transform> targetObjects;
    private List<ITriggerable> targets = new List<ITriggerable>();

    protected void Start()
    {
        base.Start();
        foreach(var targetObject in targetObjects)
        {
            targets.Add(targetObject.GetComponent<ITriggerable>());
        }  
    }

    // 물리적으로 누르면 작동되는 스위치
    private void OnCollisionEnter(Collision collision)
    {
        if (isSwitchOn == false)
        {
            isSwitchOn = !isSwitchOn;
            foreach(var target in targets)
            {
                target.Trigger();
            }
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        isSwitchOn = false;
    }
}
