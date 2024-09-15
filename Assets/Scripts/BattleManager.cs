using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BattleManager : MonoBehaviour
{
    public static BattleManager instance;
    public List<Actor> battleActors = new List<Actor>();
    public GameObject turnSprite;
    private int currentTurn = 0;
    private void Start()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }
        
        InitBattle();
    }
    
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            battleActors[currentTurn].actorInput.StartTurn();
        }
    }
    public void InitBattle()
    {
        SetUpInitiative();
        SortActorList();
        SetUpTurn();
    }

    public void NextTurn()
    {
        if (currentTurn >= (battleActors.Count - 1))
        {
            currentTurn = 0;
        }
        else
        {
            currentTurn++;
        }
        SetUpTurn();
    }

    private void SetUpTurn()
    {
        Actor currentActor = battleActors[currentTurn];
        //currentActor.actorInput.StartTurn();
        turnSprite.transform.position = currentActor.turnIndicator.position;
    }

    private void SetUpInitiative()
    {
        foreach (Actor battleActor in battleActors)
        {
            var diceResult= Utilities.GetDiceThrow(new DiceAmount(1, Dice.D6));
            battleActor.initiative = diceResult[0];
        }
    }

    private void SortActorList()
    {
        battleActors = battleActors.OrderBy(a => a.initiative).ToList();
    }
}

public interface IActorInput
{
    public void StartTurn()
    {
        
    }
}
