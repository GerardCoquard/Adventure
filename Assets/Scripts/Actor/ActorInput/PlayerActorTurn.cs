using System;
using UnityEngine;

public class PlayerActorTurn : ActorTurn
{
    private bool turnActive;
    public override void StartTurn()
    {
        base.StartTurn();
        AbilityManager.instance.SetActorTurn(this);
        turnActive = true;
    }

    public override void EndTurn()
    {
        turnActive = false;
        base.EndTurn();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.K) && turnActive)
        {
            _actor.OnDie();
            EndTurn();
        }
    }
}