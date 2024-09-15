using UnityEngine;

public class TestActorInput : MonoBehaviour, IActorInput
{
    public void StartTurn()
    {
        BattleManager.instance.NextTurn();
    }
}