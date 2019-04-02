using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bruteComboLogic : MonoBehaviour
{
    //Jump Attack Posisition
    public GameObject cubeLoc;
    public GameObject mycube;

    public GameObject bruteBody;

    public Animator bruteAnimations;

    public float bruteMoveSpeed = 2;
    public float currentBruteMoveSpeed;

    public Transform playerLocation;
    public float distanceFromPlayer;

    public float rangeToAttack = 1.8f;

    public float jumpAttackTimerValue = 25.0f;
    public float jumpAttackTimer;
    public bool canJumpAttack = false;

    public enum bruteLogicState
    {
        NOSTATE,
        Running,
        Attacking,
        JumpAttack
    }

    public bruteLogicState currentBruteLogicState;

    void Start()
    {
        currentBruteMoveSpeed = bruteMoveSpeed;
        jumpAttackTimer = 0;
    }

    void Update()
    {
        distanceFromPlayer = Vector3.Distance(playerLocation.position, transform.position);
        
        if (jumpAttackTimer <= 0)
        {
            jumpAttackTimer = 0;
        }

        if (jumpAttackTimer > 0)
        {
            jumpAttackTimer -= Time.deltaTime;
        }

        if(jumpAttackTimer == 0)
        {
            canJumpAttack = true;
        }

        switch (currentBruteLogicState)
        {
            case bruteLogicState.Running: 
                transform.LookAt(playerLocation.position);
                currentBruteMoveSpeed = bruteMoveSpeed;
                transform.position += transform.forward * currentBruteMoveSpeed * Time.deltaTime;
                bruteAnimations.SetBool("isRunning", true);
                bruteAnimations.SetBool("isWalking", false);
                bruteAnimations.SetBool("isTaunting", false);

                if (distanceFromPlayer < 6 && canJumpAttack == true)
                {
                    currentBruteLogicState = bruteLogicState.JumpAttack;
                }

                if (distanceFromPlayer < rangeToAttack)
                {
                    currentBruteLogicState = bruteLogicState.Attacking;
                }

                break;

            case bruteLogicState.Attacking:
                bruteAnimations.SetBool("isJumpAttack", false);
                bruteAnimations.SetBool("isRunning", false);
                bruteAnimations.SetBool("isWalking", true);
                
                transform.position += transform.forward * currentBruteMoveSpeed * Time.deltaTime;
                transform.LookAt(playerLocation.position);

                if (distanceFromPlayer > 6)
                {
                    currentBruteLogicState = bruteLogicState.Running;
                }

                if (distanceFromPlayer < rangeToAttack)
                {
                    currentBruteMoveSpeed = 0;
                    bruteAnimations.SetBool("isWalking", false);
                    bruteAnimations.SetBool("isComboAttack", true);
                }
                else
                {
                    currentBruteMoveSpeed = bruteMoveSpeed / 2;
                }

                break;

            case bruteLogicState.JumpAttack:
                bruteAnimations.SetBool("isJumpAttack", true);
                bruteAnimations.SetBool("isRunning", false);
                canJumpAttack = false;
                jumpAttackTimer = jumpAttackTimerValue;
                break;
        }
    }

    void startRunningState() //Taunt Animation
    {
        currentBruteLogicState = bruteLogicState.Running;
    }

    void spawnCube()
    {
        Instantiate(mycube, cubeLoc.transform.position, Quaternion.identity);
    }

    void updatePosistionNew()
    {
        // StartCoroutine(WaitFive());
        transform.position = GameObject.Find("jumpAttackLocationPrefab(Clone)").transform.position;
        Destroy(GameObject.Find("jumpAttackLocationPrefab(Clone)"));
        
        currentBruteMoveSpeed = 0;
    }

    void reenableBody()
    {
        bruteBody.SetActive(true);
        currentBruteMoveSpeed = bruteMoveSpeed / 2;
    }

    void updatePosistion()
    {
        currentBruteLogicState = bruteLogicState.Attacking;
        bruteBody.SetActive(false);
    }

   /* IEnumerator WaitFive()
    {
        yield return new WaitForSeconds(5.0f);
        transform.position = GameObject.Find("jumpAttackLocationPrefab(Clone)").transform.position;
        Destroy(GameObject.Find("jumpAttackLocationPrefab(Clone)"));
        
    }*/
}
