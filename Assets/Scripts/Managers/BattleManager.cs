using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BattleManager : MonoBehaviour
{
    public static BattleManager instance;
    
    public static Action OnBattleEnd;
    public static Action OnPlayerLost;
    
    private List<ActorInput> _battleActors = new List<ActorInput>();
    private List<Actor> _enemyActors = new List<Actor>();
    private ActorInput _currentActorTurn;
    
    [SerializeField] private float _turnDelay;
    [SerializeField] private Transform _enemiesHolder;
    [SerializeField] private float _timeToSeeInitiatives;
    [SerializeField] private GameObject _turnSprite;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        GameManager.OnRoomLoaded += StartBattle;
    }

    private void OnDisable()
    {
        OnBattleEnd = null;
        OnPlayerLost = null;
        GameManager.OnRoomLoaded -= StartBattle;
    }
    
    private void StartBattle(List<EnemyData> enemies)
    {
        DestroyPreviousActors();
        _battleActors = new List<ActorInput>();
        _enemyActors = new List<Actor>();
        LoadEnemyActors(enemies);
        LoadPlayerActors();
        HideTurnPositions();
        StartCoroutine(ThrowInitiativesAndStartFirstTurn());
    }

    private void DestroyPreviousActors()
    {
        for (int i = 0; i < _enemyActors.Count; i++)
        {
            Destroy(_enemyActors[i].gameObject);
        }
    }

    private void LoadEnemyActors(List<EnemyData> enemies)
    {
        List<Vector2> positions = GridManager.instance.GetEnemyPositions(enemies.Count);
        int indx = 0;
        foreach (EnemyData enemy in enemies.OrderByDescending(e => e.positioningLevel))
        {
            ActorEnemy enemyActor = Instantiate(enemy.prefab, positions[indx], Quaternion.identity, _enemiesHolder).GetComponent<ActorEnemy>();
            enemyActor.InitializeActor(enemy.enemyStats, enemy._name);
            _battleActors.Add(enemyActor.GetComponent<ActorInput>());
            _enemyActors.Add(enemyActor);
            indx++;
        }
    }

    private void LoadPlayerActors()
    {
        List<ActorPlayer> playerActors = PlayerActorsManager.instance.GetPlayerActors();
        foreach (ActorPlayer player in playerActors)
        {
            if(player.IsAlive())
                _battleActors.Add(player.GetComponent<ActorInput>());
        }
    }

    public void EndCurrentTurn()
    {
        _currentActorTurn = _battleActors.Last() == _currentActorTurn ? _battleActors.First() : _battleActors[_battleActors.IndexOf(_currentActorTurn)+1];

        if (CheckIfBattleEnd())
            EndBattle();
        else
            StartCoroutine(TurnDelay());
    }

    private bool CheckIfBattleEnd()
    {
        if (PlayerActorsManager.instance.AllActorsDead())
        {
            OnPlayerLost?.Invoke();
            return true;
        }
        
        if (AllEnemyActorsDead())
        {
            OnBattleEnd?.Invoke();
            return true;
        }

        return false;
    }

    private void SetUpTurn()
    {
        _currentActorTurn.StartTurn();
        _turnSprite.transform.position = _currentActorTurn.GetTurnIndicatorPosition();
    }

    private void ThrowInitiatives()
    {
        foreach (ActorInput battleActor in _battleActors)
        {
            Actor actor = battleActor.GetActor();
            battleActor.SetInitiative(Utilities.RollWithVisuals(actor.GetInitiative()));
        }
        
        _battleActors = _battleActors.OrderByDescending(a => a.GetInitiative()).ToList();
    }

    IEnumerator ThrowInitiativesAndStartFirstTurn()
    {
        ThrowInitiatives();
        yield return new WaitForSeconds(_timeToSeeInitiatives);
        UpdateTurnPositions();
        _currentActorTurn = _battleActors.First();
        SetUpTurn();
        _turnSprite.SetActive(true);
    }
    
    public bool AllEnemyActorsDead()
    {
        foreach (Actor actor in _enemyActors)
        {
            if (actor.IsAlive()) return false;
        }

        return true;
    }

    private void EndBattle()
    {
        _turnSprite.SetActive(false);
        HideTurnPositions();
    }

    public void RemoveActor(Actor actor)
    {
        ActorInput actorInput = actor.GetComponent<ActorInput>();
        if (actorInput == _currentActorTurn)
            _currentActorTurn = _battleActors.First() == _currentActorTurn ? _battleActors.Last() : _battleActors[_battleActors.IndexOf(_currentActorTurn)-1];
        
        _battleActors.Remove(actorInput);
        
        if (_enemyActors.Contains(actorInput.GetActor()))
            _enemyActors.Remove(actorInput.GetActor());

        UpdateTurnPositions();
    }

    private void UpdateTurnPositions()
    {
        for (int i = 0; i < _battleActors.Count; i++)
        {
            _battleActors[i].GetActor().SetTurn(i+1);
        }
    }

    private void HideTurnPositions()
    {
        for (int i = 0; i < _battleActors.Count; i++)
        {
            _battleActors[i].GetActor().SetTurn(0);
        }
    }

    IEnumerator TurnDelay()
    {
        yield return new WaitForEndOfFrame();
        yield return new WaitForSeconds(_turnDelay);
        SetUpTurn();
    }
}
