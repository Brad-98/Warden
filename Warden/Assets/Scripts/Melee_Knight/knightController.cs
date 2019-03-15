using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class knightController : MonoBehaviour
{
    public int maxKnightHealth = 20;
    private int currentKnightHealth;

    public float knightMoveSpeed = 2;
    public float currentKnightMoveSpeed;

    public Animator knightAnimations;

    public Transform playerLocation;
    
    void Start()
    {
        currentKnightHealth = maxKnightHealth;

        currentKnightMoveSpeed = knightMoveSpeed;
    }

    void Update()
    {
        if (currentKnightHealth <= 0)
        {
            //GetComponent<Animator>().enabled = false;
            Destroy(gameObject, 3);
        }

        transform.LookAt(playerLocation.position);
        transform.position += transform.forward * currentKnightMoveSpeed * Time.deltaTime; 
    }

    void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "playerAxe") //GET THE TIMERS RIGHT FOR ATTACKING ENEMY
        {
            GameObject.Find("Player").GetComponent<playerAttack>().playerAxe.GetComponent<Collider>().enabled = false;
            StartCoroutine(Wait());
            currentKnightHealth -= GameObject.Find("Player").GetComponent<playerAttack>().playerDamageValue;
        }

        if (collision.gameObject.tag == "playerAxeBlocking")
        {
            this.gameObject.GetComponent<knightAttack_V2>().playerAxeBlocking.GetComponent<Collider>().enabled = false;
            knightAnimations.SetBool("isKicking", true);
            this.gameObject.GetComponent<knightAttack_V2>().knightFoot.GetComponent<Collider>().enabled = true;
        }
    }

    private IEnumerator Wait()
    {
        knightAnimations.SetBool("takenDamage", true);
        yield return new WaitForSeconds(1.2f);
        knightAnimations.SetBool("takenDamage", false);
    }
}