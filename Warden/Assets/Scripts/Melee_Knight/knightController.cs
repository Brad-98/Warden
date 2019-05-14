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

    public GameObject swordCollider;

    public AudioClip hitEffect;
    public AudioSource hitEffectSource;

    void Start()
    {
        currentKnightHealth = maxKnightHealth;

        currentKnightMoveSpeed = knightMoveSpeed;

        hitEffectSource.clip = hitEffect;
    }

    void Update()
    {
        if (currentKnightHealth <= 0)
        {
            knightAnimations.SetBool("isDead", true);
            Destroy(gameObject, 10);

            //TODO Make some UI text that says victory!
            this.gameObject.GetComponent<Collider>().enabled = false;
            swordCollider.GetComponent<Collider>().enabled = false;
            this.gameObject.GetComponent<knightController>().enabled = false;
            //TODO KnightV2 can hit you when it's dead i think is when blocking kick
        }

        transform.LookAt(playerLocation.position);
        transform.position += transform.forward * currentKnightMoveSpeed * Time.deltaTime;
    }

    void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "playerAxe") //GET THE TIMERS RIGHT FOR ATTACKING ENEMY
        {
            GameObject.Find("Player").GetComponent<playerAttack>().playerSwordCollider.GetComponent<Collider>().enabled = false;
            StartCoroutine(Wait());
            currentKnightHealth -= GameObject.Find("Player").GetComponent<playerAttack>().playerDamageValue;
            hitEffectSource.Play();
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