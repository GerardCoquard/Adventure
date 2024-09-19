using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatManager : MonoBehaviour
{
    public static CombatManager instance;

    private void Awake()
    {
        instance = this;
    }
}
