using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerAttack : MonoBehaviour
{
    public GameObject playerAxe;

    public int playerDamageValue = 1;
    public bool canPlayerAttack = true;
   // public float attackTimer = 1.8f;
   // public float attackTimerDelay;

	void Update ()
    {
        // Debug.Log(attackTimerDelay);
     //   attackTimerDelay -= Time.deltaTime;

     //   if (attackTimerDelay <= 0)
      //  {
       //     attackTimerDelay = 0;
       // }

        if (Input.GetMouseButtonDown(0) && canPlayerAttack == true)
        {

            //if (GameObject.FindWithTag("enemyKnight").GetComponent<knightAttack_V2>().isBlocking == false)
            this.gameObject.GetComponent<playerController>().playerAnimations.SetBool("attack", true);
            //StartCoroutine(enableWeapon());
            
            // else
            // {
            //     StartCoroutine(enableWeaponBlocking());
            // }


        }
        else {  }
    }

   /* private IEnumerator enableWeapon()
    {
        playerAxe.GetComponent<Collider>().enabled = true;
        this.gameObject.GetComponent<playerController>().playerAnimations.SetInteger("isAttacking", Random.Range(1, 3));
        yield return new WaitForSeconds(0.8f);
        playerAxe.GetComponent<Collider>().enabled = false;
    } */

    void enableWeapon()
    {
        playerAxe.GetComponent<Collider>().enabled = true;
        //attackTimerDelay = attackTimer;
        canPlayerAttack = false;
    }

    void disableWeapon()
    {
        playerAxe.GetComponent<Collider>().enabled = false;
        canPlayerAttack = true;
        this.gameObject.GetComponent<playerController>().playerAnimations.SetBool("attack", false);
    }

    private IEnumerator enableWeaponBlocking()
    {
        GameObject.FindWithTag("enemyKnight").GetComponent<knightAttack_V2>().playerAxeBlocking.GetComponent<Collider>().enabled = true;
        this.gameObject.GetComponent<playerController>().playerAnimations.SetInteger("isAttacking", Random.Range(1, 3));
        yield return new WaitForSeconds(0.8f);
        GameObject.FindWithTag("enemyKnight").GetComponent<knightAttack_V2>().playerAxeBlocking.GetComponent<Collider>().enabled = false;
    }
}
