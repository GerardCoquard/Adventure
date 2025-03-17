using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class CombatManager : MonoBehaviour
{
    public static CombatManager instance;

    [SerializeField] private CombatTables _tables;

    private void Awake()
    {
        instance = this;
    }

    public void StartCombat(Actor attacker, Actor defender, AbilityBuffs abilityBuffs = new AbilityBuffs())
    {
        int modifier;
        DiceAmount diceAmount = new DiceAmount(attacker.GetAttacks() + abilityBuffs.extraAttacks, Dice.D8);
        modifier = GetHitModifier(defender.GetDefense() - attacker.GetCombat());
        modifier += attacker.GetHitBuff();
        diceAmount.amount = DiceManager.instance.RollAndKeep(diceAmount,modifier);
        modifier = GetWoundModifier(attacker.GetDamage() + abilityBuffs.extraDamage, defender.GetResistance());
        modifier += attacker.GetWoundBuff();
        diceAmount.amount = DiceManager.instance.RollAndKeep(diceAmount,modifier);
        defender.TakeDamage(diceAmount.amount * (attacker.GetDamage() + abilityBuffs.extraDamage));
    }
    
    public void StartMagicCombat()
    {
        
    }

    private int GetHitModifier(int difference)
    {
        if (difference <= 0)
            return _tables.hit[0].diceNeeded;
        
        return _tables.hit.FirstOrDefault(c => c.value >= difference).diceNeeded;
    }
    
    private int GetWoundModifier(int damage, int resistance)
    {
        List<Table> table;
        if (damage <= 2)
            table = _tables.wound1to2;
        else if(damage <= 5)
            table = _tables.wound3to5;
        else
            table = _tables.wound6to8;
        
        return table.Where(c => c.value == resistance).Select(c => c.diceNeeded).FirstOrDefault();
    }
}

public struct AbilityBuffs
{
    public int extraAttacks;
    public int extraDamage;
    public int extraHit;
    public int extraWound;
}
