using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowDice : MonoBehaviour
{
    public Transform dice;
    public float distance;
    void Update()
    {
        transform.position = new Vector3(dice.position.x, dice.position.y + distance, dice.position.z);
    }
}
