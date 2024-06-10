using UnityEngine;

public class DoorA : DoorBase
{
    [SerializeField]
    Transform door;

    [SerializeField]
    private bool isActivating;
    [SerializeField]
    Vector3 openLocalPosition = new Vector3(0, 5f, 0);
    [SerializeField]
    Vector3 closeLocalPosition = Vector3.zero;
    Vector3 targetPosition;

    private void Start()
    {
        isActivating = false;
    }

    private void FixedUpdate()
    {
        if (isActivating)
        {
            float moveSpeed = 2f * Time.deltaTime;
            door.localPosition = Vector3.MoveTowards(door.localPosition, targetPosition, moveSpeed);

            if(door.localPosition == targetPosition)
            {
                isActivating = false;
            }
        }

    }

    public override void Trigger()
    {
        isActivating = true;
        if (isOpened) 
        {
            targetPosition = closeLocalPosition;
        }
        else
        {
            targetPosition = openLocalPosition;
        }
        isOpened = !isOpened;
        PlaySound();
    }

    public override void Open()
    {
        targetPosition = openLocalPosition;
        isActivating = true;
    }

    public override void Close()
    {
        targetPosition = closeLocalPosition;
        isActivating = true;
    }
}