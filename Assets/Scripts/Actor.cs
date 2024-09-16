using System;
using Unity.VisualScripting;
using UnityEngine;

public abstract class Actor : MonoBehaviour
{
    protected Equipment _equipment;
    protected ActorVisuals _visuals;
    
    protected string _actorName;
    
    protected int _health;
    protected int _mana;
    protected int _lastHealthAdded;
    protected int _lastManaAdded;

    protected int _currentHealth;
    protected int _currentMana;

    private void Awake()
    {
        _equipment = GetComponent<Equipment>();
        _visuals = GetComponentInChildren<ActorVisuals>();
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

    public virtual void SetVisuals()
    {
        _visuals.SetName(_actorName);
        _visuals.SetHealth(_currentHealth, GetMaxHealth());
        _visuals.SetMana(_currentMana, GetMaxMana());
    }

    public virtual void SetTurn(int position)
    {
        _visuals.SetTurnPosition(position);
    }

    public virtual int GetMaxMana()
    {
        return _mana;//Add Buffs
    }

    public virtual int GetMaxHealth()
    {
        return _health;//Add Buffs
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
        _lastManaAdded = Utilities.Roll(GetManaDice()) + GetMind();
        _mana += _lastManaAdded;
    }
    
    public virtual void AddHealth()
    {
        _lastHealthAdded = Utilities.Roll(GetHealthDice()) + GetResistance();
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
        _currentHealth = GetMaxHealth();
        _currentHealth = GetMaxHealth();
        SetVisuals();
    }

    public virtual bool IsAlive()
    {
        return _currentHealth > 0;
    }

}