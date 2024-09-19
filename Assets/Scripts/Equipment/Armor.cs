using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public struct Armor
{
    public Armor(int _defense)
    {
        defense = _defense;
        mana = 0;
        threat = 0;
        icon = null;
    }
    
    public int defense;
    public int mana;
    public int threat;
    public Sprite icon;
}
