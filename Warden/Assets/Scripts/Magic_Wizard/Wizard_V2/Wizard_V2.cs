﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wizard_V2 : MonoBehaviour
{
    public int maxEnemyHealth = 20;
    private int currentEnemyHealth;

    public float enemyMoveSpeed = 5;
    private float currentEnemyMoveSpeed;

    public float rangeToAttack = 1.8f;
    private float distanceFromPlayer;

    public float runBackwardsTimer = 5.0f;
    private float currentRunBackwardsTimer;

    public float enemyAttackTimer = 5f;
    public float enemyAttackTimerDelay;
    public bool canAttack = false;

    Animator enemyAnimations;

    Transform target;

    public GameObject teleportLocation;

    public Transform spellLocation;

    public GameObject fireballPrefab;
    // Start is called before the first frame update
    void Start()
    {
        currentEnemyHealth = maxEnemyHealth;

        currentEnemyMoveSpeed = enemyMoveSpeed;

        enemyAttackTimerDelay = enemyAttackTimer;

        currentRunBackwardsTimer = runBackwardsTimer;

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

        if (GameObject.FindWithTag("teleportLocation") != null)
        {
            Teleport();
        }

        transform.LookAt(target.position);

        if (enemyAttackTimerDelay <= 0)
        {
            enemyAttackTimerDelay = 0;
        }

        if (currentRunBackwardsTimer <= 0)
        {
            currentRunBackwardsTimer = 0;
        }

        if (enemyAttackTimerDelay > 0)
        {
            enemyAttackTimerDelay -= Time.deltaTime;
        }

        if (enemyAttackTimerDelay == 0)
        {
            canAttack = true;
        }

        distanceFromPlayer = Vector3.Distance(target.position, transform.position);
        currentRunBackwardsTimer -= Time.deltaTime;
        //Debug.Log(currentRunBackwardsTimer);

        if (distanceFromPlayer <= 20 && currentRunBackwardsTimer <= 0)
        {
            transform.position -= transform.forward * currentEnemyMoveSpeed * Time.deltaTime;
            enemyAnimations.SetBool("isBackwardsRunning", true);
            enemyAnimations.SetBool("isCasting", false);
            canAttack = false;
            StartCoroutine(runBackwardsWait());
        }

        if (distanceFromPlayer >= 20 || currentRunBackwardsTimer > 0)
        {
            enemyAnimations.SetBool("isBackwardsRunning", false);
            enemyAnimations.SetBool("isCasting", true);
        }
    }

    void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "playerAxe") //GET THE TIMERS RIGHT FOR ATTACKING ENEMY
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

    private IEnumerator runBackwardsWait()
    {
        yield return new WaitForSeconds(5.0f);
        currentRunBackwardsTimer = runBackwardsTimer;
    }

    public void castFireball()
    {
        if (canAttack == true)
        {
            Instantiate(fireballPrefab, spellLocation.position, spellLocation.rotation);
            canAttack = false;
            enemyAttackTimerDelay = enemyAttackTimer;
        }
    }

    void Teleport()
    {
        // if (GameObject.FindWithTag("teleportLocation").GetComponent<getTeleportPosition>().teleportPositionDistanceFromPlayer <= 8.0f)
        // {
        //Debug.Log("In Range");

        //GameObject.Find("Teleport Field").GetComponent<Teleport>().currentTeleSpawnTimer = 0.1f;
        //Destroy(GameObject.FindWithTag("teleportLocation"));
        //}
        //else
        // {
        //Debug.Log("Not In Range");
        transform.position = GameObject.FindWithTag("teleportLocation").GetComponent<getTeleportPosition>().myPosition;
        //}
    }
}