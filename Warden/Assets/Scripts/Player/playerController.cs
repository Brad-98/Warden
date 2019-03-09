using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class playerController : MonoBehaviour
{
    private Rigidbody playerRB;

    private Animator playerAnimations;

    Transform cameraT;

    public float moveSpeed = 2.8f;
    public float sprintSpeed = 5.0f;
    private float currentSpeed;

    public float jumpHeight = 5.0f;
    public float fallingAccleration = 2.5f;

    public int playerHealth = 5;
    private int currentPlayerHealth;

    private bool touchingGround = true;
    
	void Start ()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        playerRB = GetComponent<Rigidbody>();

        playerAnimations = this.gameObject.GetComponent<Animator>();

        cameraT = Camera.main.transform;

        currentSpeed = moveSpeed;

        currentPlayerHealth = playerHealth;
	}
	
	// Update is called once per frame
	void Update ()
    {
        if(currentPlayerHealth == 0)
        {
            Destroy(gameObject);
            SceneManager.LoadScene("chooseLevel", 0);
        }

        Vector3 direction = Vector3.zero;
        direction.z = Input.GetAxis("Horizontal");
        direction.x = Input.GetAxis("Vertical");
        direction.y = Input.GetAxis("Jump");

        if (Input.GetKeyDown("escape"))
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }

        float zAxisSpeed = (Input.GetAxis("Vertical") * currentSpeed) * Time.deltaTime;
        float xAxisSpeed = (Input.GetAxis("Horizontal") * currentSpeed) * Time.deltaTime; //Change to z

        transform.Translate(xAxisSpeed, 0, zAxisSpeed);
        transform.eulerAngles = Vector3.up * cameraT.eulerAngles.y;

        if (Input.GetButtonDown("Jump") && (touchingGround == true))
        {
            playerRB.velocity = Vector3.up * jumpHeight;
        }

        if (playerRB.velocity.y < 0)
        {
            playerRB.velocity += Vector3.up * Physics.gravity.y * (fallingAccleration - 1) * Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.LeftShift))
        {
            currentSpeed = sprintSpeed;
        }
        else
        {
            currentSpeed = moveSpeed;
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

        /* if (direction.y > 0)
        {
            playerAnimations.SetBool("isJumping", true);
        }
        else
        {
            playerAnimations.SetBool("isJumping", false);
        } */
    }

    void OnCollisionEnter(Collision playerCollider)
    {
        if(playerCollider.gameObject.tag == "ground")
        {
            touchingGround = true;
        }
    }

    void OnCollisionExit(Collision playerCollider)
    {
        if (playerCollider.gameObject.tag == "ground")
        {
            touchingGround = false;
        }
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "enemySword") //GET THE TIMERS RIGHT FOR ATTACKING ENEMY
        {
             
            //StartCoroutine(Wait());
            //currentPlayerHealth -= GameObject.FindWithTag("enemyKnight").GetComponent<Enemy>().enemyDamageValue;
            currentPlayerHealth -= GameObject.FindWithTag("enemyKnight").GetComponent<Knight_V2>().enemyDamageValue;
            //Debug.Log(currentPlayerHealth);
        }

        if (collision.gameObject.tag == "Fireball") //GET THE TIMERS RIGHT FOR ATTACKING ENEMY
        {
            
            //StartCoroutine(Wait());
            currentPlayerHealth -= collision.gameObject.GetComponent<Fireball>().fireballDamage;
            Debug.Log(currentPlayerHealth);
        }
    }
}
