using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minion_AI : MonoBehaviour
{
    public float enemyMoveSpeed = 2;
    private float currentEnemyMoveSpeed;

    public int maxEnemyHealth = 1;
    private int currentEnemyHealth;

    Transform target;
    
    void Start()
    {
        currentEnemyMoveSpeed = enemyMoveSpeed;

        currentEnemyHealth = maxEnemyHealth;

        target = GameObject.Find("Player/enemyTarget").transform;
    }

    void Update()
    {
        if(currentEnemyHealth <= 0)
        {
            Destroy(gameObject);
        }
        transform.LookAt(target.position);
        transform.position += transform.forward * currentEnemyMoveSpeed * Time.deltaTime;
    }

    void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "Axe") //GET THE TIMERS RIGHT FOR ATTACKING ENEMY
        {

            //StartCoroutine(Wait());
            currentEnemyHealth -= GameObject.Find("Player").GetComponent<playerAttack>().playerDamageValue;
            //Debug.Log(currentEnemyHealth);
        }
    }
}
