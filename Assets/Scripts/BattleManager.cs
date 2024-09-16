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
    private int _currentTurn;
    
    [SerializeField] private Transform _enemiesHolder;
    [SerializeField] private float _timeToSeeInitiatives;
    [SerializeField] private GameObject _turnSprite;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        LevelManager.OnRoomLoaded += StartBattle;
    }

    private void OnDisable()
    {
        OnBattleEnd = null;
        OnPlayerLost = null;
        LevelManager.OnRoomLoaded -= StartBattle;
    }
    
    private void StartBattle(List<EnemyData> enemies)
    {
        DestroyPreviousActors();
        _battleActors = new List<ActorInput>();
        _enemyActors = new List<Actor>();
        LoadEnemyActors(enemies);
        LoadPlayerActors();
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
            _battleActors.Add(player.GetComponent<ActorInput>());
        }
    }

    public void EndCurrentTurn()
    {
        _currentTurn++;
        
        if (_currentTurn >= _battleActors.Count)
            _currentTurn = 0;


        if (CheckIfBattleEnd())
            EndBattle();
        else
            SetUpTurn();
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
        ActorInput currentActor = _battleActors[_currentTurn];
        currentActor.StartTurn();
        _turnSprite.transform.position = currentActor.GetTurnIndicatorPosition();
    }

    private void ThrowInitiatives()
    {
        foreach (ActorInput battleActor in _battleActors)
        {
            Actor actor = battleActor.GetActor();
            battleActor.SetInitiative(Utilities.RollWithVisuals(actor.GetInitiative()));
        }
    }

    private void ApplyTurnOrder()
    {
        _battleActors = _battleActors.OrderByDescending(a => a.GetInitiative()).ToList();
    }

    IEnumerator ThrowInitiativesAndStartFirstTurn()
    {
        ThrowInitiatives();
        yield return new WaitForSeconds(_timeToSeeInitiatives);
        ApplyTurnOrder();
        _currentTurn = 0;
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
        //Deactivate turn positions
    }

    public void RemoveActor(Actor actor)
    {
        ActorInput actorInput = _battleActors.Where(actorInput => actorInput.GetActor() == actor) as ActorInput;
        
        _battleActors.Remove(actorInput);
        
        if (_enemyActors.Contains(actorInput.GetActor()))
            _enemyActors.Remove(actorInput.GetActor());
        
        //Remake turn positions
    }
}
