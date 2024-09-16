using System;
using UnityEngine;

public class PlayerActorInput : ActorInput
{
    private bool turnActive;
    public override void StartTurn()
    {
        turnActive = true;
    }
    
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space) && turnActive)
            EndTurn();
    }
}