using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    [SerializeField] Vector3 playerOffset = Vector3.up;
    [SerializeField] GameObject player = null;

    void Update()
    {
        transform.position = player.transform.position + playerOffset;
    }
}
