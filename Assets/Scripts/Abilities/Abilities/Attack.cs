using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "Attack", menuName = "Abilities/Attack", order = 1)]
public class Attack : Ability
{
    public override void OnActivated(List<Actor> targets, Actor self)
    {
        Actor target = targets.First();
        for (int i = 0; i < self.GetAttacks(); i++)
        {
            target.TakeDamage(self.GetDamage());
        }
    }
}
