using UnityEngine;

public class DropTarget : MonoBehaviour
{
    [SerializeField] Renderer baseRenderer;

    Material baseMaterial;
    bool isActivated;

    void Awake()
    {
        baseMaterial = baseRenderer.material;
    }

    void OnCollisionEnter(Collision collision)
    {
        Pickupable pickupable = collision.gameObject.GetComponent<Pickupable>();
        if (pickupable != null && !isActivated)
        {
            isActivated = true;
            pickupable.transform.rotation = Quaternion.identity;
            pickupable.transform.position = transform.position + Vector3.up * 0.5f;
            baseMaterial.color = Color.green;
        }
    }
}
