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

    public Vector3 lookAtRockAngle;

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
        if(GameObject.Find("Rock(Clone)") && GameObject.Find("Rock(Clone)").GetComponent<rockController>().rockTouchingGround == true)
        {
            distanceFromRock = Vector3.Distance(GameObject.Find("Rock(Clone)").transform.position, transform.position);
        }
        else
        {
            distanceFromRock = 0;
        }
        
        switch (currentKnightLogicState)
        {
            case knightLogicState.Roaming:

                FOVEnemyAnimations.SetBool("isWalking", true);
                FOVEnemyAnimations.SetBool("isIdle", false);
                FOVEnemyAnimations.SetBool("isRunning", false);

                transform.LookAt(roamLocation.position);
                transform.position = Vector3.MoveTowards(transform.position, roamLocation.position, soundEnemySpeed * Time.deltaTime);

                if(GameObject.Find("Rock(Clone)") && GameObject.Find("Rock(Clone)").GetComponent<rockController>().rockTouchingGround == true)
                {
                    lookAtRockAngle = new Vector3(Random.Range(-distanceFromRock, distanceFromRock) * 1.5f, 0, 0);
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

                FOVEnemyAnimations.SetBool("isIdle", true);
                FOVEnemyAnimations.SetBool("isWalking", false);
                FOVEnemyAnimations.SetBool("isRunning", false);

                if (GameObject.Find("Rock(Clone)").GetComponent<rockController>().rockTouchingGround == true)
                {
                    transform.LookAt(GameObject.Find("Rock(Clone)").transform.position + lookAtRockAngle);
                }

                if(lookAtRockAngle.x <= 6 && lookAtRockAngle.x >= -6)
                {
                    currentKnightLogicState = knightLogicState.Investigate;
                }

                if (hasSpawnedAlertBox == false)
                {
                    Instantiate(alertBox, new Vector3(transform.position.x, transform.position.y + 2.7f, transform.position.z), Quaternion.identity);
                    hasSpawnedAlertBox = true;
                }

                if (alertTimer <= 0)
                {
                    currentKnightLogicState = knightLogicState.Roaming;
                    Destroy(GameObject.Find("alertBox(Clone)"));
                    alertTimer = alertTimerValue;
                    hasSpawnedAlertBox = false;
                    GameObject.Find("Rock(Clone)").GetComponent<rockController>().rockTouchingGround = false;
                }
                else
                {
                    alertTimer -= Time.deltaTime;
                }

                break;

            case knightLogicState.Investigate:

                Destroy(GameObject.Find("alertBox(Clone)"));

                FOVEnemyAnimations.SetBool("isRunning", true);
                FOVEnemyAnimations.SetBool("isWalking", false);
                FOVEnemyAnimations.SetBool("isIdle", false);

                if(distanceFromRock <= 1)
                {
                    soundEnemySpeed = 0;
                    FOVEnemyAnimations.SetBool("isIdle", true);
                    FOVEnemyAnimations.SetBool("isRunning", false);
                }
                
                transform.LookAt(GameObject.Find("Rock(Clone)").transform.position);
                transform.position = Vector3.MoveTowards(transform.position, GameObject.Find("Rock(Clone)").transform.position, (soundEnemySpeed * 3) * Time.deltaTime);

                break;  
        }
    }
}
