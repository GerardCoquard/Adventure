using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;
using UnityEngine.UI;

public class DiceRoll : MonoBehaviour
{
    [SerializeField] private int maxNumber;
    [SerializeField] private float _startTime;
    [SerializeField] private float _timeToShowNumber;
    [SerializeField] private float _endTime;
    [SerializeField] private Image _sprite;
    [SerializeField] private TextMeshProUGUI _numberText;
    private int _result;
    private int _maxResult;

    public void SetRoll(int result, int maxResult, Sprite sprite)
    {
        _result = result;
        _sprite.sprite = sprite;
        _maxResult = maxResult;
        StartCoroutine(Shuffle());
    }

    public int GetResult()
    {
        return _result;
    }

    IEnumerator Shuffle()
    {
        _numberText.alpha = 0;
        
        yield return new WaitForSeconds(_startTime);

        float time = 0;
        
        while (time < _timeToShowNumber)
        {
            _numberText.alpha = Mathf.Lerp(0, 1, time / _timeToShowNumber);
            time += Time.deltaTime;
            yield return null;
        }
        
        _numberText.alpha = 1;
        
        yield return new WaitForSeconds(_endTime);
    }
}
