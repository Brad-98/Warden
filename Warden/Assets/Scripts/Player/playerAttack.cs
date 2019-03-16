using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerAttack : MonoBehaviour
{
    public GameObject playerAxe;

    public int playerDamageValue = 1;

    public bool canPlayerAttack = false;

    public bool isKnight_V2Blocking = false;
   
    public float attackTimer = 1.8f;
    public float attackTimerDelay;

    void Start()
    {
        attackTimerDelay = 0;
    }

    void Update ()
    {  
        if (attackTimerDelay > 0)
        {
            attackTimerDelay -= Time.deltaTime;
        }
       
        if (attackTimerDelay <= 0)
        {
            attackTimerDelay = 0;
        }

        if(attackTimerDelay == 0)
        {
            canPlayerAttack = true;
        }
        
        if (canPlayerAttack == true)
        {
            // TODO: Fix player attacking, remember about isBlocking =true then use the other weapon collider for the kick
            if(Input.GetMouseButtonDown(0))
            {
                if (isKnight_V2Blocking == false)
                {
                    playerAxe.GetComponent<Collider>().enabled = true;
                    this.gameObject.GetComponent<playerController>().playerAnimations.SetBool("attack", true);

                    canPlayerAttack = false;
                    attackTimerDelay = attackTimer;
                }
                else
                {
                    GameObject.FindWithTag("enemyKnight").GetComponent<knightAttack_V2>().playerAxeBlocking.GetComponent<Collider>().enabled = true;
                    this.gameObject.GetComponent<playerController>().playerAnimations.SetBool("attack", true);
                    canPlayerAttack = false;
                    attackTimerDelay = attackTimer;
                }
            }
        }  
    }

    void stopAnimation()
    {
        this.gameObject.GetComponent<playerController>().playerAnimations.SetBool("attack", false);
        playerAxe.GetComponent<Collider>().enabled = false;
    }

    void stopAnimationKnightBlocking()
    {
        if (isKnight_V2Blocking == true)
        {
            this.gameObject.GetComponent<playerController>().playerAnimations.SetBool("attack", false);
            GameObject.FindWithTag("enemyKnight").GetComponent<knightAttack_V2>().playerAxeBlocking.GetComponent<Collider>().enabled = false;
        }
    }

  /*  private IEnumerator enableWeapon()
    {
        playerAxe.GetComponent<Collider>().enabled = true;
        this.gameObject.GetComponent<playerController>().playerAnimations.SetBool("attack", true);
        //this.gameObject.GetComponent<playerController>().playerAnimations.SetInteger("isAttacking", Random.Range(1, 3));
        yield return new WaitForSeconds(0.8f);
        playerAxe.GetComponent<Collider>().enabled = false;
    } */

   /* void enableWeapon()
    {
        playerAxe.GetComponent<Collider>().enabled = true;
        //attackTimerDelay = attackTimer;
        canPlayerAttack = false;
    } */

   /* void disableWeapon()
    {
        playerAxe.GetComponent<Collider>().enabled = false;
        canPlayerAttack = true;
        this.gameObject.GetComponent<playerController>().playerAnimations.SetBool("attack", false);
    } */

    private IEnumerator enableWeaponBlocking()
    {
        GameObject.FindWithTag("enemyKnight").GetComponent<knightAttack_V2>().playerAxeBlocking.GetComponent<Collider>().enabled = true;
        this.gameObject.GetComponent<playerController>().playerAnimations.SetInteger("isAttacking", Random.Range(1, 3));
        yield return new WaitForSeconds(0.8f);
        GameObject.FindWithTag("enemyKnight").GetComponent<knightAttack_V2>().playerAxeBlocking.GetComponent<Collider>().enabled = false;
    }
}
