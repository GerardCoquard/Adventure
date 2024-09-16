using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static Action<List<EnemyData>> OnRoomLoaded;
    public static Action OnLevelStarted;
    public static Action OnLevelEnded;
    
    [SerializeField] private List<Level> _levels;
    [SerializeField] private Dialogue _firstDialogue;
    private int _currentLevel;
    private int _currentRoom;

    private void Start()
    {
        LevelUpManager.OnLevelUpEnded += LoadDialogue;
        DialogueManager.OnDialogueEnded += LoadLevel;
        BattleManager.OnBattleEnd += NextRoom;
        BattleManager.OnPlayerLost += Respawn;
        
        if (_firstDialogue != null)
            DialogueManager.instance.LoadDialogue(_firstDialogue);
        else
            LoadLevel();
    }

    private void OnDisable()
    {
        OnRoomLoaded = null;
        OnLevelEnded = null;
        LevelUpManager.OnLevelUpEnded -= LoadDialogue;
        DialogueManager.OnDialogueEnded -= LoadLevel;
        BattleManager.OnBattleEnd -= NextRoom;
        BattleManager.OnPlayerLost -= Respawn;
    }

    private void PassLevel()
    {
        _currentLevel++;
        _currentRoom = 0;
        OnLevelEnded?.Invoke();
    }
    
    private void NextRoom()
    {
        if (_levels[_currentLevel].rooms.Count >= _currentRoom + 1)
        {
            PassLevel();
        }
        else
        {
            _currentRoom++;
            LoadRoom();
        }
    }

    public void Respawn()
    {
        _currentRoom = 0;
        LoadLevel();
    }

    private void LoadRoom()
    {
        List<EnemyData> enemies = _levels[_currentLevel].rooms[_currentRoom].enemies;
        OnRoomLoaded?.Invoke(enemies);
    }

    private void LoadDialogue()
    {
        DialogueManager.instance.LoadDialogue(_levels[_currentLevel].endDialogue);
    }
    
    private void LoadLevel()
    {
        OnLevelStarted?.Invoke();
        LoadRoom();
    }
    
    public int GetCurrentLevel()
    {
        return _currentLevel + 1;
    }
    
    public int GetCurrentRoom()
    {
        return _currentRoom + 1;
    }
}
