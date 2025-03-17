using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public abstract class Actor : MonoBehaviour
{
    protected Equipment _equipment;
    protected ActorBuffs _actorBuffs;
    protected ActorDisplay _display;
    
    
    protected string _actorName;
    
    protected int _health;
    protected int _mana;
    protected int _lastHealthAdded;
    protected int _lastManaAdded;
    
    protected List<Ability> _abilities;

    protected int _currentHealth;
    protected int _currentMana;
    
    protected int _currentActionAmount;
    protected int _currentBonusActionAmount;

    private void Awake()
    {
        _equipment = GetComponent<Equipment>();
        _actorBuffs = GetComponent<ActorBuffs>();
        _display = GetComponentInChildren<ActorDisplay>();
    }
    
    //Getters
    
    public abstract int GetCombat();

    public abstract int GetMind();
    
    public abstract int GetResistance();
    
    public abstract Dice GetManaDice();
    
    public abstract Dice GetHealthDice();
    
    public abstract Dice GetInitiativeDice();
    
    public abstract int GetInitiativeBonus();
    
    public abstract int GetAttacks();

    public abstract int GetMagicResistance();
    
    public abstract int GetActionAmount();
    
    public abstract int GetBonusActionAmount();
    
    public abstract void OnDie();

    protected virtual void SetVisuals()
    {
        _display.SetName(_actorName);
        _display.SetHealth(_currentHealth, _health);
        _display.SetMana(_currentMana, _mana);
    }

    public virtual void SetTurn(int position)
    {
        _display.SetTurnPosition(position);
    }
    
    public virtual void StartTurn()
    {
        _currentActionAmount = GetActionAmount();
        _currentBonusActionAmount = GetBonusActionAmount();
    }
    
    public virtual void EndTurn()
    {
        _actorBuffs.UpdateBuffsDurations();
    }
    
    public virtual DiceAmount GetInitiative()
    {
        return new DiceAmount(GetCombat(), GetInitiativeDice());
    }
    
    public virtual int GetDamage()
    {
        return _equipment.GetWeapon().damage;//Add Buffs
    }

    public virtual int GetDefense()
    {
        return 5 + _equipment.GetArmor().defense;//Add Buffs
    }

    public int GetCurrentMana()
    {
        return _currentMana;
    }
    
    public void RemoveMana(int amount)
    {
        _currentMana = Mathf.Clamp(_currentMana - amount,0,_mana);
        _display.SetMana(_currentMana, _mana);
    }
    
    public int GetCurrentActions()
    {
        return _currentActionAmount;
    }
    
    public void RemoveActions(int amount)
    {
        _currentActionAmount = Mathf.Clamp(_currentActionAmount - amount,0,GetActionAmount());
    }
    
    public int GetCurrentBonusActions()
    {
        return _currentBonusActionAmount;
    }
    
    public int GetHitBuff()
    {
        return _actorBuffs.GetHitBuff();
    }
    
    public int GetWoundBuff()
    {
        return _actorBuffs.GetWoundBuff();
    }
    
    public void RemoveBonusActions(int amount)
    {
        _currentBonusActionAmount = Mathf.Clamp(_currentBonusActionAmount - amount,0,GetBonusActionAmount());
    }

    public virtual void AddManaOnLevelUp()
    {
        _lastManaAdded = DiceManager.instance.Roll(GetManaDice()) + GetMind();
        _mana += _lastManaAdded;
    }
    
    public virtual void AddMana(int amount)
    {
        _currentMana += amount;
        _currentMana = Mathf.Clamp(_currentMana, 0, _mana);
        _display.SetMana(_currentMana,_mana);
    }
    
    public virtual void AddHealthOnLevelUp()
    {
        _lastHealthAdded = DiceManager.instance.Roll(GetHealthDice()) + GetResistance();
        _health += _lastHealthAdded;
    }
    
    public virtual void AddHealth(int amount)
    {
        _currentHealth += amount;
        _currentHealth = Mathf.Clamp(_currentHealth, 0, _health);
        _display.SetHealth(_currentHealth,_health);
    }

    public virtual void AddLevel()
    {
        AddHealthOnLevelUp();
        AddManaOnLevelUp();
    }
    
    public virtual void AddFirstLevel()
    {
        _lastHealthAdded = (int)GetHealthDice() + GetResistance();
        _health += _lastHealthAdded;
        _lastManaAdded = (int)GetManaDice() + GetMind();
        _mana += _lastManaAdded;
    }
    
    public virtual void ResetActor()
    {
        _currentHealth = _health;
        _currentMana = _mana;
        SetVisuals();
    }

    public virtual bool IsAlive()
    {
        return _currentHealth > 0;
    }

    public virtual void TakeDamage(int amount)
    {
        _currentHealth = Mathf.Clamp(_currentHealth - amount, 0, _health);
        _display.SetHealth(_currentHealth,_health);
        if(!IsAlive())
            OnDie();
    }
    
    public Vector2 GetDicePosition()
    {
        return _display.GetDicePosition();
    }
    
    //DELETE
    public List<Ability> GetAbilities()
    {
        return _abilities;
    }
}