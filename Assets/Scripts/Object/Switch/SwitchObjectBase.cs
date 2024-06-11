using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;



public abstract class SwitchObjectBase : MonoBehaviour
{
    [SerializeField] protected bool isSwitchOn;

    public List<Transform> targetObjects;
    protected List<ITriggerable> targets = new List<ITriggerable>();

    protected Animator animator;

    protected void Start()
    {
        isSwitchOn = false;
        foreach (var targetObject in targetObjects)
        {
            targets.Add(targetObject.GetComponent<ITriggerable>());
        }
        TryGetComponent<Animator>(out animator);
    }
}
