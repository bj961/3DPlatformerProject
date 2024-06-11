using System.Collections.Generic;
using UnityEngine;

// 물리적으로 충돌으로 작동되는 스위치
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
