using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public abstract class Actor : MonoBehaviour
{
    protected Equipment _equipment;
    protected ActorDisplay _display;
    
    protected string _actorName;
    
    protected int _health;
    protected int _mana;
    protected int _lastHealthAdded;
    protected int _lastManaAdded;
    
    protected List<Ability> _abilities;

    protected int _currentHealth;
    protected int _currentMana;

    private void Awake()
    {
        _equipment = GetComponent<Equipment>();
        _display = GetComponentInChildren<ActorDisplay>();
    }
    
    //Getters
    
    public abstract int GetCombat();

    public abstract int GetMind();
    
    public abstract int GetResistance();
    
    public abstract Dice GetManaDice();
    
    public abstract Dice GetHealthDice();
    
    public abstract Dice GetInitiativeDice();
    
    public abstract int GetAttacks();

    public abstract int GetMagicResistance();
    
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
    
    public virtual DiceAmount GetInitiative()
    {
        return new DiceAmount(GetCombat(), GetInitiativeDice());//Add Buffs
    }
    
    public virtual int GetDamage()
    {
        return _equipment.GetWeapon().damage;//Add Buffs
    }

    public virtual int GetArmor()
    {
        return 5 + _equipment.GetArmor().defense;//Add Buffs
    }

    public virtual void AddMana()
    {
        _lastManaAdded = DiceManager.instance.Roll(GetManaDice()) + GetMind();
        _mana += _lastManaAdded;
    }
    
    public virtual void AddHealth()
    {
        _lastHealthAdded = DiceManager.instance.Roll(GetHealthDice()) + GetResistance();
        _health += _lastHealthAdded;
    }

    public virtual void AddLevel()
    {
        AddHealth();
        AddMana();
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
    }
    
    public Vector2 GetInitiativePosition()
    {
        return _display.GetInitiativePosition();
    }
    
    //DELETE
    public List<Ability> GetAbilities()
    {
        return _abilities;
    }
}