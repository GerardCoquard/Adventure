using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;
    
    public static Action<List<EnemyData>> OnRoomLoaded;
    public static Action OnLevelStarted;
    public static Action OnLevelEnded;
    
    [SerializeField] private List<Level> _levels;
    [SerializeField] private Dialogue _firstDialogue;
    private int _currentLevel;
    private int _currentRoom;

    private void Awake()
    {
        instance = this;
    }

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
        _currentRoom++;
        if (_currentRoom >= _levels[_currentLevel].rooms.Count)
            PassLevel();
        else
            LoadRoom();
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
    
    public int GetMaxRooms()
    {
        return _levels[_currentLevel].rooms.Count;
    }
}
