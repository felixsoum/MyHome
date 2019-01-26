using UnityEngine;

public class Pickupable : Interactable
{
    new Rigidbody rigidbody;

    protected override void Awake()
    {
        base.Awake();
        rigidbody = GetComponent<Rigidbody>();
    }

    internal override void Interact(ActorController actor)
    {
        actor.DropPickupable();
        rigidbody.isKinematic = true;
        actor.CurrentPickupable = this;
    }

    public void OnDrop(Vector3 force)
    {
        rigidbody.isKinematic = false;
        rigidbody.AddForce(force);
    }
}
