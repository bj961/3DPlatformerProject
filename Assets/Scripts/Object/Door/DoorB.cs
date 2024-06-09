using UnityEngine;

public class DoorB : DoorBase
{
    [SerializeField]
    Transform door;

    [SerializeField]
    private bool isActivating;
    [SerializeField]
    private Quaternion closeRotation = Quaternion.identity;
    [SerializeField]
    private Quaternion openRotation = Quaternion.Euler(0f, 90f, 0f);
    private Quaternion targetRotation;

    private void Start()
    {
        isActivating = false;
    }

    private void FixedUpdate()
    {
        if (isActivating)
        {
            float rotateSpeed = 90f * Time.deltaTime; // 회전 속도 (도/초)
            door.localRotation = Quaternion.RotateTowards(door.localRotation, targetRotation, rotateSpeed);

            if (door.localRotation == targetRotation)
            {
                isActivating = false;
                isOpened = !isOpened;
            }
        }

    }

    public override void Open()
    {
        targetRotation = openRotation;
        isActivating = true;
    }

    public override void Close()
    {
        targetRotation = closeRotation;
        isActivating = true;
    }
}