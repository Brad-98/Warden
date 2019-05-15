using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bossController : MonoBehaviour
{
    public int maxBossHealth = 10;
    private int currentBossHealth;

    public bool startPhase2 = false;
    // Start is called before the first frame update
    void Start()
    {
        currentBossHealth = maxBossHealth;
        this.gameObject.GetComponent<bossLogicPhase2>().enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(currentBossHealth <= 7)
        {
            this.gameObject.GetComponent<bossLogicPhase2>().enabled = true;
            startPhase2 = true;
        }

        if (currentBossHealth <= 0)
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "playerAxe") //GET THE TIMERS RIGHT FOR ATTACKING ENEMY
        {
            GameObject.Find("Player").GetComponent<playerAttack>().playerSwordCollider.GetComponent<Collider>().enabled = false;
            currentBossHealth -= GameObject.Find("Player").GetComponent<playerAttack>().playerDamageValue;
        }
    }
}
