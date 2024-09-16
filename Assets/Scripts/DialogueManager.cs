using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager instance;
    
    public static Action OnDialogueEnded;

    public GameObject dialogueTest;

    private void Awake()
    {
        instance = this;
        dialogueTest.SetActive(false);
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
        dialogueTest.SetActive(false);
        OnDialogueEnded?.Invoke();
    }

    IEnumerator DialogueTest()
    {
        dialogueTest.SetActive(true);
        yield return new WaitForSeconds(3);
    }
}
