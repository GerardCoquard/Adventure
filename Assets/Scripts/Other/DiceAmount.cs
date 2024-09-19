using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[Serializable]
public struct DiceAmount
{
    public DiceAmount(int _amount, Dice _dice)
    {
        dice = _dice;
        amount = _amount;
    }
    public int amount;
    public Dice dice;
}
