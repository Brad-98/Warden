using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerController : MonoBehaviour
{
    public float moveSpeed = 7.0f;

	// Use this for initialization
	void Start ()
    {
        Cursor.lockState = CursorLockMode.Locked;
	}
	
	// Update is called once per frame
	void FixedUpdate ()
    {
        float zAxisSpeed = (Input.GetAxis("Vertical") * moveSpeed) * Time.deltaTime;
        float xAxisSpeed = (Input.GetAxis("Horizontal") * moveSpeed) * Time.deltaTime;

        transform.Translate(xAxisSpeed, 0, zAxisSpeed);

        if(Input.GetKeyDown("escape"))
        {
            Cursor.lockState = CursorLockMode.None;
        }
    }
}
