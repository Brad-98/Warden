using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerAttack : MonoBehaviour
{
    Animator playerAnimations;

    public GameObject playerAxe;

    public int playerDamageValue = 1;

    public float attackTimer = 1.8f;
    public float attackTimerDelay;

	void Start ()
    {
        playerAnimations = this.gameObject.GetComponent<Animator>();
	}

	void Update ()
    {
       // Debug.Log(attackTimerDelay);

        if (attackTimerDelay <= 0)
        {
            attackTimerDelay = 0;
        }

        if (attackTimerDelay > 0)
        {
            attackTimerDelay -= Time.deltaTime;
        }

        if (Input.GetMouseButtonDown(0) && (attackTimerDelay == 0))
        {
            if (GameObject.FindWithTag("enemyKnight").GetComponent<knightAttack_V2>().isBlocking == false)
            {
                StartCoroutine(enableWeapon());
            }
            else
            {
                StartCoroutine(enableWeaponBlocking());
            }

            attackTimerDelay = attackTimer;
        }
        else
        {
            playerAnimations.SetInteger("isAttacking", 0);
        }
    }

    private IEnumerator enableWeapon()
    {
        playerAxe.GetComponent<Collider>().enabled = true;
        playerAnimations.SetInteger("isAttacking", Random.Range(1, 3));
        yield return new WaitForSeconds(0.8f);
        playerAxe.GetComponent<Collider>().enabled = false;
    }

    private IEnumerator enableWeaponBlocking()
    {
        GameObject.FindWithTag("enemyKnight").GetComponent<knightAttack_V2>().playerAxeBlocking.GetComponent<Collider>().enabled = true;
        playerAnimations.SetInteger("isAttacking", Random.Range(1, 3));
        yield return new WaitForSeconds(0.8f);
        GameObject.FindWithTag("enemyKnight").GetComponent<knightAttack_V2>().playerAxeBlocking.GetComponent<Collider>().enabled = false;
    }
}
