using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetSelector : MonoBehaviour
{
    public static TargetSelector instance;

    [SerializeField] private LayerMask _whatIsActor;
    
    public static Action<Ability> OnAbilitySelected;
    public static Action OnAbilityCanceled;
    public static Action<Actor,int> OnTargetSelected;
    
    private Ability _currentAbility;
    private Action OnEscapePressed;

    private List<Actor> _targets;

    private void Awake()
    {
        instance = this;
    }

    private void OnDestroy()
    {
        OnAbilitySelected = null;
        OnAbilityCanceled = null;
    }

    private void Update()
    {
        if(_currentAbility == null)
            return;
        
        if(Input.GetKeyDown(KeyCode.Escape))
            OnEscapePressed?.Invoke();
        if (Input.GetMouseButtonDown(0))
            CheckSelection();
        if (Input.GetKeyDown(KeyCode.Return))
            TriggerAbility();
    }

    public void StartSelectingTargetsPlayer(Ability ability)
    {
        if (_currentAbility == ability)
            return;

        if (_currentAbility == null)
        {
            OnEscapePressed += EndSelectingTargets;
        }

        UnselectTargets();
        _currentAbility = ability;
        OnAbilitySelected?.Invoke(_currentAbility);
    }
    
    public void StartSelectingTargetsEnemy(Ability ability)
    {
        
    }

    private void UnselectTargets()
    {
        _targets = new List<Actor>();
    }

    public void EndSelectingTargets()
    {
        _currentAbility = null;
        OnAbilityCanceled?.Invoke();
        OnEscapePressed -= EndSelectingTargets;
    }

    private void CheckSelection()
    {
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector2.zero, Mathf.Infinity, _whatIsActor);
        if (hit.collider != null)
        {
            Actor actor = hit.collider.GetComponentInParent<Actor>();
            if(ValidTarget(actor))
                SelectTarget(actor);
        }
    }

    private void SelectTarget(Actor actor)
    {
        if (_targets.Contains(actor))
        {
            _targets.Remove(actor);
            OnTargetSelected?.Invoke(actor,_targets.Count);
            return;
        }

        if (_targets.Count >= _currentAbility.targets)
        {
            //Max targets selected
        }
        else
        {
            _targets.Add(actor);
            OnTargetSelected?.Invoke(actor,_targets.Count);
        }
            
    }
    

    private bool ValidTarget(Actor actor)
    {
        bool isAlly = actor as ActorEnemy == null;
        return (isAlly && _currentAbility.targetTeam == Team.Self) ||
               (!isAlly && _currentAbility.targetTeam == Team.Other);
    }

    private void TriggerAbility()
    {
        if(_targets.Count != 0)
            AbilityManager.instance.UseAbility(_currentAbility,_targets);
            
        EndSelectingTargets();
    }
}
