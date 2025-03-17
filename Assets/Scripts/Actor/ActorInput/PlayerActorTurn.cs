using System;
using UnityEngine;

public class PlayerActorTurn : ActorTurn
{
    public override void StartTurn()
    {
        base.StartTurn();
        AbilityManager.instance.SetActorTurn(this);
    }
}