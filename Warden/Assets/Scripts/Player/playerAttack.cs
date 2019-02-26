using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerAttack : MonoBehaviour
{
    Animator playerAnimations;

    public int damage = 1;

   // Collider swordCollisionBox;
    public GameObject enemy;

	// Use this for initialization
	void Start ()
    {
        playerAnimations = this.gameObject.GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (Input.GetMouseButtonDown(0))
        {
            StartCoroutine(Wait());
        }
    }

    private IEnumerator Wait()
    {
        playerAnimations.SetBool("isAttacking1", true);
        yield return new WaitForSeconds(0.4f);
        playerAnimations.SetBool("isAttacking1", false);
    }

    /*private void OnCollisionEnter(Collision collision)
    {
        if (Input.GetMouseButtonDown(0))
        {
            //swordCollisionBox.
            enemy.GetComponent<BasicEnemy>().damageTaken(damage);
        }
    }*/
}
