using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    //public Animator a;

    Transform target;
    
    void Start()
    {
        //a = this.gameObject.GetComponent<Animator>();
        target = GameObject.Find("Player/enemyTarget").transform;
    }

    void Update()
    {
        transform.LookAt(target.position);
        transform.position += transform.forward * 6 * Time.deltaTime;
        //a.SetBool("isAttacking", true);
    }
}
