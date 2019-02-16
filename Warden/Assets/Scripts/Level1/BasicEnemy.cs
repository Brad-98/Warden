using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicEnemy : MonoBehaviour
{
    public int maxEnemyHealth = 3;
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
            Destroy(gameObject, 5);
        }
    }

    public void damageTaken (int damageAmount)
    {
        StartCoroutine(Wait());
        currentEnemyHealth -= damageAmount;
    }

    private IEnumerator Wait()
    {
        enemyAnimations.SetBool("hit", true);
        yield return new WaitForSeconds(1);
        enemyAnimations.SetBool("hit", false);
    }
}
