using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActorBuffs : MonoBehaviour
{
    private ActorDisplay _actorDisplay;
    
    private List<BuffData> _hitBuffs = new List<BuffData>();
    private List<BuffData> _woundBuffs = new List<BuffData>();
    private List<BuffData> _threatBuffs = new List<BuffData>();
    private List<BuffData> _magicDefenseBuffs = new List<BuffData>();

    private void Awake()
    {
        _actorDisplay = GetComponentInChildren<ActorDisplay>();
    }

    public void UpdateBuffsDurations()
    {
        foreach (BuffData buff in _hitBuffs)
        {
            buff.duration--;
            if (buff.duration <= 0)
            {
                _hitBuffs.Remove(buff); //Possible BUG?
                _actorDisplay.DeleteBuff(buff);
            }
            else
                _actorDisplay.UpdateBuff(buff);

        }
    }

    public void AddHitBuff(BuffData buff)
    {
        _hitBuffs.Add(buff);
        _actorDisplay.AddHitBuff(buff);
    }
    
    public void AddWoundBuff(BuffData buff)
    {
        _woundBuffs.Add(buff);
        _actorDisplay.AddWoundBuff(buff);
    }
    
    public void AddThreatBuff(BuffData buff)
    {
        _threatBuffs.Add(buff);
        _actorDisplay.AddThreatBuff(buff);
    }
    
    public void AddMagicDefenseBuff(BuffData buff)
    {
        _magicDefenseBuffs.Add(buff);
        _actorDisplay.AddMagicDefenseBuff(buff);
    }

    public int GetHitBuff()
    {
        int amount = 0;

        foreach (BuffData buff in _hitBuffs)
        {
            amount += buff.amount;
        }
        
        return amount;
    }
    
    public int GetWoundBuff()
    {
        int amount = 0;

        foreach (BuffData buff in _woundBuffs)
        {
            amount += buff.amount;
        }
        
        return amount;
    }
    
    public int GetThreatBuff()
    {
        int amount = 0;

        foreach (BuffData buff in _threatBuffs)
        {
            amount += buff.amount;
        }
        
        return amount;
    }
    
    public int GetMagicDefenseBuff()
    {
        int amount = 0;

        foreach (BuffData buff in _magicDefenseBuffs)
        {
            amount += buff.amount;
        }
        
        return amount;
    }
}

public class BuffData
{
    public string description;
    public Sprite icon;
    public int duration;
    public int amount;
}
