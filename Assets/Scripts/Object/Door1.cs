using UnityEngine;

public class Door1 : DoorBase
{
    [SerializeField]
    Transform door;

    private bool isActivating;
    Vector3 targetPosition;
    Vector3 closeLocalPosition = Vector3.zero;
    Vector3 openLocalPosition = new Vector3(0, 5f, 0);

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