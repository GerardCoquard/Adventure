using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TEST : MonoBehaviour
{
    public DiceAmount diceAmount;
    public int bonus;
    public Transform parent;
    public GameObject roller;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            int total = RollWithVisuals(diceAmount, bonus,transform.position);
        }
    }
    
    
    public int RollWithVisuals(DiceAmount diceAmount, int bonus, Vector2 position)
    {
        int total = 0;
        int[] results = new int[diceAmount.amount];
        
        for (int i = 0; i < diceAmount.amount; i++)
        {
            int result = Random.Range(1, (int)diceAmount.dice+1);
            results[i] = result;
            total += result;
        }

        total += bonus;
        
        Roll3D roll = Instantiate(roller, Camera.main.WorldToScreenPoint(position), Quaternion.identity, parent).GetComponent<Roll3D>();
        roll.RollDices(diceAmount, results, bonus, total);
        return total;
    }
}
