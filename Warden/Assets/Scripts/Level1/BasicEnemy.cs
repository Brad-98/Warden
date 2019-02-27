using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicEnemy : MonoBehaviour
{
    public int maxEnemyHealth = 20;
    private int currentEnemyHealth;
   
    Animator enemyAnimations;
    // Start is called before the first frame update
    void Start()
    {
        currentEnemyHealth = maxEnemyHealth;
        enemyAnimations = this.gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (currentEnemyHealth <= 0)
        {
            GetComponent<Animator>().enabled = false;
            Destroy(gameObject, 3);
        }
    }

     void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "Axe") //GET THE TIMERS RIGHT FOR ATTACKING ENEMY
        {
            Debug.Log(currentEnemyHealth);
            StartCoroutine(Wait());
            currentEnemyHealth -= GameObject.Find("Player").GetComponent<playerAttack>().damage;
        }
    }

    private IEnumerator Wait()
    {
        enemyAnimations.SetBool("takenDamage", true);
        yield return new WaitForSeconds(1.2f);
        enemyAnimations.SetBool("takenDamage", false);
    }
}
