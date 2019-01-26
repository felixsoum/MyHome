using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActorController : MonoBehaviour
{
    [SerializeField] protected Transform pickupTransform = null;
    [SerializeField] protected GameObject mesh = null;

    protected const int MoveForce = 300;
    protected const int JumpForce = 50;
    protected const float ThrowForce = 10000;


    public Pickupable CurrentPickupable { get; set; }
    protected new Rigidbody rigidbody;

    internal void DropPickupable()
    {
        CurrentPickupable?.OnDrop((mesh.transform.forward + rigidbody.velocity / 5f) * ThrowForce);
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
}
