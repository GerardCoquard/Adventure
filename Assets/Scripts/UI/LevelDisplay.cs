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
        GameManager.OnLevelStarted += UpdateLevel;
        GameManager.OnRoomLoaded += (x) => UpdateRoom();
    }

    private void OnDisable()
    {
        GameManager.OnLevelStarted -= UpdateLevel;
        GameManager.OnRoomLoaded -= (x) => UpdateRoom();
    }

    private void UpdateLevel()
    {
        _levelText.text = "Level: " + GameManager.instance.GetCurrentLevel();
        UpdateRoom();
    }

    private void UpdateRoom()
    {
        _roomText.text = "Room: " + GameManager.instance.GetCurrentRoom() + "/" + GameManager.instance.GetMaxRooms();
    }
}
