using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    GameDirector gameDirector;


    void Awake()
    {
        gameDirector = GameObject.FindGameObjectWithTag("GameDirector").GetComponent<GameDirector>();
        gameDirector.Interactables.Add(this);
    }


}
