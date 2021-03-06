﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FOVEnemyLogic : MonoBehaviour
{
    private Animator FOVEnemyAnimations;

    public float FOVEnemySpeed = 2.0f;

    //Roaming Varibles

    public float roamTimerValue = 3.0f;
    public float roamTimer;

    public float minX;
    public float maxX;
    public float minZ;
    public float maxZ;

    public bool lookingAtWall = false;

    public Transform roamLocation;

    //Alert Varibles

    public float alertTimerValue = 5.0f;
    public float alertTimer;

    public float enemyFOV = 40.0f;

    private Vector3 playerDirectionFromEnemy;
    private float distanceFromPlayer;
    private float angleBetweenPlayerAndEnemy;
    public Transform playerLocation;

    private bool hasSpawnedAlertBox = false;

    public GameObject alertBox;

    //Attacking Varibles

    public float enemyAttackTimerValue = 1.5f;
    public float enemyAttackTimer;
    private bool canAttack = false;

    public float rangeToAttack = 1.8f;

    //public int knightDamageValue = 10;

    //public GameObject knightSword;

    public enum FOVEnemyLogicState
    {
        Roaming,
        Alert,
        Attacking
    }

    public FOVEnemyLogicState currentFOVEnemyLogicState;

    void Start()
    {
        FOVEnemyAnimations = this.gameObject.GetComponent<Animator>();

        currentFOVEnemyLogicState = FOVEnemyLogicState.Roaming;

        roamTimer = roamTimerValue;
        alertTimer = alertTimerValue;
        enemyAttackTimer = enemyAttackTimerValue;

        roamLocation.position = new Vector3(Random.Range(minX, maxX), 0, Random.Range(minZ, maxZ));
}

    // Update is called once per frame
    void Update()
    {
        GameObject.Find("FOVKnight/RaycastLocation").transform.LookAt(playerLocation.transform);

        playerDirectionFromEnemy = playerLocation.transform.position - transform.position;
        angleBetweenPlayerAndEnemy = Vector3.Angle(playerDirectionFromEnemy, transform.forward);

        distanceFromPlayer = Vector3.Distance(playerLocation.position, transform.position);

        switch (currentFOVEnemyLogicState)
        {
            case FOVEnemyLogicState.Roaming:

                FOVEnemyAnimations.SetBool("isWalking", true);
                FOVEnemyAnimations.SetBool("isIdle", false);
                FOVEnemyAnimations.SetBool("isAttacking", false);

                transform.LookAt(roamLocation.position);
                transform.position = Vector3.MoveTowards(transform.position, roamLocation.position, FOVEnemySpeed * Time.deltaTime);
                
                RaycastHit hitWallWhileRoaming;
               
                if (Physics.Raycast(GameObject.Find("FOVKnight/RaycastLocation").transform.position, GameObject.Find("FOVKnight/RaycastLocation").transform.forward, out hitWallWhileRoaming))
                {
                    if (hitWallWhileRoaming.collider.gameObject.tag == "Wall")
                    {
                        lookingAtWall = true;
                    }
                    else
                    {
                        lookingAtWall = false;
                    }
                }
                
                if ((angleBetweenPlayerAndEnemy < enemyFOV) && (distanceFromPlayer <= 20) && lookingAtWall == false)
                {
                        currentFOVEnemyLogicState = FOVEnemyLogicState.Alert;
                }

                if (Vector3.Distance(transform.position, roamLocation.position) < 0.2f)
                {
                    if(roamTimer <= 0)
                    {
                        roamLocation.position = new Vector3(Random.Range(minX, maxX), 0, Random.Range(minZ, maxZ));
                        roamTimer = roamTimerValue;
                    }
                    else
                    {
                        FOVEnemyAnimations.SetBool("isWalking", false);
                        FOVEnemyAnimations.SetBool("isIdle", true);
                        roamTimer -= Time.deltaTime;
                    }
                }
                break;

            case FOVEnemyLogicState.Alert:

                FOVEnemyAnimations.SetBool("isIdle", true);
                FOVEnemyAnimations.SetBool("isWalking", false);
                FOVEnemyAnimations.SetBool("isAttacking", false);

                RaycastHit hitWallWhileAlert;

                if (Physics.Raycast(GameObject.Find("FOVKnight/RaycastLocation").transform.position, GameObject.Find("FOVKnight/RaycastLocation").transform.forward, out hitWallWhileAlert))
                {
                    if (hitWallWhileAlert.collider.gameObject.tag == "Wall")
                    {
                        lookingAtWall = true;
                    }
                    else
                    {
                        lookingAtWall = false;
                    }
                }

                if (hasSpawnedAlertBox == false)
                {
                    Instantiate(alertBox, new Vector3(transform.position.x, transform.position.y + 2.7f, transform.position.z), Quaternion.identity);
                    hasSpawnedAlertBox = true;
                }

                if((angleBetweenPlayerAndEnemy < enemyFOV) && (distanceFromPlayer <= 10) && lookingAtWall == false)
                {
                    currentFOVEnemyLogicState = FOVEnemyLogicState.Attacking;
                    Destroy(GameObject.Find("alertBox(Clone)"));
                    hasSpawnedAlertBox = false;
                }

                if(alertTimer <= 0)
                {
                    currentFOVEnemyLogicState = FOVEnemyLogicState.Roaming;
                    Destroy(GameObject.Find("alertBox(Clone)"));
                    alertTimer = alertTimerValue;
                    hasSpawnedAlertBox = false;
                }
                else
                {
                    alertTimer -= Time.deltaTime;
                }
                break;

            case FOVEnemyLogicState.Attacking:

                FOVEnemyAnimations.SetBool("isIdle", false);
                FOVEnemyAnimations.SetBool("isWalking", true);

                transform.LookAt(playerLocation.position);
                transform.position = Vector3.MoveTowards(transform.position, playerLocation.position, FOVEnemySpeed * Time.deltaTime);

                RaycastHit hitWallWhileAttacking;

                if (Physics.Raycast(GameObject.Find("FOVKnight/RaycastLocation").transform.position, GameObject.Find("FOVKnight/RaycastLocation").transform.forward, out hitWallWhileAttacking))
                {
                    if (hitWallWhileAttacking.collider.gameObject.tag == "Wall")
                    {
                        alertTimer = 3;
                        roamTimer = 0;
                        currentFOVEnemyLogicState = FOVEnemyLogicState.Alert;
                        Destroy(GameObject.Find("alertBox(Clone)"));
                    }
                }

                if (enemyAttackTimer <= 0)
                {
                    canAttack = true;
                }
                else
                {
                    enemyAttackTimer -= Time.deltaTime;
                }

                if (distanceFromPlayer <= rangeToAttack)
                {
                    FOVEnemySpeed = 0;

                    if (canAttack == true)
                    {
                        FOVEnemyAnimations.SetBool("isWalking", false);
                        FOVEnemyAnimations.SetBool("isAttacking", true);
                        //StartCoroutine(enableWeapon());
                        canAttack = false;
                        enemyAttackTimer = enemyAttackTimerValue;
                    }
                }
                else
                {
                    FOVEnemyAnimations.SetBool("isWalking", true); // CHANGE THIS BACK TO ALERT BECAUSE TO STOP ATTACKING THE PLAYER WILL HAVE TO HIDE BEHIND A WALL
                    FOVEnemyAnimations.SetBool("isAttacking", false);

                    if (canAttack == false)
                    {
                        FOVEnemySpeed = 2.0f;
                    }
                }
                break;
        }
    }
}