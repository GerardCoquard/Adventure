using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyActorInput : ActorInput
{
    [SerializeField] private float turnDuration;
    
    public override void StartTurn()
    {
        StartCoroutine(WaitForPassingTurn());
    }
    
    IEnumerator WaitForPassingTurn()
    {
        yield return new WaitForSeconds(turnDuration);
        if(Input.GetKey(KeyCode.K))
            GetActor().OnDie();
        EndTurn();
    }
}
