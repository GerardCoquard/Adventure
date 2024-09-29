using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Roll : MonoBehaviour
{
    [SerializeField] private Transform _diceHolder;
    [SerializeField] private GameObject _dicePrefab;
    [SerializeField] private TextMeshProUGUI _totalText;
    [SerializeField] private TextMeshProUGUI _bonusText;
    [SerializeField] private float _startTime;
    [SerializeField] private float _timeToShowNumber;
    [SerializeField] private float _resultDelay;
    [SerializeField] private float _timeToDestroy;
    
    public void RollDices(DiceAmount diceAmount, int[] results, int bonus, int total, Sprite sprite, bool showTotal)
    {
        gameObject.SetActive(false);
        for (int i = 0; i < diceAmount.amount; i++)
        {
            DiceRoll dice = Instantiate(_dicePrefab, _diceHolder).GetComponent<DiceRoll>();
            dice.SetParameters(results[i], sprite, _startTime, _timeToShowNumber);
        }
        
        _bonusText.text = "+ " + bonus;
        _bonusText.alpha = 0;
        if(bonus == 0)
            _bonusText.gameObject.SetActive(false);
        
        _totalText.gameObject.SetActive(true);
        _totalText.text = " = " + total;
        _totalText.alpha = 0;
        
        LayoutRebuilder.ForceRebuildLayoutImmediate(transform.GetComponent<RectTransform>());
        gameObject.SetActive(true);
        
        StartCoroutine(Destroy());
        if (showTotal)
            StartCoroutine(ShowTotal());
    }

    IEnumerator ShowTotal()
    {
        yield return new WaitForSeconds(_startTime + _timeToShowNumber + _resultDelay);
        
        float time = 0;
        
        while (time < _timeToShowNumber)
        {
            _totalText.alpha = Mathf.Lerp(0, 1, time / _timeToShowNumber);
            _bonusText.alpha = Mathf.Lerp(0, 1, time / _timeToShowNumber);
            time += Time.deltaTime;
            yield return null;
        }
        
        _totalText.alpha = 1;
        _bonusText.alpha = 1;
    }
    
    IEnumerator Destroy()
    {
        yield return new WaitForSeconds(_startTime + _timeToShowNumber + _resultDelay + _timeToDestroy);

        Destroy(gameObject);
    }
}
