using System;
using UnityEngine;

public class PlayerActorInput : ActorInput
{
    private bool turnActive;
    public override void StartTurn()
    {
        turnActive = true;
    }

    public override void EndTurn()
    {
        turnActive = false;
        base.EndTurn();
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space) && turnActive)
            EndTurn();

        if (Input.GetKeyDown(KeyCode.K) && turnActive)
        {
            _actor.OnDie();
            EndTurn();
        }
        
        if (Input.GetKeyDown(KeyCode.Alpha1) && turnActive)
        {
            SelectAbility(_actor.GetAbilities()[0]);
        }
            
    }

    public void SelectAbility(Ability ability)
    {
        TargetManager.instance.StartSelectingTargets(ability);
    }
}