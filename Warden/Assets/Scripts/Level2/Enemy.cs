using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int maxEnemyHealth = 20;
    private int currentEnemyHealth;

    public float enemyMoveSpeed = 2;
    private float currentEnemyMoveSpeed;

    public int enemyDamageValue = 10;

    public float rangeToAttack = 1.8f;
    private float distanceFromPlayer;

    public float enemyAttackTimer = 1.5f;
    public float enemyAttackTimerDelay;
    private bool isAttacking = false;

    public GameObject enemySword;

    Animator enemyAnimations;

    Transform target;
    // Start is called before the first frame update
    void Start()
    {
        currentEnemyHealth = maxEnemyHealth;

        currentEnemyMoveSpeed = enemyMoveSpeed;

        target = GameObject.Find("Player/enemyTarget").transform;

        enemyAnimations = this.gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (currentEnemyHealth <= 0)
        {
            //GetComponent<Animator>().enabled = false;
            Destroy(gameObject, 3);
        }

        transform.LookAt(target.position);
        transform.position += transform.forward * currentEnemyMoveSpeed * Time.deltaTime;

        if (enemyAttackTimerDelay <= 0)
        {
            enemyAttackTimerDelay = 0;
        }

        if (enemyAttackTimerDelay > 0)
        {
            enemyAttackTimerDelay -= Time.deltaTime;
        }

        distanceFromPlayer = Vector3.Distance(target.position, transform.position);

        if (distanceFromPlayer <= rangeToAttack)
        {
            currentEnemyMoveSpeed = 0;

            if (enemyAttackTimerDelay == 0)
            {
                enemyAnimations.SetBool("isWalking", false);
                enemyAnimations.SetBool("isAttacking", true);
                StartCoroutine(enableWeapon());
                enemyAttackTimerDelay = enemyAttackTimer;
            }
        }
        else
        {
            enemyAnimations.SetBool("isAttacking", false);
            enemyAnimations.SetBool("isWalking", true);

            if (isAttacking == false)
            {
                currentEnemyMoveSpeed = enemyMoveSpeed;
            }
        }    
    }

    void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "Axe") //GET THE TIMERS RIGHT FOR ATTACKING ENEMY
        {
            // Debug.Log(currentEnemyHealth);
            StartCoroutine(Wait());
            currentEnemyHealth -= GameObject.Find("Player").GetComponent<playerAttack>().playerDamageValue;
        }
    }

    private IEnumerator Wait()
    {
        enemyAnimations.SetBool("takenDamage", true);
        yield return new WaitForSeconds(1.2f);
        enemyAnimations.SetBool("takenDamage", false);
    }

    private IEnumerator enableWeapon()
    {
        enemySword.GetComponent<Collider>().enabled = false;
        enemySword.GetComponent<Collider>().enabled = true;
        isAttacking = true;
        yield return new WaitForSeconds(0.8f);
        isAttacking = false;
        enemySword.GetComponent<Collider>().enabled = false;
    }
}