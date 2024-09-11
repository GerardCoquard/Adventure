using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Enemy", menuName = "Enemies", order = 2)]
public class EnemyData : ScriptableObject
{
    public EnemyStats enemyStats;
}
