using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ActorInput : MonoBehaviour
{
    [SerializeField] protected Transform _turnIndicatorPosition;
    protected int _lastInitiativeRoll;
    protected Actor _actor;

    protected virtual void Awake()
    {
        _actor = GetComponent<Actor>();
    }

    public abstract void StartTurn();

    public virtual void EndTurn()
    {
        BattleManager.instance.EndCurrentTurn();
    }

    public virtual void SetInitiative(int initiative)
    {
        _lastInitiativeRoll = initiative;
    }

    public virtual int GetInitiative()
    {
        return _lastInitiativeRoll;
    }

    public Actor GetActor()
    {
        return _actor;
    }

    public Vector2 GetTurnIndicatorPosition()
    {
        return _turnIndicatorPosition.position;
    }
}
