using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableSwitch : SwitchObjectBase, IInteractable
{
    protected void Start()
    {
        base.Start();

        gameObject.layer = LayerMask.NameToLayer("Interactable"); 
    }

    public string GetInteractPrompt()
    {
        string str = $"[F] Switch {(isSwitchOn ? "On" : "Off" )}";
        return str;
    }

    public void OnInteract()
    {
        isSwitchOn = !isSwitchOn;

        if (animator != null)
        {
            bool isAnimatorSwitchOn = animator.GetBool("IsSwitchOn");
            animator.SetBool("IsSwitchOn", !isAnimatorSwitchOn);
        }

        foreach (var target in targets)
        {
            target.Trigger();
        }   
    }
}
