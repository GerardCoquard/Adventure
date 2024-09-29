using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;
using UnityEngine.UI;

public class DiceRoll : MonoBehaviour
{
    [SerializeField] private Image _sprite;
    [SerializeField] private TextMeshProUGUI _numberText;
    private int _result;
    private float _startTime;
    private float _timeToShowNumber;

    public void SetParameters(int result, Sprite sprite, float startTime, float timeToShowNumber)
    {
        _result = result;
        _numberText.text = _result.ToString();
        _sprite.sprite = sprite;
        _sprite.color = new Color(_sprite.color.r, _sprite.color.g, _sprite.color.b, 1);
        _startTime = startTime;
        _timeToShowNumber = timeToShowNumber;
    }

    private void OnEnable()
    {
        StartCoroutine(Shuffle(_startTime, _timeToShowNumber));
    }

    public int GetResult()
    {
        return _result;
    }

    IEnumerator Shuffle(float startTime, float timeToShowNumber)
    {
        _numberText.alpha = 0;
        
        yield return new WaitForSeconds(startTime);

        float time = 0;
        
        while (time < timeToShowNumber)
        {
            _numberText.alpha = Mathf.Lerp(0, 1, time / timeToShowNumber);
            time += Time.deltaTime;
            yield return null;
        }
        
        _numberText.alpha = 1;
    }
}
