using System;
using UnityEngine;

public class ActorController : MonoBehaviour
{
    [SerializeField] protected Transform pickupTransform = null;
    [SerializeField] protected GameObject mesh = null;

    public const float InteractionRange = 1.5f;
    protected const int JumpForce = 50;
    protected const float ThrowForce = 10000;


    public Pickupable CurrentPickupable { get; set; }

    internal virtual void PickUp(Pickupable pickupable)
    {
        CurrentPickupable = pickupable;
        CurrentPickupable.transform.position = pickupTransform.position;
        CurrentPickupable.transform.rotation = mesh.transform.rotation;
    }

    protected new Rigidbody rigidbody;

    internal void DropPickupable(float extraExtraForce = 0)
    {
        Vector3 extraForce = Vector3.zero;
        if (rigidbody != null)
        {
            extraForce += rigidbody.velocity / 5f;
        }
        CurrentPickupable?.OnDrop((mesh.transform.forward + extraForce) * (ThrowForce + extraExtraForce));
        CurrentPickupable = null;
    }

    protected virtual void Update()
    {
        if (CurrentPickupable != null)
        {
            CurrentPickupable.transform.position = pickupTransform.position;
            CurrentPickupable.transform.rotation = mesh.transform.rotation;
        }
    }

    internal void Interact(Interactable interactable)
    {
        interactable.Interact(this);
    }

    protected void FaceDirection(Vector3 moveDirection)
    {
        mesh.transform.forward = Vector3.Lerp(mesh.transform.forward, moveDirection.normalized, 20 * Time.deltaTime);
    }
}
