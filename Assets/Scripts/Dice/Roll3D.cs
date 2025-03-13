using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;

public class Roll3D : MonoBehaviour
{
    [SerializeField] private Transform _diceHolder;
    [SerializeField] private List<DiceRollUI> _diceRollUIList;
    [SerializeField] private TextMeshProUGUI _totalText;
    [SerializeField] private TextMeshProUGUI _bonusText;
    [SerializeField] private float _startTime;
    [SerializeField] private float _timeToShowNumber;
    [SerializeField] private float _resultDelay;
    private int _diceCount;
    private int _stoppedDiceCount;
    private List<Dice3D> _diceList;

    public Action OnResultShown;
    
    public void RollDices(DiceAmount diceAmount, int[] results, int bonus, int total)
    {
        gameObject.SetActive(false);
        _diceList = Dice3DManager.instance.GetAvailableDice(diceAmount);
        for (int i = 0; i < diceAmount.amount; i++)
        {
            _diceRollUIList[i].SetParameters(results[i], _diceList[i].GetRenderTexture(), _startTime, _timeToShowNumber);
            _diceList[i].OnDiceStopped += _diceRollUIList[i].ShowText;
            _diceList[i].RollDice();

            _diceList[i].OnDiceStopped += DiceStopped;
        }

        _diceCount = diceAmount.amount;
        
        _bonusText.text = " + " + bonus;
        _bonusText.gameObject.SetActive(bonus > 0);
        
        
        _totalText.gameObject.SetActive(true);
        _totalText.text = " = " + total;
        _totalText.alpha = 0;
        
        LayoutRebuilder.ForceRebuildLayoutImmediate(transform.GetComponent<RectTransform>());
        gameObject.SetActive(true);
    }

    private void DiceStopped()
    {
        _stoppedDiceCount++;
        if (_stoppedDiceCount >= _diceCount)
            StartCoroutine(ShowTotal());
    }

    IEnumerator ShowTotal()
    {
        yield return new WaitForSeconds(_startTime + _timeToShowNumber + _resultDelay);
        
        float time = 0;
        
        while (time < _timeToShowNumber)
        {
            _totalText.alpha = Mathf.Lerp(0, 1, time / _timeToShowNumber);
            time += Time.deltaTime;
            yield return null;
        }
        
        _totalText.alpha = 1;
        
        OnResultShown?.Invoke();
    }

    public void DeleteRoll()
    {
        foreach (Dice3D dice in _diceList)
        {
            dice.DisableDice();
        }
        
        Destroy(gameObject);
    }
}
