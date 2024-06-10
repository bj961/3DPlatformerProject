using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableSwitch : SwitchObjectBase, IInteractable
{
    public List<Transform> targetObjects;
    private List<ITriggerable> targets = new List<ITriggerable>();

    Animator animator;

    protected void Start()
    {
        base.Start();
        foreach (var targetObject in targetObjects)
        {
            targets.Add(targetObject.GetComponent<ITriggerable>());
        }

        gameObject.layer = LayerMask.NameToLayer("Interactable");

        TryGetComponent<Animator>(out animator);
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
