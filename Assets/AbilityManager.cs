using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class AbilityManager : MonoBehaviour
{
    public static AbilityManager instance;
    
    [SerializeField] private List<AbilitySlotUI> _slots;
    [SerializeField] private GameObject _skipTurnButton;
    [SerializeField] private TextMeshProUGUI _actions;
    [SerializeField] private TextMeshProUGUI _bonusActions;

    private ActorTurn _currentActorTurn;

    private void Awake()
    {
        instance = this;
    }

    public void SkipTurn()
    {
        DisableUI();
        _currentActorTurn.EndTurn();
    }

    public void SetActorTurn(ActorTurn actorTurn)
    {
        _currentActorTurn = actorTurn;
        SetAbilitySlots();
        _skipTurnButton.SetActive(true);
        _actions.gameObject.SetActive(true);
        _bonusActions.gameObject.SetActive(true);
        UpdateResources();
    }

    private void SetAbilitySlots()
    {
        List<Ability> abilities = _currentActorTurn.GetActor().GetAbilities();
        for (int i = 0; i < abilities.Count; i++)
        {
            _slots[i].SetSlot(abilities[i]);
            _slots[i].OnAbilityPressed += SelectAbility;
        }
    }

    public bool HasResources(Ability ability)
    {
        if (_currentActorTurn.GetActor().GetCurrentMana() < ability.manaCost) return false;
        if (_currentActorTurn.GetActor().GetCurrentActions() < ability.actionCost) return false;
        if (_currentActorTurn.GetActor().GetCurrentBonusActions() < ability.bonusActionCost) return false;

        return true;
    }

    private void SelectAbility(AbilitySlotUI slot)
    {
        //Unmark previous slot
        //Mark the slot being used
        TargetSelector.instance.StartSelectingTargetsPlayer(slot.GetAbility());
    }

    public void UseAbility(Ability ability, List<Actor> targets)
    {
        _currentActorTurn.GetActor().RemoveMana(ability.manaCost);
        _currentActorTurn.GetActor().RemoveActions(ability.actionCost);
        _currentActorTurn.GetActor().RemoveBonusActions(ability.bonusActionCost);
        UpdateResources();
        //Use ability
    }

    private void UpdateResources()
    {
        _actions.text = _currentActorTurn.GetActor().GetCurrentActions().ToString();
        _bonusActions.text = _currentActorTurn.GetActor().GetCurrentBonusActions().ToString();
    }

    private void DisableUI()
    {
        foreach (AbilitySlotUI slot in _slots)
        {
            slot.DisableSlot();
        }
        
        _skipTurnButton.SetActive(false);
        _actions.gameObject.SetActive(false);
        _bonusActions.gameObject.SetActive(false);
    }
}
