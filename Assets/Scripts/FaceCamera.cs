using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FaceCamera : MonoBehaviour
{
    new Camera camera;

    void Awake()
    {
        camera = Camera.main;
    }

    void Update()
    {
        transform.forward = camera.transform.forward;
    }
}
