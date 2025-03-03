using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;
using UnityEngine.UI;

public class DiceRollUI : MonoBehaviour
{
    [SerializeField] private RawImage _sprite;
    [SerializeField] private TextMeshProUGUI _numberText;
    private float _startTime;
    private float _timeToShowNumber;

    public void SetParameters(int result, Texture sprite, float startTime, float timeToShowNumber)
    {
        _numberText.text = result.ToString();
        _numberText.alpha = 0;
        _sprite.texture = sprite;
        _startTime = startTime;
        _timeToShowNumber = timeToShowNumber;
        
        gameObject.SetActive(true);
    }

    public void ShowText()
    {
        StartCoroutine(FadeInText(_startTime, _timeToShowNumber));
    }

    IEnumerator FadeInText(float startTime, float timeToShowNumber)
    {
        
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
