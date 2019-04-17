using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rockController : MonoBehaviour
{
    public float rockSpeed = 100;
    private Rigidbody rockRB;

    public bool rockTouchingGround;

    void Start()
    {
        rockRB = this.gameObject.GetComponent<Rigidbody>();
        rockRB.AddForce(transform.forward * rockSpeed);
    }

    void Update()
    {
        StartCoroutine(cancelAddForce());
    }

    IEnumerator cancelAddForce()
    {
        yield return new WaitForSeconds(0.6f);
        rockRB.velocity = Vector3.zero;
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "ground")
        {
            rockTouchingGround = true;
        }
    }
}

