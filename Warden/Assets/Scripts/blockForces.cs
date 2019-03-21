using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class blockForces : MonoBehaviour
{
    public float force;

    private Rigidbody blockRB;

    private void Start()
    {
        force = Random.Range(-1500, -2500);

        blockRB = this.gameObject.GetComponent<Rigidbody>();
    }

    void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Boss")
        {
            blockRB.AddForce(transform.forward * force);
            transform.rotation = Random.rotation;
        }
    }
}
