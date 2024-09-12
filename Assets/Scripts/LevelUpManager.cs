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

    private void OnDestroy()
    {
        OnLevelUpEnded = null;
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
