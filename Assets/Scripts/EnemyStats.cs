using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[Serializable]
public struct EnemyStats
{
    public int combat;
    public int mind;
    public int resistance;
    public int health;
    public DiceAmount initiative;
    public int mana;
    public int attacks;
    public int magicResistance;
    public int defense;
}