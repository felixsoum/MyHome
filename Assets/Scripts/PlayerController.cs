using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] Transform pickupTransform = null;
    [SerializeField] GameObject mesh = null;
    public const float InteractionRange = 1.5f;
    private const float ThrowForce = 10000;
    private const int MoveForce = 200;
    private const int JumpForce = 50;

    float currentExtraGravity;
    float incrementExtraGravity = 300;

    public Pickupable CurrentPickupable { get; set; }
    new Rigidbody rigidbody;
    new Camera camera;

    void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
        camera = Camera.main;
    }

    internal void DropPickupable()
    {
        CurrentPickupable?.OnDrop(mesh.transform.forward * ThrowForce);
        CurrentPickupable = null;
    }

    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        Vector3 forward = camera.transform.forward;
        forward.y = 0;
        Vector3 right = camera.transform.right;
        right.y = 0;

        Vector3 moveForce = forward * vertical + right * horizontal;
        if (moveForce.magnitude > 1)
        {
            moveForce.Normalize();
        }
        moveForce *= MoveForce;
        rigidbody.AddForce(moveForce * Time.deltaTime, ForceMode.VelocityChange);

        if (moveForce.magnitude > 0.01f)
        {
            mesh.transform.forward = Vector3.Lerp(mesh.transform.forward, moveForce, 1 * Time.deltaTime);
        }

        if (CurrentPickupable != null)
        {
            CurrentPickupable.transform.position = pickupTransform.position;
            CurrentPickupable.transform.rotation = mesh.transform.rotation;
        }

        if (IsGrounded())
        {
            currentExtraGravity = 0;
            if (Input.GetButtonDown("Jump"))
            {
                rigidbody.AddForce(Vector3.up * JumpForce, ForceMode.VelocityChange);
            }
        }
        else
        {
            currentExtraGravity += incrementExtraGravity * Time.deltaTime;
        }

        rigidbody.AddForce(Vector3.down * currentExtraGravity);
    }

    internal void Interact(Interactable interactable)
    {
        interactable.Interact(this);
    }

    bool IsGrounded()
    {
        return Physics.BoxCast(transform.position + Vector3.up * 1, new Vector3(0.4f, 0.05f, 0.4f), Vector3.down, Quaternion.identity, 1);
    }
}
