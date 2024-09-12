using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    public static GridManager instance;
    
    public float enemyWidth;
    public float playerWidth;
    public float lateralSpacing;
    public float verticalSpacing;

    private void Awake()
    {
        instance = this;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position + new Vector3(enemyWidth,20,0),transform.position + new Vector3(enemyWidth,-20,0));
        for (int i = 0; i < 3; i++)
        {
            int multiplier = i % 2 == 0 ? 1 : -1;
            
            Vector2 pointDown = new Vector2(enemyWidth + multiplier * verticalSpacing, -lateralSpacing * i);
            Gizmos.DrawWireSphere(pointDown,0.2f);
            Vector2 pointUp = new Vector2(enemyWidth + multiplier * verticalSpacing, lateralSpacing * i);
            Gizmos.DrawWireSphere(pointUp,0.2f);
        }
        
        
        Gizmos.color = Color.blue;
        Gizmos.DrawLine(transform.position + new Vector3(playerWidth,20,0),transform.position + new Vector3(playerWidth,-20,0));
        for (int i = 0; i < 2; i++)
        {
            int multiplier = i % 2 == 0 ? 1 : -1;
            
            Vector2 pointDown = new Vector2(playerWidth + multiplier * verticalSpacing, -lateralSpacing * i);
            Gizmos.DrawWireSphere(pointDown,0.2f);
            Vector2 pointUp = new Vector2(playerWidth + multiplier * verticalSpacing, lateralSpacing * i);
            Gizmos.DrawWireSphere(pointUp,0.2f);
        }
    }
    
    public List<Vector2> GetEnemyPositions(int amountOfActors)
    {
        List<Vector2> points = new List<Vector2>();
        bool even = amountOfActors % 2 == 0;
        float firstPoint = amountOfActors / 2;
        if (even) firstPoint -= lateralSpacing / 2;
        
        for (int i = 0; i < amountOfActors/2; i++)
        {
            float distanceFromCenter = firstPoint - lateralSpacing * i;
            int multiplier = i % 2 == 0 ? 1 : -1;
            
            Vector2 pointDown = new Vector2(enemyWidth + multiplier * verticalSpacing, -distanceFromCenter);
            Vector2 pointUp = new Vector2(enemyWidth + multiplier * verticalSpacing, distanceFromCenter);
            
            points.Add(pointDown);
            points.Add(pointUp);
        }
        if(!even) points.Add(new Vector2(enemyWidth - verticalSpacing, 0));

        points.Reverse();
        
        return points;
    }
    
    public List<Vector2> GetPlayerPositions(int amountOfActors)
    {
        List<Vector2> points = new List<Vector2>();
        bool even = amountOfActors % 2 == 0;
        float firstPoint = amountOfActors / 2;
        if (even) firstPoint -= lateralSpacing / 2;
        
        for (int i = 0; i < amountOfActors/2; i++)
        {
            float distanceFromCenter = firstPoint - lateralSpacing * i;
            int multiplier = i % 2 == 0 ? 1 : -1;
            
            Vector2 pointDown = new Vector2(playerWidth + multiplier * verticalSpacing, -distanceFromCenter);
            Vector2 pointUp = new Vector2(playerWidth + multiplier * verticalSpacing, distanceFromCenter);
            
            points.Add(pointDown);
            points.Add(pointUp);
        }
        if(!even) points.Add(new Vector2(playerWidth - verticalSpacing, 0));

        points.Reverse();
        
        return points;
    }
}
