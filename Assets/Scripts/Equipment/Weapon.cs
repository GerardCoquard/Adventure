using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct Weapon
{
    public Weapon(int _damage)
    {
        damage = _damage;
        threat = 0;
        icon = null;
    }
    public int damage;
    public int threat;
    public Sprite icon;
}
