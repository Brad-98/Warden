using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerAttack : MonoBehaviour
{
    public GameObject playerSwordCollider;

    public int playerDamageValue = 1;

    public bool canPlayerAttack;

    public bool isKnight_V2Blocking = false;

    private void Awake()
    {
        canPlayerAttack = true;
    }

    void Update ()
    {  
        if ((Input.GetMouseButtonDown(0)) && (canPlayerAttack == true))
        {
            // TODO: Fix player attacking, remember about isBlocking =true then use the other weapon collider for the kick
            
            if (isKnight_V2Blocking == false)
            {
                playerSwordCollider.GetComponent<Collider>().enabled = true;
                this.gameObject.GetComponent<playerController>().playerAnimations.SetBool("isAttacking", true);
                canPlayerAttack = false;
                 
            }
            else
            {
                GameObject.FindWithTag("enemyKnight").GetComponent<knightAttack_V2>().playerAxeBlocking.GetComponent<Collider>().enabled = true;
                this.gameObject.GetComponent<playerController>().playerAnimations.SetBool("isAttacking", true);
                canPlayerAttack = false;
  
            }
        }  
    }

    void stopAnimation()
    {
        this.gameObject.GetComponent<playerController>().playerAnimations.SetBool("isAttacking", false);
        playerSwordCollider.GetComponent<Collider>().enabled = false;
        canPlayerAttack = true;
    }

    void stopAnimationKnightBlocking()
    {
        if (isKnight_V2Blocking == true)
        {
            this.gameObject.GetComponent<playerController>().playerAnimations.SetBool("isAttacking", false);
            GameObject.FindWithTag("enemyKnight").GetComponent<knightAttack_V2>().playerAxeBlocking.GetComponent<Collider>().enabled = false;
            canPlayerAttack = true;
        }
    }

    private IEnumerator enableWeaponBlocking()
    {
        GameObject.FindWithTag("enemyKnight").GetComponent<knightAttack_V2>().playerAxeBlocking.GetComponent<Collider>().enabled = true;
        this.gameObject.GetComponent<playerController>().playerAnimations.SetInteger("isAttacking", Random.Range(1, 3));
        yield return new WaitForSeconds(0.8f);
        GameObject.FindWithTag("enemyKnight").GetComponent<knightAttack_V2>().playerAxeBlocking.GetComponent<Collider>().enabled = false;
    }
}
