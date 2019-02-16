using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerAttack : MonoBehaviour {

    public int damage = 1;

   // Collider swordCollisionBox;
    public GameObject enemy;

	// Use this for initialization
	void Start ()
    {
        //swordCollisionBox = GameObject.
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (Input.GetMouseButtonDown(0))
        {
            //swordCollisionBox.
            enemy.GetComponent<BasicEnemy>().damageTaken(damage);
        }

    }

    /*private void OnCollisionEnter(Collision collision)
    {
        if (Input.GetMouseButtonDown(0))
        {
            //swordCollisionBox.
            enemy.GetComponent<BasicEnemy>().damageTaken(damage);
        }
    }*/
}
