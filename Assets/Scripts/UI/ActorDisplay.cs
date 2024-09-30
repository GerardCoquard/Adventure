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
    [Header("Buffs")]
    [SerializeField] private GameObject _buffDisplayPrefab;
    [SerializeField] private Transform _buffHitHolder;
    [SerializeField] private Transform _buffFNPHolder;
    [SerializeField] private Transform _buffThreatHolder;
    [SerializeField] private Transform _buffMagicDefenseHolder;

    private Dictionary<BuffData, BuffDisplay> _buffs;

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
    
    //Buffs

    public void AddHitBuff(BuffData buff)
    {
        CreateBuffDisplay(buff, _buffHitHolder);
    }
    
    public void AddFeelNoPainBuff(BuffData buff)
    {
        CreateBuffDisplay(buff, _buffFNPHolder);
    }
    
    public void AddThreatBuff(BuffData buff)
    {
        CreateBuffDisplay(buff, _buffThreatHolder);
    }
    
    public void AddMagicDefenseBuff(BuffData buff)
    {
        CreateBuffDisplay(buff, _buffMagicDefenseHolder);
    }

    private void CreateBuffDisplay(BuffData buff, Transform parent)
    {
        BuffDisplay buffDisplay = Instantiate(_buffDisplayPrefab, parent).GetComponent<BuffDisplay>();
        buffDisplay.SetBuffData(buff);
    }
    
    public void DeleteBuff(BuffData buff)
    {
        Destroy(_buffs[buff].gameObject);
    }
    
    public void UpdateBuff(BuffData buff)
    {
        _buffs[buff].UpdateDuration(buff.duration);
    }
}
