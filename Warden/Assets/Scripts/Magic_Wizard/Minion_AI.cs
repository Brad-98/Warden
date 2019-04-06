using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minion_AI : MonoBehaviour
{
    public float enemyMoveSpeed;
    public float chosenSpeed;

    public Animator minionAnimations;

    public int maxEnemyHealth = 1;
    private int currentEnemyHealth;

    public float rangeToAttack = 1.8f;
    private float distanceFromPlayer;

    public float minionDamageValue = 1;

    public bool isAttacking;

    public Collider minionCollider;
    public GameObject minionAttackCollider;
    Transform target;
    
    void Start()
    {
        enemyMoveSpeed = Random.Range(1, 6);
        chosenSpeed = enemyMoveSpeed;

        currentEnemyHealth = maxEnemyHealth;

        target = GameObject.Find("Player/enemyTarget").transform;
    }

    void Update()
    {
        distanceFromPlayer = Vector3.Distance(target.position, transform.position);

        if (currentEnemyHealth <= 0)
        {
            minionAnimations.SetBool("isDead", true);
            minionAttackCollider.GetComponent<Collider>().enabled = false;
            this.gameObject.GetComponent<Collider>().enabled = false;
            this.gameObject.GetComponent<Rigidbody>().isKinematic = true;
            minionCollider.enabled = false;
            this.gameObject.GetComponent<Minion_AI>().enabled = false;
        }

        if(enemyMoveSpeed > 2)
        {
            minionAnimations.SetBool("isRunning", true);
        }

        transform.LookAt(target.position);
        transform.position += transform.forward * chosenSpeed * Time.deltaTime;

        if (distanceFromPlayer <= rangeToAttack)
        {  
            minionAnimations.SetBool("isAttacking", true);
            isAttacking = true;
        }

        if(isAttacking == false)
        {
            chosenSpeed = enemyMoveSpeed;
        }
    }

    void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "playerAxe") //GET THE TIMERS RIGHT FOR ATTACKING ENEMY
        {
            //StartCoroutine(Wait());
            currentEnemyHealth -= GameObject.Find("Player").GetComponent<playerAttack>().playerDamageValue;
        }
    }

    void stopAttackAnimation()
    {
        isAttacking = false;
        minionAnimations.SetBool("isAttacking", false);
    }

    void enableAttackCollider()
    {
        minionAttackCollider.GetComponent<Collider>().enabled = true;
        chosenSpeed = 0;
    }
}
