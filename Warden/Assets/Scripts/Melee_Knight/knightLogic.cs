using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class knightLogic : MonoBehaviour
{
    public float knightMoveSpeed = 2;
    public float currentKnightMoveSpeed;

    public Transform playerLocation;
    public float distanceFromPlayer;

    public float rangeToAttack = 1.8f;

    public enum knightLogicState
    {
        Walking,
        Attacking,
        Blocking
    }

    public knightLogicState currentKnightLogicState;

    void Start()
    {
        currentKnightMoveSpeed = knightMoveSpeed;
        currentKnightLogicState = knightLogicState.Walking;
    }

    void Update()
    {
        distanceFromPlayer = Vector3.Distance(playerLocation.position, transform.position);
        transform.LookAt(playerLocation.position);
        if (distanceFromPlayer <= rangeToAttack)
        {
            currentKnightLogicState = knightLogicState.Attacking;
        }
        else
        {
            currentKnightLogicState = knightLogicState.Walking;
        }

        switch (currentKnightLogicState)
        {
            case knightLogicState.Walking:
                currentKnightMoveSpeed = knightMoveSpeed;
                transform.position += transform.forward * currentKnightMoveSpeed * Time.deltaTime;
                this.gameObject.GetComponent<knightController>().knightAnimations.SetBool("isWalking", true);
                this.gameObject.GetComponent<knightController>().knightAnimations.SetBool("isAttacking", false);
                break;

            case knightLogicState.Attacking:
                this.gameObject.GetComponent<knightController>().knightAnimations.SetBool("isAttacking", true);
                currentKnightMoveSpeed = 0;
                break;

            case knightLogicState.Blocking:
               
                break;
        }
    }
}
