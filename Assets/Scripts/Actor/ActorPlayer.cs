using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActorPlayer : Actor
{

    private EnemyStats _stats;

    public void InitializeActor(EnemyStats stats, string actorName)
    {
        _stats = stats;
        _actorName = actorName;
        _equipment.SetWeapon(new Weapon(_stats.damage));
        _equipment.SetArmor(new Armor(_stats.armor));
        LoadAbilities();
        LevelUpActor();
        ResetActor();
    }

    private void LoadAbilities()
    {
        _abilities = new List<Ability>();
        foreach (EnemyAbility ability in _stats._abilities)
        {
            _abilities.Add(ability._ability);
        }
    }

    private void LevelUpActor()
    {
        AddFirstLevel();
        for (int i = 1; i < _stats.level; i++)
        {
            AddLevel();
        }
    }

    public override int GetCombat()
    {
        return _stats.combat;
    }

    public override int GetMind()
    {
        return _stats.mind;
    }

    public override int GetResistance()
    {
        return _stats.resistance;
    }

    public override Dice GetManaDice()
    {
        return _stats.manaDice;
    }

    public override Dice GetHealthDice()
    {
        return _stats.healthDice;
    }

    public override Dice GetInitiativeDice()
    {
        return _stats.initiativeDice;
    }

    public override int GetAttacks()
    {
        return _stats.attacks;
    }

    public override int GetMagicResistance()
    {
        return _stats.magicResistance;
    }
    
    public override int GetActionAmount()
    {
        return _stats.actionAmount;
    }

    public override int GetBonusActionAmount()
    {
        return _stats.bonusActionAmount;
    }

    public void GetThreat()
    {
        
    }

    public override void OnDie()
    {
        BattleManager.instance.RemoveActor(this);
        _currentHealth -= _currentHealth;//DELETE
        _currentMana -= _currentMana;
        SetTurn(0);
        _display.SetHealth(_currentHealth, _health);
        _display.SetMana(_currentMana, _mana);
        
        //TO DO
    }
}
