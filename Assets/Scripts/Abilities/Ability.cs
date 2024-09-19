using System;using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ability : ScriptableObject
{
    public int targets;
    public Team targetTeam = Team.Other;
    public int actionCost;
    public int bonusActionCost;
    public int manaCost;

    public virtual void OnActivated(List<Actor> targets, Actor self)
    {
        
    }
}

public enum Team
{
    Self,
    Other
}
