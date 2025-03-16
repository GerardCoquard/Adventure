using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class DiceManager : MonoBehaviour
{
    public static DiceManager instance;
    
    [SerializeField] private GameObject _diceRollPrefab;
    [SerializeField] private Transform _initiativesHolder;

    private void Awake()
    {
        instance = this;
    }

    public Roll3D RollWithVisuals(DiceAmount diceAmount, int bonus, Vector2 position, out int result)
    {
        result = 0;
        int[] results = new int[diceAmount.amount];
        
        for (int i = 0; i < diceAmount.amount; i++)
        {
            int diceResult = Random.Range(1, (int)diceAmount.dice+1);
            results[i] = diceResult;
            result += diceResult;
        }

        result += bonus;
        Roll3D roll = Instantiate(_diceRollPrefab, Camera.main.WorldToScreenPoint(position), Quaternion.identity, _initiativesHolder).GetComponent<Roll3D>();
        roll.RollDices(diceAmount, results, bonus, result);
        return roll;
    }
    
    public int Roll(DiceAmount diceAmount)
    {
        int total = 0;
        
        for (int i = 0; i < diceAmount.amount; i++)
        {
            int result = Roll(diceAmount.dice);;
            total += result;
        }
        return total;
    }

    public int Roll(Dice dice)
    {
        return Random.Range(1, (int)dice+1);
    }

    public int RollAndKeep(DiceAmount diceAmount, int threshold)
    {
        int keeped = 0;
        
        for (int i = 0; i < diceAmount.amount; i++)
        {
            if(Roll(diceAmount.dice) >= threshold)
                keeped++;
        }

        return keeped;
    }
}

public enum Dice
{
    D4=4,
    D6=6,
    D8=8,
    D10=10,
    D12=12,
    D20=20
}

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
