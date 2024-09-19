using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class TargetDisplay : MonoBehaviour
{
    [SerializeField] private string _useMessage;
    [SerializeField] private string _targetMessage;
    [SerializeField] private char _splitter;
    [SerializeField] private Color _enemyTargetColor;
    [SerializeField] private Color _allyTargetColor;
    [SerializeField] private TextMeshProUGUI _messageText;
    [SerializeField] private Image[] _targetIcons;

    private void Start()
    {
        TargetManager.OnAbilitySelected += OnAbilitySelected;
    }

    public void OnAbilitySelected(Ability ability)
    {
        _messageText.gameObject.SetActive(true);
        //Set params depending on ability
    }
}
