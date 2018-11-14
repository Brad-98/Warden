using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerController : MonoBehaviour
{
    public float moveSpeed = 7.0f;
    public float sprintSpeed = 12.0f;
    public float currentSpeed;

    public float jumpHeight = 5.0f;
    public float fallingAccleration = 2.5f;
    
    Rigidbody playerRB;

	// Use this for initialization
	void Start ()
    {
        Cursor.lockState = CursorLockMode.Locked;
        playerRB = GetComponent<Rigidbody>();

        currentSpeed = moveSpeed;
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            currentSpeed = sprintSpeed;
        }
        else
        {
            currentSpeed = moveSpeed;
        }
        
        float zAxisSpeed = (Input.GetAxis("Vertical") * currentSpeed) * Time.deltaTime;
        float xAxisSpeed = (Input.GetAxis("Horizontal") * currentSpeed) * Time.deltaTime;

        transform.Translate(xAxisSpeed, 0, zAxisSpeed);

        if (Input.GetKeyDown("escape"))
        {
            Cursor.lockState = CursorLockMode.None;
        }
    }

    void FixedUpdate()
    {
        if(Input.GetButtonDown("Jump"))
        {
            playerRB.velocity = Vector3.up * jumpHeight;
        }

        if (playerRB.velocity.y < 0)
        {
            playerRB.velocity += Vector3.up * Physics.gravity.y * (fallingAccleration - 1) * Time.deltaTime;
        }
    }

}
