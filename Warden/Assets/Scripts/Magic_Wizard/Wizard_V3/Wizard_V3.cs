using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wizard_V3 : MonoBehaviour
{
    public int maxEnemyHealth = 20;
    private int currentEnemyHealth;

    public float enemyMoveSpeed = 2;
    private float currentEnemyMoveSpeed;

    public int enemyDamageValue = 1;

    public float rangeToAttack = 1.8f;
    private float distanceFromPlayer;

    public float enemyAttackTimer = 1.5f;
    public float enemyAttackTimerDelay;

    public GameObject enemyMinion;
    public GameObject spawnMinionLocation1;
    public GameObject spawnMinionLocation2;

    public float minionSpawnTimer = 5.0f;
    public float currentMinionSpawnTimer;

    public GameObject teleportLocation;

    Animator enemyAnimations;

    Transform target;

    public Transform spellLocation;

    public GameObject fireballPrefab;
    // Start is called before the first frame update
    void Start()
    {
        currentEnemyHealth = maxEnemyHealth;

        currentEnemyMoveSpeed = enemyMoveSpeed;

        enemyAttackTimerDelay = enemyAttackTimer;

        currentMinionSpawnTimer = minionSpawnTimer;

        target = GameObject.Find("Player/enemyTarget").transform;
        
        enemyAnimations = this.gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (currentMinionSpawnTimer <= 0)
        {
            currentMinionSpawnTimer = 0;
        }

        if (currentMinionSpawnTimer > 0)
        {
            currentMinionSpawnTimer -= Time.deltaTime;
        }

        if (currentMinionSpawnTimer == 0)
        {
            Instantiate(enemyMinion, spawnMinionLocation1.transform.position, Quaternion.identity);
            Instantiate(enemyMinion, spawnMinionLocation2.transform.position, Quaternion.identity);
            currentMinionSpawnTimer = minionSpawnTimer;
        }

        if (currentEnemyHealth <= 0)
        {
            //GetComponent<Animator>().enabled = false;
            Destroy(gameObject, 3);
        }

        if(GameObject.FindWithTag("teleportLocation") != null)
        {
            Teleport();
        }

        transform.LookAt(target.position);

        if (enemyAttackTimerDelay <= 0)
        {
            enemyAttackTimerDelay = 0;
        }

        if (enemyAttackTimerDelay > 0)
        {
            enemyAttackTimerDelay -= Time.deltaTime;
        }

        if (enemyAttackTimerDelay == 0)
        {
            //enemyAnimations.SetBool("isWalking", false);
            //enemyAnimations.SetBool("isAttacking", true);
            Instantiate(fireballPrefab, spellLocation.position, spellLocation.rotation);
            enemyAttackTimerDelay = enemyAttackTimer;
        }

        distanceFromPlayer = Vector3.Distance(target.position, transform.position);

        if (distanceFromPlayer <= 15)
        {
            transform.position -= transform.forward * currentEnemyMoveSpeed * Time.deltaTime;

            
        }
        else
        {
            // enemyAnimations.SetBool("isAttacking", false);
            //  enemyAnimations.SetBool("isWalking", true);

            //currentEnemyMoveSpeed = 0;
        }
    }

    void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "playerAxe") //GET THE TIMERS RIGHT FOR ATTACKING ENEMY
        {
            // Debug.Log(currentEnemyHealth);
            StartCoroutine(Wait());
            currentEnemyHealth -= GameObject.Find("Player").GetComponent<playerAttack>().playerDamageValue;
        }
    }

    private IEnumerator Wait()
    {
        enemyAnimations.SetBool("takenDamage", true);
        yield return new WaitForSeconds(1.2f);
        enemyAnimations.SetBool("takenDamage", false);
    }

    void Teleport()
    {
        //if (GameObject.FindWithTag("teleportLocation").GetComponent<getTeleportPosition>().teleportPositionDistanceFromPlayer <= 8.0f)
       // {
            //Debug.Log("In Range");
            
            //GameObject.Find("Teleport Field").GetComponent<Teleport>().currentTeleSpawnTimer = 0.1f;
            //Destroy(GameObject.FindWithTag("teleportLocation"));
        //}
        //else
       // {
            //Debug.Log("Not In Range");
            transform.position = GameObject.FindWithTag("teleportLocation").GetComponent<getTeleportPosition>().myPosition;
        //}
    }
}