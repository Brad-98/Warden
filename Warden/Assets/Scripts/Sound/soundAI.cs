using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class soundAI : MonoBehaviour
{
    private Animator FOVEnemyAnimations;

    public float soundEnemySpeed = 2.0f;

    //Roaming Varibles

    public float roamTimerValue = 3.0f;
    public float roamTimer;

    public float minX;
    public float maxX;
    public float minZ;
    public float maxZ;

    public Transform roamLocation;

    //Alert Varibles

    public float alertTimerValue = 5.0f;
    public float alertTimer;

    private float distanceFromRock;
    public Transform rockLocation;
    public bool rockHitGround = false;

    private bool hasSpawnedAlertBox = false;

    public Quaternion lookAtRockAngle;

    public GameObject alertBox;

    public enum knightLogicState
    {
        Roaming,
        Alert,
        Investigate
    }

    public knightLogicState currentKnightLogicState;

    void Start()
    {
        roamTimer = roamTimerValue;
        alertTimer = alertTimerValue;
        FOVEnemyAnimations = this.gameObject.GetComponent<Animator>();
        currentKnightLogicState = knightLogicState.Roaming;
        roamLocation.position = new Vector3(Random.Range(minX, maxX), 0, Random.Range(minZ, maxZ));
    }

    void Update()
    {
        if(GameObject.Find("Rock(Clone)"))
        {
            distanceFromRock = Vector3.Distance(GameObject.Find("Rock(Clone)").transform.position, transform.position);
        }

        switch (currentKnightLogicState)
        {
            case knightLogicState.Roaming:

                FOVEnemyAnimations.SetBool("isWalking", true);
                FOVEnemyAnimations.SetBool("isIdle", false);

                transform.LookAt(roamLocation.position);
                transform.position = Vector3.MoveTowards(transform.position, roamLocation.position, soundEnemySpeed * Time.deltaTime);

             /*   if ((angleBetweenPlayerAndEnemy < enemyFOV) && (distanceFromPlayer <= 20) && lookingAtWall == false)
                {
                    currentFOVEnemyLogicState = FOVEnemyLogicState.Alert;
                }*/

                if(rockHitGround == true)
                {
                    Quaternion chooseAngle = Quaternion.Euler(0, Random.Range(-distanceFromRock, distanceFromRock), 0);
                    lookAtRockAngle = chooseAngle;
                    currentKnightLogicState = knightLogicState.Alert;
                }
                
                if (Vector3.Distance(transform.position, roamLocation.position) < 0.2f)
                {
                    if (roamTimer <= 0)
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

            case knightLogicState.Alert:

                //  FOVEnemyAnimations.SetBool("isIdle", true);
                //  FOVEnemyAnimations.SetBool("isWalking", false);
                //  FOVEnemyAnimations.SetBool("isAttacking", false);
                rockHitGround = false;
                transform.LookAt(GameObject.Find("rock(Clone)").transform.position);
                //transform.rotation = lookAtRockAngle;
                if (hasSpawnedAlertBox == false)
                {
                    Instantiate(alertBox, new Vector3(transform.position.x, transform.position.y + 2.7f, transform.position.z), Quaternion.identity);
                    hasSpawnedAlertBox = true;
                }

                /*if ((angleBetweenPlayerAndEnemy < enemyFOV) && (distanceFromPlayer <= 10) && lookingAtWall == false)
                {
                    currentFOVEnemyLogicState = FOVEnemyLogicState.Attacking;
                    Destroy(GameObject.Find("alertBox(Clone)"));
                    hasSpawnedAlertBox = false;
                }

                if (alertTimer <= 0)
                {
                    currentFOVEnemyLogicState = FOVEnemyLogicState.Roaming;
                    Destroy(GameObject.Find("alertBox(Clone)"));
                    alertTimer = alertTimerValue;
                    hasSpawnedAlertBox = false;
                }
                else
                {
                    alertTimer -= Time.deltaTime;
                }*/
                break;

            case knightLogicState.Investigate:
                break;  
        }
    }
}
