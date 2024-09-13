using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static Action OnRoomLoaded;
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
        
        if (_firstDialogue != null)
            DialogueManager.instance.LoadDialogue(_firstDialogue);
        else
            LoadRoom();
    }

    private void OnDestroy()
    {
        OnRoomLoaded = null;
        OnLevelEnded = null;
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

    private void Respawn()
    {
        _currentRoom = 0;
        //Set player characters to full HP again
        //Reset abilities cooldown
        //BattleManager do that
        LoadRoom();
    }

    private void LoadRoom()
    {
        List<EnemyData> enemies = _levels[_currentLevel].rooms[_currentRoom].enemies;
        List<Vector2> positions = GridManager.instance.GetEnemyPositions(enemies.Count);
        int indx = 0;
        foreach (EnemyData enemy in enemies.OrderByDescending(e => e.positioningLevel))
        {
            Instantiate(enemy.prefab, positions[indx], Quaternion.identity);
            indx++;
        }
        //BattleManager.LoadPlayerCharacters();
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
