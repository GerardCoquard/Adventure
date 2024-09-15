using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Utilities
{
    public static int[] GetDiceThrow(DiceAmount diceAmount)
    {
        int[] results = new int[diceAmount.amount];
        for (int i = 0; i < results.Length; i++)
        {
            int result = Random.Range(1, (int)diceAmount.dice+1);
            results[i] = result;
        }
        return results;
    }

    public static int Roll(Dice dice)
    {
        return Random.Range(1, (int)dice+1);
    }
}

public enum Dice
{
    D3=3,
    D4=4,
    D6=6,
    D8=8,
    D10=10,
    D12=12,
    D20=20
}
