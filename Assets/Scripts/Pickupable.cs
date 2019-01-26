using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickupable : Interactable
{
    new Rigidbody rigidbody;

    protected override void Awake()
    {
        base.Awake();
        rigidbody = GetComponent<Rigidbody>();
    }

    internal override void Interact(PlayerController playerController)
    {
        playerController.DropPickupable();
        rigidbody.isKinematic = true;
        playerController.CurrentPickupable = this;
    }

    public void OnDrop(Vector3 force)
    {
        rigidbody.isKinematic = false;
        rigidbody.AddForce(force);
    }
}
