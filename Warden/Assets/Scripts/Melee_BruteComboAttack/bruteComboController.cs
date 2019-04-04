using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bruteComboController : MonoBehaviour
{
    public int maxBruteHealth = 5;
    public int currentBruteHealth;

    public GameObject bruteAxeCollider;

    void Start()
    {
        currentBruteHealth = maxBruteHealth;
    }

    void Update()
    {
        if (currentBruteHealth <= 0)
        {
            this.gameObject.GetComponent<bruteComboLogic>().enabled = false;
            this.gameObject.GetComponent<bruteComboController>().enabled = false;
            this.gameObject.GetComponent<bruteComboLogic>().bruteAnimations.SetBool("isDead", true);
            Destroy(gameObject, 10);

            this.gameObject.GetComponent<Collider>().enabled = false;
            bruteAxeCollider.GetComponent<Collider>().enabled = false;
            
        }
    }

    void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "playerAxe") //GET THE TIMERS RIGHT FOR ATTACKING ENEMY
        {
            GameObject.Find("Player").GetComponent<playerAttack>().playerAxe.GetComponent<Collider>().enabled = false;
            //StartCoroutine(Wait());
            currentBruteHealth -= GameObject.Find("Player").GetComponent<playerAttack>().playerDamageValue;
        }
    }

   /* private IEnumerator Wait()
    {
        knightAnimations.SetBool("takenDamage", true);
        yield return new WaitForSeconds(1.2f);
        knightAnimations.SetBool("takenDamage", false);
    }*/
}