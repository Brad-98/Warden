﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine;

public class playerController : MonoBehaviour
{
    private Rigidbody playerRB;

    public Animator playerAnimations;

    public Slider playerHealthBar;
    public Slider playerSprintBar;

    Transform cameraT;

    public float moveSpeed = 2.8f;
    public float sprintSpeed = 5.0f;
    public float currentSpeed;

    public bool isPlayerSpinting;
    public bool isTalkingToDialogAI;

    public AudioClip hitEffect;
    public AudioSource hitEffectSource;

    void Start ()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        playerRB = GetComponent<Rigidbody>();

        playerAnimations = this.gameObject.GetComponent<Animator>();

        cameraT = Camera.main.transform;

        currentSpeed = moveSpeed;

        hitEffectSource.clip = hitEffect;
    }

	void Update ()
    {
        
        if (isPlayerSpinting == true)
        {
        currentSpeed = sprintSpeed;
        }
        else
        {
        currentSpeed = moveSpeed;
        }
        
        if (playerHealthBar.value <= 0)
        {
            playerAnimations.SetBool("isDead", true);
            StartCoroutine(goToChooseLevelScene());
        }

        Vector3 direction = Vector3.zero;
        direction.z = Input.GetAxis("Horizontal");
        direction.x = Input.GetAxis("Vertical");

        if (Input.GetKeyDown("escape"))
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }

        float zAxisSpeed = (Input.GetAxis("Vertical") * currentSpeed) * Time.deltaTime;
        float xAxisSpeed = (Input.GetAxis("Horizontal") * currentSpeed) * Time.deltaTime; //Change to z

        transform.Translate(xAxisSpeed, 0, zAxisSpeed);
        transform.eulerAngles = Vector3.up * cameraT.eulerAngles.y;

        if (playerSprintBar.value != 0)
        {
            if ((Input.GetKey(KeyCode.LeftShift)) && (Input.GetKey(KeyCode.W)) && (!Input.GetKey(KeyCode.A)) && (!Input.GetKey(KeyCode.D)) && (!Input.GetKey(KeyCode.S)))
            {
                isPlayerSpinting = true;
                currentSpeed = sprintSpeed;
                playerAnimations.SetBool("isRunning", true);
                playerSprintBar.value -= Time.deltaTime;
            }
            else
            {
                isPlayerSpinting = false;
                currentSpeed = moveSpeed;
                playerAnimations.SetBool("isRunning", false);
                playerSprintBar.value += Time.deltaTime;
            }
        }

        if (playerSprintBar.value == 0 && (Input.GetKey(KeyCode.LeftShift)))
        {
            playerAnimations.SetBool("isRunning", false);
            currentSpeed = moveSpeed;
        }

        if (playerSprintBar.value == 0 && (!Input.GetKey(KeyCode.LeftShift)))
        {
            playerAnimations.SetBool("isRunning", false);
            playerSprintBar.value += Time.deltaTime;
        }

        if (direction.x > 0)
        {
            playerAnimations.SetBool("isWalkingForward", true);
        }
        else
        {
            playerAnimations.SetBool("isWalkingForward", false);
        }

        if (direction.x < 0)
        {
            playerAnimations.SetBool("isWalkingBackwards", true);
        }
        else
        {
            playerAnimations.SetBool("isWalkingBackwards", false);
        }

        if (direction.z > 0)
        {
            playerAnimations.SetBool("isWalkingRight", true);
        }
        else
        {
            playerAnimations.SetBool("isWalkingRight", false);
        }

        if (direction.z < 0)
        {
            playerAnimations.SetBool("isWalkingLeft", true);
        }
        else
        {
            playerAnimations.SetBool("isWalkingLeft", false);
        }
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "knightSword_V1") 
        {
            GameObject.FindWithTag("enemyKnight").GetComponent<knightAttack_V1>().knightSword.GetComponent<Collider>().enabled = false;
            playerHealthBar.value -= GameObject.FindWithTag("enemyKnight").GetComponent<knightAttack_V1>().knightDamageValue;
            hitEffectSource.Play();
        }

        if (collision.gameObject.tag == "scoutFriendSword") 
        {
            GameObject.FindWithTag("scoutFriend").GetComponent<scoutFriendController>().scoutFriendSword.GetComponent<Collider>().enabled = false;
            playerHealthBar.value -= GameObject.FindWithTag("scoutFriend").GetComponent<scoutFriendController>().scoutFriendDamageValue;
            hitEffectSource.Play();
        }

        if (collision.gameObject.tag == "bossHand") 
        {
            GameObject.FindWithTag("Boss").GetComponent<bossLogicPhase1>().bossHandCollider_Phase1.GetComponent<Collider>().enabled = false;
            playerHealthBar.value -= GameObject.FindWithTag("Boss").GetComponent<bossLogicPhase1>().bossDamageValue;
            hitEffectSource.Play();
        }

        if (collision.gameObject.tag == "comboEnemyAxe") 
        {
            GameObject.FindWithTag("comboEnemy").GetComponent<bruteComboLogic>().comboEnemyAxe.GetComponent<Collider>().enabled = false;
            GameObject.FindWithTag("comboEnemy").GetComponent<bruteComboLogic>().jumpAttackCollider.GetComponent<Collider>().enabled = false;
            playerHealthBar.value -= GameObject.FindWithTag("comboEnemy").GetComponent<bruteComboLogic>().comboEnemyDamageValue;
            hitEffectSource.Play();
        }

        if (collision.gameObject.tag == "minionAttackCollider")
        {
            GameObject.FindWithTag("minion").GetComponent<Minion_AI>().minionAttackCollider.GetComponent<Collider>().enabled = false;
            playerHealthBar.value -= GameObject.FindWithTag("minion").GetComponent<Minion_AI>().minionDamageValue;
            hitEffectSource.Play();
        }

        if (collision.gameObject.tag == "knightSword_V2") 
        {
            GameObject.FindWithTag("enemyKnight").GetComponent<knightAttack_V2>().knightSword.GetComponent<Collider>().enabled = false;
            playerHealthBar.value -= GameObject.FindWithTag("enemyKnight").GetComponent<knightAttack_V2>().knightDamageValue;
            hitEffectSource.Play();
        }

        if (collision.gameObject.tag == "knightFoot") 
        {
            GameObject.FindWithTag("enemyKnight").GetComponent<knightAttack_V2>().knightFoot.GetComponent<Collider>().enabled = false;
            playerHealthBar.value -= GameObject.FindWithTag("enemyKnight").GetComponent<knightAttack_V2>().knightFootDamageValue;
            playerRB.AddForce(transform.forward * -250);
            hitEffectSource.Play();
        }

        if (collision.gameObject.tag == "Fireball") 
        {
            playerHealthBar.value -= collision.gameObject.GetComponent<Fireball>().fireballDamage;
            hitEffectSource.Play();
        }

        if (collision.gameObject.tag == "DialogAI") 
        {
            isTalkingToDialogAI = true;
        }
    }

    private void OnTriggerExit(Collider collision)
    {
        if (collision.gameObject.tag == "DialogAI") 
        {
            isTalkingToDialogAI = false;
        }
    }

    IEnumerator goToChooseLevelScene()
    {
        yield return new WaitForSeconds(9.0f);
        SceneManager.LoadScene("chooseLevel", 0);
    }
}
