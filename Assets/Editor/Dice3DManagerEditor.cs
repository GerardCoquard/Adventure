using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Dice3DManager))]
public class Dice3DManagerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        Dice3DManager manager = (Dice3DManager)target;

        if (GUILayout.Button("Reset Pool"))
        {
            manager.ResetPool();
        }
    }
}
