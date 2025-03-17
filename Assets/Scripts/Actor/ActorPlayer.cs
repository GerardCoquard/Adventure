using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActorPlayer : Actor
{
    private int _level;
    private int _combat;
    private int _mind;
    private int _resistance;
    private Dice _manaDice;
    private Dice _healthDice;
    private Dice _initiativeDice;
    private int _initiativeBonus;
    private int _attacks;
    private int _magicResistance;
    private int _actionAmount;
    private int _bonusActionAmount;

    public void InitializeActorCustom(EnemyStats _stats, string actorName)
    {
        _actorName = actorName;
        _equipment.SetWeapon(_stats.weapon);
        _equipment.SetArmor(_stats.armor);
        LoadAbilitiesCustom(_stats);

        _level = _stats.level;
        _combat = _stats.combat;
        _mind = _stats.mind;
        _resistance = _stats.resistance;
        _manaDice = _stats.manaDice;
        _healthDice = _stats.healthDice;
        _initiativeDice = _stats.initiativeDice;
        _initiativeBonus = _stats.initiativeBonus;
        _attacks = _stats.attacks;
        _magicResistance = _stats.magicResistance;
        _actionAmount = _stats.actionAmount;
        _bonusActionAmount = _stats.bonusActionAmount;
        
        LevelUpActor();
        ResetActor();
    }
    
    public void InitializeActor()
    {
        //TODO
    }

    private void LoadAbilitiesCustom(EnemyStats _stats)
    {
        _abilities = new List<Ability>();
        foreach (EnemyAbility ability in _stats.abilities)
        {
            _abilities.Add(ability._ability);
        }
    }
    
    private void LoadAbilities()
    {
        _abilities = new List<Ability>();
    }

    private void LevelUpActor()
    {
        AddFirstLevel();
        for (int i = 1; i < _level; i++)
        {
            AddLevel();
        }
    }

    public override int GetCombat()
    {
        return _combat;
    }

    public override int GetMind()
    {
        return _mind;
    }

    public override int GetResistance()
    {
        return _resistance;
    }

    public override Dice GetManaDice()
    {
        return _manaDice;
    }

    public override Dice GetHealthDice()
    {
        return _healthDice;
    }

    public override Dice GetInitiativeDice()
    {
        return _initiativeDice;
    }
    
    public override int GetInitiativeBonus()
    {
        return _initiativeBonus;
    }

    public override int GetAttacks()
    {
        return _attacks;
    }

    public override int GetMagicResistance()
    {
        return _magicResistance;
    }
    
    public override int GetActionAmount()
    {
        return _actionAmount;
    }

    public override int GetBonusActionAmount()
    {
        return _bonusActionAmount;
    }

    public int GetThreat()
    {
        return 5 + _equipment.GetThreat() + _actorBuffs.GetThreatBuff();//Default value of 5?
    }

    public void AddInitiativeBonus(int amount)
    {
        _initiativeBonus += amount;
    }

    public override void OnDie()
    {
        BattleManager.instance.RemoveActor(this);
        _currentHealth -= _currentHealth;//DELETE
        _currentMana -= _currentMana;//DELETE?
        SetTurn(0);
        _display.SetHealth(_currentHealth, _health);
        _display.SetMana(_currentMana, _mana);
        //No targeteable for abilities
        
        //TO DO
    }
}
