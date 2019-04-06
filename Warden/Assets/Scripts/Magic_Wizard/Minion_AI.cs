using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minion_AI : MonoBehaviour
{
    public float enemyMoveSpeed;

    public Animator minionAnimations;

    public int maxEnemyHealth = 1;
    private int currentEnemyHealth;

    Transform target;
    
    void Start()
    {
        enemyMoveSpeed = Random.Range(2, 5);

        currentEnemyHealth = maxEnemyHealth;

        target = GameObject.Find("Player/enemyTarget").transform;
    }

    void Update()
    {
        if(currentEnemyHealth <= 0)
        {
            minionAnimations.SetBool("isDead", true);
            this.gameObject.GetComponent<Collider>().enabled = false;
            this.gameObject.GetComponent<Minion_AI>().enabled = false;
        }

        if(enemyMoveSpeed > 2)
        {
            minionAnimations.SetBool("isRunning", true);
        }

        transform.LookAt(target.position);
        transform.position += transform.forward * enemyMoveSpeed * Time.deltaTime;
    }

    void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "playerAxe") //GET THE TIMERS RIGHT FOR ATTACKING ENEMY
        {

            //StartCoroutine(Wait());
            currentEnemyHealth -= GameObject.Find("Player").GetComponent<playerAttack>().playerDamageValue;
            //Debug.Log(currentEnemyHealth);
        }
    }
}
