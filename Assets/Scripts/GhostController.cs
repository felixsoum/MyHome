using UnityEngine;

public class GhostController : ActorController
{
    Interactable targetInteractable;
    const float FlySpeed = 5;

    public bool LookingForTarget { get; set; } = true;
    float currentWaitForThrow;
    float timeWaitForThrow = 0.5f;

    float decisionTimeCurrent;
    float decisionTimeMax = 5;

    void Awake()
    {
        decisionTimeCurrent = decisionTimeMax;
    }

    protected override void Update()
    {
        base.Update();

        decisionTimeCurrent -= Time.deltaTime;
        if (decisionTimeCurrent <= 0)
        {
            decisionTimeCurrent = decisionTimeMax;
            if (targetInteractable != null)
            {
                if (Random.value < 0.5f)
                {
                    targetInteractable = null;
                    LookingForTarget = true;
                }
            }
            else
            {
                LookingForTarget = true;
            }
        }

        if (targetInteractable != null)
        {
            Vector3 pos = Vector3.MoveTowards(transform.position, targetInteractable.transform.position, FlySpeed * Time.deltaTime);
            transform.position = pos;

            FaceDirection(targetInteractable.transform.position - transform.position);

            if (Vector3.Distance(transform.position, targetInteractable.transform.position) < ActorController.InteractionRange)
            {
                Interact(targetInteractable);
                targetInteractable = null;
                decisionTimeCurrent = decisionTimeMax;
            }
        }

        if (currentWaitForThrow > 0)
        {
            currentWaitForThrow -= Time.deltaTime;
            if (currentWaitForThrow <= 0)
            {
                DropPickupable(Random.Range(0, 10000));
            }
        }
    }

    internal override void PickUp(Pickupable pickupable)
    {
        base.PickUp(pickupable);
        currentWaitForThrow = timeWaitForThrow;
    }

    public void SetTargetInteractable(Interactable interactable)
    {
        targetInteractable = interactable;
        LookingForTarget = false;
    }
}
