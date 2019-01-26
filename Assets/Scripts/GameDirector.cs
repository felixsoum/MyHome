using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameDirector : MonoBehaviour
{
    [SerializeField] PlayerController player = null;
    [SerializeField] GameObject arrow = null;

    public List<Interactable> Interactables { get; set; } = new List<Interactable>();

    void Update()
    {
        float bestDistance = float.MaxValue;
        Interactable closestInteractable = null;

        foreach (var interactable in Interactables)
        {
            float currentDistance = Vector3.Distance(player.transform.position, interactable.transform.position);
            if (currentDistance < bestDistance)
            {
                bestDistance = currentDistance;
                closestInteractable = interactable;
            }
        }

        if (bestDistance <= PlayerController.InteractionRange)
        {
            arrow.gameObject.SetActive(true);
            arrow.transform.position = closestInteractable.transform.position + Vector3.up;
        }
        else
        {
            arrow.gameObject.SetActive(false);
            closestInteractable = null;
        }

        if (Input.GetButtonDown("Interact"))
        {
            if (closestInteractable != null)
            {
                player.Interact(closestInteractable);
            }
        }
    }
}
