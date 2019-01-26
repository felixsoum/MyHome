using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    new Rigidbody rigidbody;
    Camera camera;
    void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
        camera = Camera.main;
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
        moveForce *= 200;
        rigidbody.AddForce(moveForce * Time.deltaTime, ForceMode.VelocityChange);
    }
}
