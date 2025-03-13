using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitiativesInstance
{
    public Action OnInitiativesEnded;
    private int _amountOfRolls;
    private int _currentRolls;
    private List<Roll3D> _diceRolls;
    
    public InitiativesInstance(List<ActorTurn> actorTurns)
    {
        _diceRolls = new List<Roll3D>();
        _amountOfRolls = actorTurns.Count;
        
        foreach (ActorTurn battleActor in actorTurns)
        {
            Actor actor = battleActor.GetActor();
            Roll3D roll = DiceManager.instance.RollWithVisuals(actor.GetInitiative(), actor.GetInitiativeBonus(), actor.GetDicePosition(), out int result);
            roll.OnResultShown += CheckIfAllInitiativesAreRolled;
            _diceRolls.Add(roll);
            battleActor.SetInitiative(result);
        }
    }

    private void CheckIfAllInitiativesAreRolled()
    {
        _currentRolls++;
        if (_currentRolls >= _amountOfRolls)
            OnInitiativesEnded?.Invoke();
    }

    public void DeleteInitiatives()
    {
        foreach (var roll in _diceRolls)
        {
            roll.DeleteRoll();
        }
        
        OnInitiativesEnded = null;
    }
}
