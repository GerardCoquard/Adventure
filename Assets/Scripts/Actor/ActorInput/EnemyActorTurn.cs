using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyActorTurn : ActorTurn
{
    [SerializeField] private float turnDuration;
    
    public override void StartTurn()
    {
        base.StartTurn();
        StartCoroutine(WaitForPassingTurn());
    }
    
    IEnumerator WaitForPassingTurn()
    {
        int rnd = Random.Range(0, 3);
        CombatManager.instance.StartCombat(GetActor(),PlayerActorsManager.instance.GetPlayerActors()[rnd]);
        yield return new WaitForSeconds(turnDuration);
        EndTurn();
    }
}
