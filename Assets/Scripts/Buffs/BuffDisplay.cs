using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BuffDisplay : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _durationText;
    [SerializeField] private Image _icon;
    private string _description;

    public void SetBuffData(BuffData buffData)
    {
        _icon.sprite = buffData.icon;
        _description = buffData.description;
        UpdateDuration(buffData.duration);
    }

    public void UpdateDuration(int amount)
    {
        _durationText.text = amount.ToString();
    }
}
