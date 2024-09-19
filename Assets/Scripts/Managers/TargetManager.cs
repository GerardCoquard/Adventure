using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetManager : MonoBehaviour
{
    public static TargetManager instance;

    public static Action<Ability> OnAbilitySelected;
    public static Action OnAbilityCanceled;
    public static Action OnTargetSelected;
    
    private Ability _currentAbility;
    private Action OnEscapePressed;

    private List<Actor> targets;

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
        if(Input.GetKeyDown(KeyCode.Escape))
            OnEscapePressed?.Invoke();
    }

    public void StartSelectingTargets(Ability ability)
    {
        if (_currentAbility == ability)
            return;
        
        if (_currentAbility == null)
            OnEscapePressed += EndSelectingTargets;
        
        _currentAbility = ability;
        OnAbilitySelected?.Invoke(_currentAbility);
    }

    private void EndSelectingTargets()
    {
        _currentAbility = null;
        OnAbilityCanceled?.Invoke();
        OnEscapePressed -= EndSelectingTargets;
    }
}
