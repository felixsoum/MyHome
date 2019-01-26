using UnityEngine;

public class Interactable : MonoBehaviour
{
    GameDirector gameDirector;


    protected virtual void Awake()
    {
        gameDirector = GameObject.FindGameObjectWithTag("GameDirector").GetComponent<GameDirector>();
        gameDirector.Interactables.Add(this);
    }

    internal virtual void Interact(PlayerController playerController)
    {

    }
}
