using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wizard_V1 : MonoBehaviour
{
    public int maxEnemyHealth = 20;
    private int currentEnemyHealth;

    public float enemyMoveSpeed = 2;
    private float currentEnemyMoveSpeed;

    public int enemyDamageValue = 1;

    public float rangeToAttack = 1.8f;
    private float distanceFromPlayer;

    public float enemyAttackTimer = 1.5f;
    public float enemyAttackTimerDelay;

    Animator enemyAnimations;

    Transform target;

    public Transform spellLocation;

    public GameObject fireballPrefab;
    // Start is called before the first frame update
    void Start()
    {
        currentEnemyHealth = maxEnemyHealth;

        currentEnemyMoveSpeed = enemyMoveSpeed;

        enemyAttackTimerDelay = enemyAttackTimer;

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

        if (enemyAttackTimerDelay <= 0)
        {
            enemyAttackTimerDelay = 0;
        }

        if (enemyAttackTimerDelay > 0)
        {
            enemyAttackTimerDelay -= Time.deltaTime;
        }

        if (enemyAttackTimerDelay == 0)
        {
            //enemyAnimations.SetBool("isWalking", false);
            //enemyAnimations.SetBool("isAttacking", true);
            Instantiate(fireballPrefab, spellLocation.position, spellLocation.rotation);
            enemyAttackTimerDelay = enemyAttackTimer;
        }

        distanceFromPlayer = Vector3.Distance(target.position, transform.position);

        if (distanceFromPlayer <= 15)
        {
            transform.position -= transform.forward * currentEnemyMoveSpeed * Time.deltaTime;

            
        }
        else
        {
            // enemyAnimations.SetBool("isAttacking", false);
            //  enemyAnimations.SetBool("isWalking", true);

            //currentEnemyMoveSpeed = 0;
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
}