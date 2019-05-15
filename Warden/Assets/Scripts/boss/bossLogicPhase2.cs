using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bossLogicPhase2 : MonoBehaviour
{
    public float bossMoveSpeed = 5.0f;
    public float currentBossMoveSpeed;

    public Animator bossAnimations;

    public float rangeToAttack = 5f;
    private float distanceFromPlayer;
    public Transform playerLocation;

    public float bossDamageValue = 12.0f;

    public bool isAttacking;
    public GameObject bossHandCollider_Phase1;

    public bool awake = false;

    public bool startPhase2 = false;

    public float stompAttackTimerValue = 7.0f;
    public float stompAttackTimer;
    public bool canStompAttack = false;

    //Scale
    Vector3 minScale;
    public Vector3 maxScale;
    public float speed = 2f;
    public float duration = 5f;

    void Start()
    {
        if (this.gameObject.GetComponent<bossController>().startPhase2 == true)
        { 
            currentBossMoveSpeed = bossMoveSpeed;
            this.gameObject.GetComponent<bossLogicPhase1>().enabled = false;
            StartCoroutine(startScale());
            stompAttackTimer = stompAttackTimerValue;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (this.gameObject.GetComponent<bossController>().startPhase2 == true)
        {
            distanceFromPlayer = Vector3.Distance(playerLocation.position, transform.position);

            if (stompAttackTimer <= 0)
            {
                stompAttackTimer = 0;
            }

            if (stompAttackTimer > 0)
            {
                stompAttackTimer -= Time.deltaTime;
            }

            if (stompAttackTimer == 0)
            {
                canStompAttack = true;
            }

            if (awake == true)
            {
                transform.LookAt(playerLocation.position);
                transform.position += transform.forward * currentBossMoveSpeed * Time.deltaTime;

                if (distanceFromPlayer <= rangeToAttack)
                {
                    if (canStompAttack == true)
                    {
                        bossAnimations.SetBool("isStomping", true);
                        stompAttackTimer = stompAttackTimerValue;
                        canStompAttack = false;
                    }
                    else
                    {
                        bossAnimations.SetBool("isAttacking", true);
                        isAttacking = true;
                    }
                }

                if (isAttacking == false)
                {
                    currentBossMoveSpeed = bossMoveSpeed;
                }
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

    void stopAttackStomp()
    {
        bossAnimations.SetBool("isStomping", false);
    }

    public IEnumerator startScale()
    {
        minScale = transform.localScale;
        currentBossMoveSpeed = 0;
        yield return Lerp(minScale, maxScale, duration);
    }

    public IEnumerator Lerp(Vector3 a, Vector3 b, float time)
    {
        float i = 0.0f;
        float rate = (1.0f / time) * speed;
        while(i < 1.0f)
        {
            i += Time.deltaTime * rate;
            transform.localScale = Vector3.Lerp(a, b, i);
            yield return null;
        }
    }
}
