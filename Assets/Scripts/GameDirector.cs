using System.Collections.Generic;
using UnityEngine;

public class GameDirector : MonoBehaviour
{
    [SerializeField] PlayerController player = null;
    [SerializeField] GameObject arrow = null;
    [SerializeField] List<GameObject> pickupablePrefabs = new List<GameObject>();
    [SerializeField] GhostController ghost = null;
    public List<Interactable> Interactables { get; set; } = new List<Interactable>();

    void Awake()
    {
        InvokeRepeating("SpawnCube", 5, 5);
    }

    void Update()
    {
        float bestDistance = float.MaxValue;
        Interactable closestInteractable = null;

        for (int i = Interactables.Count - 1; i >= 0; i--)
        {
            if (Interactables[i].transform.position.y < -1)
            {
                Destroy(Interactables[i].gameObject);
                Interactables.RemoveAt(i);
            }
        }

        foreach (var interactable in Interactables)
        {
            float currentDistance = Vector3.Distance(player.transform.position, interactable.transform.position);
            if (currentDistance < bestDistance)
            {
                bestDistance = currentDistance;
                closestInteractable = interactable;
            }
        }

        if (bestDistance <= ActorController.InteractionRange)
        {
            arrow.gameObject.SetActive(true);
            arrow.transform.position = closestInteractable.transform.position + Vector3.up * 2;
        }
        else
        {
            arrow.gameObject.SetActive(false);
            closestInteractable = null;
        }

        if (Input.GetButtonDown("Interact"))
        {
            if (player.CurrentPickupable != null)
            {
                player.DropPickupable();
            }
            else if (closestInteractable != null)
            {
                player.Interact(closestInteractable);
            }
        }

        if (ghost.LookingForTarget)
        {
            GhostTarget();
            ghost.LookingForTarget = false;
        }
    }

    void SpawnCube()
    {
        Vector3 randomPos = new Vector3(Random.Range(-10, 10), 10, Random.Range(-10, 10));
        var randomPrefab = pickupablePrefabs[Random.Range(0, pickupablePrefabs.Count)];
        Instantiate(randomPrefab, randomPos, Quaternion.identity);
    }
    
    void GhostTarget()
    {
        var randomInteractable = Interactables[Random.Range(0, Interactables.Count)];
        ghost.SetTargetInteractable(randomInteractable);
    }
}
