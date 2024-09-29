using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class DiceManager : MonoBehaviour
{
    public static DiceManager instance;
    
    [SerializeField] private List<Sprite> _diceSprites;
    [SerializeField] private GameObject _diceRollPrefab;
    [SerializeField] private Transform _initiativesHolder;
    private Dictionary<Dice, Sprite> _diceSpritesDictionary;

    private void Awake()
    {
        instance = this;
        
        _diceSpritesDictionary = new Dictionary<Dice, Sprite>();
        int indx = 0;
        foreach (int i in Enum.GetValues(typeof(Dice)))
        {
            _diceSpritesDictionary.Add((Dice)i,_diceSprites[indx]);
            indx++;
        }
            
    }

    public int RollWithVisuals(DiceAmount diceAmount, Vector2 position, bool showTotal)
    {
        int total = 0;
        int[] results = new int[diceAmount.amount];
        
        for (int i = 0; i < diceAmount.amount; i++)
        {
            int result = Random.Range(1, (int)diceAmount.dice+1);
            results[i] = result;
            total += result;
        }
        Roll roll = Instantiate(_diceRollPrefab, Camera.main.WorldToScreenPoint(position), Quaternion.identity, _initiativesHolder).GetComponent<Roll>();
        roll.RollDices(diceAmount, results, 3, total, _diceSpritesDictionary[diceAmount.dice], showTotal);
        return total;
    }
    
    public int Roll(DiceAmount diceAmount)
    {
        int total = 0;
        
        for (int i = 0; i < diceAmount.amount; i++)
        {
            int result = Random.Range(1, (int)diceAmount.dice+1);
            total += result;
        }
        return total;
    }

    public int Roll(Dice dice)
    {
        return Random.Range(1, (int)dice+1);
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
