using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CombatTables", menuName = "CombatTables", order = 1)]
public class CombatTables : ScriptableObject
{
    public List<Table> hit;
    public List<Table> wound1to2;
    public List<Table> wound3to5;
    public List<Table> wound6to8;
}

[Serializable]
public struct Table
{
    public int value;
    public int diceNeeded;
}
