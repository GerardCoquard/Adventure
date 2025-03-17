using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AbilitySlotUI : MonoBehaviour
{
    [SerializeField] private Image _icon;
    [SerializeField] private TextMeshProUGUI _actionText;
    [SerializeField] private TextMeshProUGUI _bonusActionText;
    [SerializeField] private TextMeshProUGUI _manaText;
    
    
    
    public Action<AbilitySlotUI> OnAbilityPressed;

    private Ability _ability;

    public void SetSlot(Ability ability)
    {
        _ability = ability;
        SetVisuals();
        gameObject.SetActive(true);
    }

    public void SlotPressed()
    {
        if(AbilityManager.instance.HasResources(_ability))
            OnAbilityPressed?.Invoke(this);
    }

    public void DisableSlot()
    {
        OnAbilityPressed = null;
        gameObject.SetActive(false);
    }

    public Ability GetAbility()
    {
        return _ability;
    }

    private void SetVisuals()
    {
        _icon.sprite = _ability.icon;
        
        
        if (_ability.actionCost > 0)
        {
            _actionText.text = _ability.actionCost.ToString();
            _actionText.gameObject.SetActive(true);
        }
        else
            _actionText.gameObject.SetActive(false);
        
        if (_ability.bonusActionCost > 0)
        {
            _bonusActionText.text = _ability.bonusActionCost.ToString();
            _bonusActionText.gameObject.SetActive(true);
        }
        else
            _bonusActionText.gameObject.SetActive(false);
        
        if (_ability.manaCost > 0)
        {
            _manaText.text = _ability.manaCost.ToString();
            _manaText.gameObject.SetActive(true);
        }
        else
            _manaText.gameObject.SetActive(false);
    }
}
