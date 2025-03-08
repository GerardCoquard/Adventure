using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerActorsManager : MonoBehaviour
{
    public static PlayerActorsManager instance;
    
    private List<ActorPlayer> _playerActors = new List<ActorPlayer>();
    [SerializeField] private Transform _actorsHolder;
    [SerializeField] private List<EnemyData> _playerDatas = new List<EnemyData>();//TO DELETE IN FUTURE

    private void Awake()
    {
        instance = this;
        LoadPlayerActors();
    }

    private void Start()
    {
        GameManager.OnLevelStarted += ResetActors;
    }

    private void OnDisable()
    {
        GameManager.OnLevelStarted -= ResetActors;
    }

    private void LoadPlayerActors()
    {
        List<Vector2> positions = GridManager.instance.GetPlayerPositions(_playerDatas.Count);
        int indx = 0;
        foreach (EnemyData player in _playerDatas)
        {
            ActorPlayer playerActor = Instantiate(player.prefab, positions[indx], Quaternion.identity, _actorsHolder).GetComponent<ActorPlayer>();
            playerActor.InitializeActor(player.enemyStats, player._name);
            _playerActors.Add(playerActor);
            indx++;
        }
    }

    public List<ActorPlayer> GetPlayerActors()
    {
        return _playerActors;
    }

    private void ResetActors()
    {
        foreach (ActorPlayer actor in _playerActors)
        {
            actor.ResetActor();
        }
    }

    public bool AllActorsDead()
    {
        foreach (ActorPlayer actor in _playerActors)
        {
            if (actor.IsAlive()) return false;
        }

        return true;
    }
}
