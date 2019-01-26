using UnityEngine;

public class Gate : MonoBehaviour
{
    bool isOpen;
    Vector3 originalPos;
    Vector3 targetPos;

    void Awake()
    {
        originalPos = transform.position;
        targetPos = originalPos;
    }

    void Update()
    {
        transform.position = Vector3.Lerp(transform.position, targetPos, 10 * Time.deltaTime);
    }

    public void Toggle()
    {
        isOpen = !isOpen;

        targetPos = originalPos;
        if (isOpen)
        {
            targetPos += Vector3.up * 5;
        }
    }
}
