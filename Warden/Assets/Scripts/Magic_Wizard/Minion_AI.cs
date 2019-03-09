using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minion_AI : MonoBehaviour
{
    public float enemyMoveSpeed = 2;
    private float currentEnemyMoveSpeed;

    Transform target;
    
    void Start()
    {
        currentEnemyMoveSpeed = enemyMoveSpeed;

        target = GameObject.Find("Player/enemyTarget").transform;
    }

    void Update()
    {
        transform.LookAt(target.position);
        transform.position += transform.forward * currentEnemyMoveSpeed * Time.deltaTime;
    }
}
