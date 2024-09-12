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
    public Dice initiativeDice;
    public Dice manaDice;
    public int attacks;
    public int magicResistance;
    [Header("Equipment")]
    public Weapon weapon;
    public Armor armor;
}