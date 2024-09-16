using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelUpManager : MonoBehaviour
{
    public static Action OnLevelUpEnded;

    private void Start()
    {
        LevelManager.OnLevelEnded += CheckLevelUp;
    }

    private void OnDisable()
    {
        OnLevelUpEnded = null;
        LevelManager.OnLevelEnded -= CheckLevelUp;
    }

    public void CheckLevelUp()
    {
        if(true)
            OnLevelUpEnded?.Invoke();
        else
            LevelUp();
    }

    private void LevelUp()
    {
        
    }
}
