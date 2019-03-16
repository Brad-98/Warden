using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class knightAttack_V2 : MonoBehaviour
{
    public float knightAttackTimer = 1.5f;
    public float knightAttackTimerDelay;
    private bool isAttacking = false;

    public float rangeToAttack = 1.8f;
    private float distanceFromPlayer;

    public int knightDamageValue = 10;
    public int knightFootDamageValue = 20;

    public GameObject knightSword;
    public GameObject knightFoot;

    public float knightBlockTimer;
    private float currentKnightBlockTimer;
  
    public bool isBlocking = false;

    public GameObject playerAxe;
    public GameObject playerAxeBlocking;

    void Start()
    {
        knightBlockTimer = Random.Range(4, 8);

        currentKnightBlockTimer = knightBlockTimer;    
    }

    void Update()
    {
        distanceFromPlayer = Vector3.Distance(this.gameObject.GetComponent<knightController>().playerLocation.position, transform.position);

        if (knightAttackTimerDelay <= 0)
        {
            knightAttackTimerDelay = 0;
        }

        if (knightAttackTimerDelay > 0)
        {
            knightAttackTimerDelay -= Time.deltaTime;
        }

        if (distanceFromPlayer <= rangeToAttack)
        {
            this.gameObject.GetComponent<knightController>().currentKnightMoveSpeed = 0;

            if (knightAttackTimerDelay == 0 && isBlocking == false)
            {
                this.gameObject.GetComponent<knightController>().knightAnimations.SetBool("isWalking", false);
                this.gameObject.GetComponent<knightController>().knightAnimations.SetBool("isAttacking", true);
                StartCoroutine(enableWeapon());
                knightAttackTimerDelay = knightAttackTimer;
            }
        }
        else
        {
            this.gameObject.GetComponent<knightController>().knightAnimations.SetBool("isAttacking", false);
            this.gameObject.GetComponent<knightController>().knightAnimations.SetBool("isWalking", true);

            if (isAttacking == false)
            {
                this.gameObject.GetComponent<knightController>().currentKnightMoveSpeed = this.gameObject.GetComponent<knightController>().knightMoveSpeed;
            }
        }

        //Blocking

        currentKnightBlockTimer -= Time.deltaTime;

        if (currentKnightBlockTimer <= 0)
        {
            isBlocking = true;
        }

        if (isBlocking == true)
        { 
            playerAxe.GetComponent<Collider>().enabled = false;
            GameObject.Find("Player").GetComponent<playerAttack>().isKnight_V2Blocking = true;
            //playerAxeBlocking.GetComponent<Collider>().enabled = true;

            this.gameObject.GetComponent<knightController>().knightAnimations.SetBool("isBlocking", true);
            this.gameObject.GetComponent<knightController>().knightAnimations.SetBool("isWalking", false);
            this.gameObject.GetComponent<knightController>().currentKnightMoveSpeed = 0;

            StartCoroutine(blockingTimer());
        }
        else
        {
            // playerAxe.GetComponent<Collider>().enabled = true;
            playerAxeBlocking.GetComponent<Collider>().enabled = false;
            GameObject.Find("Player").GetComponent<playerAttack>().isKnight_V2Blocking = false;
            this.gameObject.GetComponent<knightController>().knightAnimations.SetBool("isBlocking", false);
            //this.gameObject.GetComponent<knightController>().currentKnightMoveSpeed = this.gameObject.GetComponent<knightController>().knightMoveSpeed;
        }
    }

    private IEnumerator enableWeapon() //Maybe use animation event
    {
        knightSword.GetComponent<Collider>().enabled = true;
        isAttacking = true;
        yield return new WaitForSeconds(0.8f);
        isAttacking = false; 
    }

    private IEnumerator blockingTimer()
    {
        yield return new WaitForSeconds(8.0f);
        isBlocking = false;
        currentKnightBlockTimer = knightBlockTimer;
    }

    void stopKickAnimation()
    {
        this.gameObject.GetComponent<knightController>().knightAnimations.SetBool("isKicking", false);
    }
}