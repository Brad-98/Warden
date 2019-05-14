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
            GameObject.Find("Player").GetComponent<playerAttack>().playerSwordCollider.GetComponent<Collider>().enabled = false;
            this.gameObject.GetComponent<bruteComboLogic>().comboEnemyAxe.GetComponent<Collider>().enabled = false;
            this.gameObject.GetComponent<bruteComboLogic>().bruteAnimations.SetBool("isHit", true);
            currentBruteHealth -= GameObject.Find("Player").GetComponent<playerAttack>().playerDamageValue;
        }
    }

    void endHitAnimation()
    {
        this.gameObject.GetComponent<bruteComboLogic>().bruteAnimations.SetBool("isHit", false);
    }
}