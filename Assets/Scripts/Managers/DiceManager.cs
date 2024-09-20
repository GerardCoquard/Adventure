using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class DiceManager : MonoBehaviour
{
    public static DiceManager instance;
    
    [SerializeField] private List<Sprite> _diceSprites;
    [SerializeField] private GameObject _dicePrefab;
    [SerializeField] private Transform _initiativesHolder;
    private Dictionary<Dice, Sprite> _diceSpritesDictionary;

    private void Awake()
    {
        instance = this;
        
        _diceSpritesDictionary = new Dictionary<Dice, Sprite>();
        
        foreach(int i in Enum.GetValues(typeof(Dice)))
            _diceSpritesDictionary.Add((Dice)i,_diceSprites[i]);
    }

    public int RollWithVisuals(DiceAmount diceAmount, Vector2 position)
    {
        int total = 0;
        
        for (int i = 0; i < diceAmount.amount; i++)
        {
            int result = Random.Range(1, (int)diceAmount.dice+1);
            DiceRoll dice = Instantiate(_dicePrefab, Camera.main.WorldToScreenPoint(position), Quaternion.identity, _initiativesHolder).GetComponent<DiceRoll>();
            dice.SetRoll(result,(int)diceAmount.dice,_diceSpritesDictionary[diceAmount.dice]);
            total += result;
        }
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
