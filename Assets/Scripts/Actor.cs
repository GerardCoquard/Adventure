using System;
using Unity.VisualScripting;
using UnityEngine;

public abstract class Actor : MonoBehaviour
{
    //TO DELETE
    public IActorInput actorInput;
    public Transform turnIndicator;
    public int initiative;
    //TO DELETE

    protected Equipment _equipment;
    protected int _health;
    protected int _mana;
    protected int _lastHealthAdded;
    protected int _lastManaAdded;

    private void Awake()
    {
        //TO DELETE
        actorInput = GetComponent<IActorInput>();
        //TO DELETE

        _equipment = GetComponent<Equipment>();
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

    public virtual int GetMana()
    {
        return _mana;
    }

    public virtual int GetHealth()
    {
        return _health;
    }
    
    public virtual DiceAmount GetInitiative()
    {
        return new DiceAmount(GetCombat(), GetInitiativeDice());
    }

    public virtual int GetDamage()
    {
        return _equipment.GetWeapon().damage;
    }

    public virtual int GetArmor()
    {
        return 5 + _equipment.GetArmor().defense;
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

}