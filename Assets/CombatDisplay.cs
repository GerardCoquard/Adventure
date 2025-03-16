using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CombatDisplay : MonoBehaviour
{
    [SerializeField] private GameObject _tray;
    [SerializeField] private TextMeshProUGUI _attackerText;
    [SerializeField] private TextMeshProUGUI _defenderText;
    [SerializeField] private TextMeshProUGUI _attackerNumber;
    [SerializeField] private TextMeshProUGUI _defenderNumber;
    [SerializeField] private TextMeshProUGUI _diceNeeded;
    [SerializeField] private TextMeshProUGUI _modifier;

    private void Start()
    {
        
    }

    public void SetCombatHit(int combat, int defense, int hit, int hitBonus)
    {
        _tray.SetActive(true);
        _attackerText.text = "Combat";
        _defenderText.text = "Defense";
        _attackerNumber.text = combat.ToString();
        _defenderNumber.text = defense.ToString();
        _diceNeeded.text = hit.ToString();
        _modifier.text = hitBonus.ToString();
        _modifier.gameObject.SetActive(hitBonus > 0);
    }
    
    public void SetCombatWound(int damage, int resistance, int wound, int woundBonus)
    {
        _attackerText.text = "Damage";
        _defenderText.text = "Resistance";
        _attackerNumber.text = damage.ToString();
        _defenderNumber.text = resistance.ToString();
        _diceNeeded.text = wound.ToString();
        _modifier.text = woundBonus.ToString();
        _modifier.gameObject.SetActive(woundBonus > 0);
    }

    public void HideTray()
    {
        _tray.SetActive(false);
    }
}
