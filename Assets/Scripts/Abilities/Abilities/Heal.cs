using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[CreateAssetMenu(fileName = "Heal", menuName = "Abilities/Heal", order = 2)]
public class Heal : Ability
{
    public Dice dice;
    public override void OnActivated(List<Actor> targets, Actor self)
    {
        Actor target = targets.First();
        int amount = DiceManager.instance.Roll(new DiceAmount(self.GetMind(), dice));
        target.AddHealth(amount);
    }
}
