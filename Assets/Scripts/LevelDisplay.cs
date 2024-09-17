using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LevelDisplay : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _levelText;
    [SerializeField] private TextMeshProUGUI _roomText;
    
    private void Start()
    {
        LevelManager.OnLevelStarted += UpdateLevel;
        LevelManager.OnRoomLoaded += (x) => UpdateRoom();
    }

    private void OnDisable()
    {
        LevelManager.OnLevelStarted -= UpdateLevel;
        LevelManager.OnRoomLoaded -= (x) => UpdateRoom();
    }

    private void UpdateLevel()
    {
        _levelText.text = "Level: " + LevelManager.instance.GetCurrentLevel();
        UpdateRoom();
    }

    private void UpdateRoom()
    {
        _roomText.text = "Room: " + LevelManager.instance.GetCurrentRoom() + "/" + LevelManager.instance.GetMaxRooms();
    }
}
