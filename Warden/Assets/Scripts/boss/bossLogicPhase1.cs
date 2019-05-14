using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bossLogicPhase1 : MonoBehaviour
{
    public float bossMoveSpeed = 5.0f;
    public float currentBossMoveSpeed;

    public Animator bossAnimations;

    public float rangeToAttack = 1.8f;
    private float distanceFromPlayer;
    public Transform playerLocation;

    public float bossDamageValue = 5.0f;

    public bool isAttacking;
    public GameObject bossHandCollider_Phase1;

    public bool awake = false;

    void Start()
    {
        currentBossMoveSpeed = bossMoveSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        distanceFromPlayer = Vector3.Distance(playerLocation.position, transform.position);

        if (awake == true)
        {
            transform.LookAt(playerLocation.position);
            transform.position += transform.forward * currentBossMoveSpeed * Time.deltaTime;

            if (distanceFromPlayer <= rangeToAttack)
            {
                bossAnimations.SetBool("isAttacking", true);
                isAttacking = true;
            }

            if (isAttacking == false)
            {
                currentBossMoveSpeed = bossMoveSpeed;
            }
        }
    }

    void sitToStand()
    {
        awake = true;
    }

    void startAttack()
    {
        bossHandCollider_Phase1.GetComponent<Collider>().enabled = true;
    }

    void attackStopMoving()
    {
        currentBossMoveSpeed = 0;
    }

    void stopAttack()
    {
        bossAnimations.SetBool("isAttacking", false);
        isAttacking = false;
    }
}
