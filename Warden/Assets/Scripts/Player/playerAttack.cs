using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerAttack : MonoBehaviour
{
    Animator playerAnimations;

    public int damage = 1;

	void Start ()
    {
        playerAnimations = this.gameObject.GetComponent<Animator>();
	}

	void Update ()
    {
        if (Input.GetMouseButtonDown(0))
        {
            playerAnimations.SetBool("isWalkingForward", false);
            playerAnimations.SetInteger("isAttacking", Random.Range(1,3));
        }
        else
        {
            playerAnimations.SetInteger("isAttacking", 0);
        }
    }
}
