using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class TargetDisplay : MonoBehaviour
{
    [SerializeField] private string _useMessage;
    [SerializeField] private Color _enemyTargetColor;
    [SerializeField] private Color _allyTargetColor;
    [SerializeField] private TextMeshProUGUI _messageText;
    [SerializeField] private TextMeshProUGUI _currentSelectionText;
    [SerializeField] private List<Image> _targets;
    [SerializeField] private GameObject _holder;
    private Dictionary<Actor, Image> _targetsDictionary;
    private int abilityMaxTargets;

    private void Start()
    {
        TargetSelector.OnAbilitySelected += OnAbilitySelected;
        TargetSelector.OnTargetSelected += OnTargetSelected;
        TargetSelector.OnAbilityCanceled += Hide;
        _messageText.text = _useMessage;
    }

    private void OnAbilitySelected(Ability ability)
    {
        abilityMaxTargets = ability.targets;
        _targetsDictionary = new Dictionary<Actor, Image>();
        foreach (Image target in _targets)
        {
            target.gameObject.SetActive(false);
        }

        Color abilityColor;
        abilityColor = ability.targetTeam == Team.Other ? _enemyTargetColor : _allyTargetColor;

        _currentSelectionText.color = abilityColor;
        _currentSelectionText.text = "0/" + ability.targets;
        foreach (Image target in _targets)
        {
            target.color = abilityColor;
        }
        
        _holder.SetActive(true);
    }
    
    private void OnTargetSelected(Actor actor, int currentSelectionAmount)
    {
        _currentSelectionText.text = currentSelectionAmount+ "/" + abilityMaxTargets;
        if (_targetsDictionary.ContainsKey(actor))
        {
            _targetsDictionary[actor].gameObject.SetActive(false);
            _targetsDictionary.Remove(actor);
        }
        else
        {
            Image target = _targets.FirstOrDefault(t => !t.gameObject.activeInHierarchy);
            target.gameObject.SetActive(true);
            _targetsDictionary.Add(actor, target);
        }
            
    }

    private void Hide()
    {
        foreach (Image target in _targets)
        {
            target.gameObject.SetActive(false);
        }
        _holder.SetActive(false);
    }
}
