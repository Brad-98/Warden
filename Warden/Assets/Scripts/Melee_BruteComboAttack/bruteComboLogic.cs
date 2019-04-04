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

    public float comboAttackTimerValue = 12.0f;
    public float comboAttackTimer;

    public bool canJumpAttack = false;
    public bool canComboAttack = false;
    public bool canComboAttackHealthCheck = false;

    public int howManyNormalAttacks = 0;
    public bool canDownAttack = false;

    public float bruteDamageValue = 0;

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
        comboAttackTimer = comboAttackTimerValue;
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

        if (canComboAttackHealthCheck == true)
        {
            if (comboAttackTimer <= 0)
            {
                comboAttackTimer = 0;
            }

            if (comboAttackTimer > 0)
            {
                comboAttackTimer -= Time.deltaTime;
            }

            if (comboAttackTimer == 0)
            {
                canComboAttack = true;
            }
        }

        if(howManyNormalAttacks >= 3)
        {
            canDownAttack = true;
        }

        if (this.gameObject.GetComponent<bruteComboController>().currentBruteHealth < 3)
        {
            canComboAttackHealthCheck = true;
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
                bruteAnimations.SetBool("isAttacking", false);
                bruteAnimations.SetBool("isComboAttack", false);

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
                
                transform.position += transform.forward * currentBruteMoveSpeed * Time.deltaTime;
                transform.LookAt(playerLocation.position);

                if (distanceFromPlayer > 6)
                {
                    currentBruteLogicState = bruteLogicState.Running;
                }

                if ((distanceFromPlayer < rangeToAttack) && (canComboAttack == false) && (canDownAttack == false))
                {
                    currentBruteMoveSpeed = 0;
                    bruteAnimations.SetBool("isWalking", false);
                    bruteAnimations.SetBool("isAttacking", true);
                }
                else if((distanceFromPlayer < rangeToAttack) && (canComboAttack == true) && (canDownAttack == false))
                {
                    currentBruteMoveSpeed = 0;
                    canComboAttack = false;
                    comboAttackTimer = comboAttackTimerValue;
                    bruteAnimations.SetBool("isWalking", false);
                    bruteAnimations.SetBool("isAttacking", false);
                    bruteAnimations.SetBool("isComboAttack", true);
                }
                else if((distanceFromPlayer < rangeToAttack) && (canComboAttack == false) && (canDownAttack == true))
                {
                    currentBruteMoveSpeed = 0;
                    howManyNormalAttacks = 0;
                    canDownAttack = false;
                    bruteAnimations.SetBool("isWalking", false);
                    bruteAnimations.SetBool("isAttacking", false);
                    bruteAnimations.SetBool("isComboAttack", false);
                    bruteAnimations.SetBool("isDownAttack", true);
                }
                else
                {
                    bruteAnimations.SetBool("isWalking", true);
                    bruteAnimations.SetBool("isAttacking", false);
                    bruteAnimations.SetBool("isComboAttack", false);
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

    void howManyNormalAttacksAnimation()
    {
        howManyNormalAttacks++;
    }

    void setComboAttackAnimationToFalse()
    {
        bruteAnimations.SetBool("isComboAttack", false);
    }

    void setDownAttackAnimationToFalse()
    {
        bruteAnimations.SetBool("isDownAttack", false);
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
