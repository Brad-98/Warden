using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rockController : MonoBehaviour
{
    public float rockSpeed = 100;
    private Rigidbody rockRB;

    void Start()
    {
        rockRB = this.gameObject.GetComponent<Rigidbody>();
        rockRB.AddForce(transform.forward * rockSpeed);
    }
}
