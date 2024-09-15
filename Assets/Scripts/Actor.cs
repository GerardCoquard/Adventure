using System;
using Unity.VisualScripting;
using UnityEngine;

public class Actor : MonoBehaviour
{
    public IActorInput actorInput;
    public Transform turnIndicator;

    private void Start()
    {
        actorInput = GetComponent<IActorInput>();
    }
    /*
     * Custom stats for test
     */
    
    public int initiative;
    
    /*
     * Base class for every actor in the battle
     *
     * Stats
     * Info
     */
}