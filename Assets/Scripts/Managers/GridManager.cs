using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    public static GridManager instance;
    
    public float enemyWidth;
    public float playerWidth;
    public float verticalSpacing;
    public float lateralSpacing;

    private void Awake()
    {
        instance = this;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position + new Vector3(enemyWidth,5,0),transform.position + new Vector3(enemyWidth,-5,0));
        List<Vector2> pointsEnemy = GetEnemyPositions(5);
        foreach (Vector2 point in pointsEnemy)
        {
            Gizmos.DrawWireSphere(point,0.2f);
        }
        
        
        Gizmos.color = Color.blue;
        Gizmos.DrawLine(transform.position + new Vector3(playerWidth,5,0),transform.position + new Vector3(playerWidth,-5,0));
        List<Vector2> pointsPlayer = GetPlayerPositions(3);
        foreach (Vector2 point in pointsPlayer)
        {
            Gizmos.DrawWireSphere(point,0.2f);
        }
    }
    
    public List<Vector2> GetEnemyPositions(int amountOfActors)
    {
        List<Vector2> points = new List<Vector2>();
        bool even = amountOfActors % 2 == 0;
        int verticalMultiplier = -1;
        float verticalOffset = 0;

        if (!even)
            points.Add(new Vector2(enemyWidth - lateralSpacing, 0));
        else
        {
            verticalMultiplier *= -1;
            verticalOffset = verticalSpacing / 2;
        }
        
        for (int i = 0; i < amountOfActors/2; i++)
        {
            Vector2 pointUp = new Vector2(enemyWidth + verticalMultiplier * -lateralSpacing, verticalSpacing * (i+1) - verticalOffset);
            Vector2 pointDown = new Vector2(enemyWidth + verticalMultiplier * -lateralSpacing, -(verticalSpacing * (i+1) - verticalOffset));
            
            points.Add(pointUp);
            points.Add(pointDown);

            verticalMultiplier *= -1;
        }
        
        return points;
    }
    
    public List<Vector2> GetPlayerPositions(int amountOfActors)
    {
        List<Vector2> points = new List<Vector2>();
        bool even = amountOfActors % 2 == 0;
        int verticalMultiplier = 1;
        float verticalOffset = 0;

        if (!even)
            points.Add(new Vector2(playerWidth + lateralSpacing, 0));
        else
        {
            verticalMultiplier *= -1;
            verticalOffset = verticalSpacing / 2;
        }
        
        for (int i = 0; i < amountOfActors/2; i++)
        {
            Vector2 pointUp = new Vector2(playerWidth + verticalMultiplier * -lateralSpacing, verticalSpacing * (i+1) - verticalOffset);
            Vector2 pointDown = new Vector2(playerWidth + verticalMultiplier * -lateralSpacing, -(verticalSpacing * (i+1) - verticalOffset));
            
            points.Add(pointUp);
            points.Add(pointDown);

            verticalMultiplier *= -1;
        }
        
        return points;
    }
}
