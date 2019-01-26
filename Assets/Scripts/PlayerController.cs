using UnityEngine;

public class PlayerController : ActorController
{
    new Camera camera;
    protected const int MoveForce = 300;

    float currentExtraGravity;
    float incrementExtraGravity = 300;

    void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
        camera = Camera.main;
    }

    protected override void Update()
    {
        base.Update();
        UpdateMovement();

        UpdateJump();

        if (transform.position.y < -1)
        {
            transform.position = new Vector3(0, 2, 0);
        }
    }

    private void UpdateMovement()
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

        if (moveForce.magnitude > 1f)
        {
            FaceDirection(moveForce);
        }
    }

    private void UpdateJump()
    {
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

    bool IsGrounded()
    {
        return Physics.BoxCast(transform.position + Vector3.up * 1, new Vector3(0.4f, 0.05f, 0.4f), Vector3.down, Quaternion.identity, 1);
    }


}
