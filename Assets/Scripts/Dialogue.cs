using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Dialogue", menuName = "Dialogue", order = 3)]
public class Dialogue : ScriptableObject
{
    public List<DialogueNode> conversation;
}

[Serializable]
public struct DialogueNode
{
    public Characters character;
    [TextArea(1,10)] public string text;
}

public enum Characters
{
    Knight,
    Princess,
    Monster,
    Dog
}
