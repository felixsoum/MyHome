using UnityEngine;

public class Lever : Interactable
{
    [SerializeField] GameObject handleOrigin;
    [SerializeField] Gate gate;
    bool isOn;

    Quaternion targetRot;
    private readonly float HandleSpeed = 10;

    protected override void Awake()
    {
        base.Awake();
        targetRot = handleOrigin.transform.localRotation;
    }


    void Update()
    {
        handleOrigin.transform.localRotation = Quaternion.Lerp(handleOrigin.transform.localRotation, targetRot, HandleSpeed * Time.deltaTime);
    }

    internal override void Interact(PlayerController playerController)
    {
        isOn = !isOn;
        float angle = 30f;
        if (!isOn)
        {
            angle *= -1;
        }

        gate.Toggle();

        targetRot = Quaternion.Euler(angle, 0, 0);
    }
}
