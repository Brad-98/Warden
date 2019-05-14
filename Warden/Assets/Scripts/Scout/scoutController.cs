using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scoutController : MonoBehaviour
{
    private Animator scoutAnimations;

    public float scoutSpeed = 2.0f;

    //Roaming Varibles

    public float roamTimerValue = 3.0f;
    public float roamTimer;

    public float minX;
    public float maxX;
    public float minZ;
    public float maxZ;

    public Transform roamLocation;

    public GameObject alertBox;

    private Vector3 playerDirectionFromEnemy;
    private float distanceFromPlayer;
    private float angleBetweenPlayerAndEnemy;
    public Transform playerLocation;

    public float scoutFOV = 40.0f;

    public enum scoutLogicState
    {
        Roaming,
        FindFriends
    }

    public scoutLogicState currentScoutLogicState;

    void Start()
    {
        roamTimer = roamTimerValue;
        scoutAnimations = this.gameObject.GetComponent<Animator>();
        currentScoutLogicState = scoutLogicState.Roaming;
        roamLocation.position = new Vector3(Random.Range(minX, maxX), 0, Random.Range(minZ, maxZ));
    }

    void Update()
    {
        playerDirectionFromEnemy = playerLocation.transform.position - transform.position;
        angleBetweenPlayerAndEnemy = Vector3.Angle(playerDirectionFromEnemy, transform.forward);

        distanceFromPlayer = Vector3.Distance(playerLocation.position, transform.position);

        switch (currentScoutLogicState)
        {
            case scoutLogicState.Roaming:

                scoutAnimations.SetBool("isWalking", true);
                scoutAnimations.SetBool("isIdle", false);
                scoutAnimations.SetBool("isRunning", false);

                transform.LookAt(roamLocation.position);
                transform.position = Vector3.MoveTowards(transform.position, roamLocation.position, scoutSpeed * Time.deltaTime);

                if ((angleBetweenPlayerAndEnemy < scoutFOV) && (distanceFromPlayer <= 20))
                {
                    currentScoutLogicState = scoutLogicState.FindFriends;
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
                        scoutAnimations.SetBool("isWalking", false);
                        scoutAnimations.SetBool("isIdle", true);
                        roamTimer -= Time.deltaTime;
                    }
                }
                break;

            case scoutLogicState.FindFriends:

                scoutAnimations.SetBool("isIdle", false);
                scoutAnimations.SetBool("isWalking", false);
                scoutAnimations.SetBool("isRunning", true);

                alertBox.SetActive(true);

                transform.LookAt(FindClosestEnemy().transform.position);
                transform.position = Vector3.MoveTowards(transform.position, FindClosestEnemy().transform.position, (scoutSpeed * 3) * Time.deltaTime);

                break;
        }
    }

    private GameObject FindClosestEnemy()
    {
        GameObject[] gos;
        gos = GameObject.FindGameObjectsWithTag("scoutEnemyFriend");
        GameObject closest = null;
        float distance = Mathf.Infinity;
        Vector3 position = transform.position;
        foreach (GameObject go in gos)
        {
            Vector3 diff = go.transform.position - position;
            float curDistance = diff.sqrMagnitude;
            if (curDistance < distance)
            {
                closest = go;
                distance = curDistance;
            }
        }
        return closest;
    }
}
