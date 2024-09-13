using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    public static Action OnDialogueEnded;
    public static DialogueManager instance;

    private void Awake()
    {
        instance = this;
    }

    private void OnDestroy()
    {
        OnDialogueEnded = null;
    }

    public void LoadDialogue(Dialogue dialogue)
    {
        EndDialogue();
    }

    private void EndDialogue()
    {
        OnDialogueEnded?.Invoke();
    }
}
