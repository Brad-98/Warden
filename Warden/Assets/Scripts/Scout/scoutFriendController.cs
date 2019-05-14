using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scoutFriendController : MonoBehaviour
{
    private Animator scoutFriendAnimations;

    public float scoutFriendSpeed = 4.0f;

    public int maxScoutFriendHealth = 1;
    private int currentScoutFriendHealth;

    public float scoutFriendDamageValue = 10.0f;

    public float rangeToAttack = 1.8f;
    private float distanceFromPlayer;

    public Transform playerLocation;
    public GameObject scoutFriendSword;

    public bool isAttacking;

    public enum scoutFriendLogicState
    {
        NOSTATE,
        Attack
    }

    public scoutFriendLogicState currentScoutFriendLogicState;

    void Start()
    {
        currentScoutFriendHealth = maxScoutFriendHealth;
        scoutFriendAnimations = this.gameObject.GetComponent<Animator>();
        currentScoutFriendLogicState = scoutFriendLogicState.NOSTATE;
    }

    void Update()
    {
        if (currentScoutFriendHealth <= 0)
        {
            scoutFriendAnimations.SetBool("isDead", true);
            scoutFriendSword.GetComponent<Collider>().enabled = false;
            this.gameObject.GetComponent<Collider>().enabled = false;
            this.gameObject.GetComponent<scoutFriendController>().enabled = false;
        }

        distanceFromPlayer = Vector3.Distance(playerLocation.position, transform.position);

        switch (currentScoutFriendLogicState)
        {
            case scoutFriendLogicState.Attack:

                scoutFriendAnimations.SetBool("isIdle", false);
                scoutFriendAnimations.SetBool("isRunning", true);

                transform.LookAt(playerLocation.position);
                transform.position = Vector3.MoveTowards(transform.position, playerLocation.position, scoutFriendSpeed * Time.deltaTime);

                if (distanceFromPlayer <= rangeToAttack)
                {
                    scoutFriendAnimations.SetBool("attack", true);
                    isAttacking = true;
                }

                if (isAttacking == false)
                {
                    scoutFriendSpeed = 4.0f;
                }

                break;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "scout")
        {
            currentScoutFriendLogicState = scoutFriendLogicState.Attack;
            gameObject.tag = "scoutFriend";
        }

        if (other.gameObject.tag == "playerAxe") //GET THE TIMERS RIGHT FOR ATTACKING ENEMY
        {
            //StartCoroutine(Wait());
            currentScoutFriendHealth -= GameObject.Find("Player").GetComponent<playerAttack>().playerDamageValue;
        }
    }

    void stopScoutFriendAttackAnimation()
    {
        isAttacking = false;
        scoutFriendAnimations.SetBool("attack", false);
    }

    void enableScoutFriendAttackCollider()
    {
        scoutFriendSword.GetComponent<Collider>().enabled = true;

        scoutFriendSpeed = 0;
    }
}
