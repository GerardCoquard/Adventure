using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[Serializable]
public struct EnemyStats
{
    public int level;
    public int combat;
    public int mind;
    public int resistance;
    public Dice healthDice;
    public int healthFixedAmount;
    public Dice manaDice;
    public int manaFixedAmount;
    public Dice initiativeDice;
    public int attacks;
    public int magicResistance;
    [Header("Equipment")]
    public int damage;
    public int armor;
    [Header("Abilities")]
    public List<EnemyAbility> _abilities;
}