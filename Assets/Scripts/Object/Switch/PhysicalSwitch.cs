using System.Collections.Generic;
using UnityEngine;

// 물리적으로 충돌으로 작동되는 스위치
public class PhysicalSwitch : SwitchObjectBase
{
    protected void Start()
    {
        base.Start();
    }

    
    private void OnCollisionEnter(Collision collision)
    {
        if (isSwitchOn == false)
        {
            isSwitchOn = !isSwitchOn;

            if (animator != null)
            {
                animator.SetBool("IsSwitchOn", true);
            }

            foreach (var target in targets)
            {
                target.Trigger();
            } 
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        isSwitchOn = false;

        if (animator != null)
        {
            animator.SetBool("IsSwitchOn", false);
        }
    }
}
