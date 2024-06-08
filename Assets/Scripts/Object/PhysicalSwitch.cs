using UnityEngine;

public class PhysicalSwitch : SwitchObjectBase
{
    public Transform targetObject;
    
    private ITriggerable target;

    private void Start()
    {
        base.Start();
        target = targetObject.GetComponent<ITriggerable>();
    }

    // 물리적으로 누르면 작동되는 1회성 스위치
    private void OnCollisionEnter(Collision collision)
    {
        if (isSwitchOn == false)
        {
            isSwitchOn = !isSwitchOn;
            target.Trigger();
        }
    }

    //private void OnCollisionExit(Collision collision)
    //{
    //    isSwitchOn = false;
    //}
}
