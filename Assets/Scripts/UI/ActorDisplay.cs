using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ActorDisplay : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _turnPosition;
    [SerializeField] private TextMeshProUGUI _actorName;
    [SerializeField] private Image _healthBar;
    [SerializeField] private TextMeshProUGUI _healthText;
    [SerializeField] private Image _manaBar;
    [SerializeField] private TextMeshProUGUI _manaText;
    [SerializeField] private Transform _diceThrowPosition;

    public void SetTurnPosition(int position)
    {
        if(position == 0)
            _turnPosition.text = "";
        else
            _turnPosition.text = position.ToString();
    }
    
    public void SetName(string newName)
    {
        _actorName.text = newName;
    }
    
    public void SetHealth(int current, int max)
    {
        _healthBar.fillAmount = (float)current / max;
        _healthText.text = current + " / " + max;
    }
    
    public void SetMana(int current, int max)
    {
        _manaBar.fillAmount = (float)current / max;
        _manaText.text = current + " / " + max;
    }

    public Vector2 GetDicePosition()
    {
        return _diceThrowPosition.position;
    }
}
