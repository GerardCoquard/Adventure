using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Enemy", menuName = "Enemy", order = 2)]
public class EnemyData : ScriptableObject
{
    [Header("Visuals")]
    public GameObject prefab;
    public string _name;
    public int positioningLevel;
    [Header("Stats")]
    public EnemyStats enemyStats;
}
