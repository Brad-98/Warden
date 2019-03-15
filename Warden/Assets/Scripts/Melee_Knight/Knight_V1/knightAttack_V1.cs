using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class knightAttack_V1 : MonoBehaviour
{
    public float knightAttackTimer = 1.5f;
    public float knightAttackTimerDelay;
    private bool isAttacking = false;

    public float rangeToAttack = 1.8f;
    private float distanceFromPlayer;

    public int knightDamageValue = 10;

    public GameObject knightSword;

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

            if (knightAttackTimerDelay == 0)
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
    }

    private IEnumerator enableWeapon()
    {
        knightSword.GetComponent<Collider>().enabled = true;
        isAttacking = true;
        yield return new WaitForSeconds(0.8f);
        isAttacking = false;
    }
}
