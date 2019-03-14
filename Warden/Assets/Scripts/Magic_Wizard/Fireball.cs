using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : MonoBehaviour
{
    public float fireballSpeed = 10f;

    public GameObject explosion;

    public int fireballDamage = 1;

    public float timer = 5f;

    private void Start()
    {
        //transform.rotation = Quaternion.Euler(-90, 0, 0);
    }
    void Update()
    {

        //Vector3 velocity = new Vector3(0, speed * Time.deltaTime, 0);

        transform.position += transform.forward * fireballSpeed * Time.deltaTime;
        



        timer -= Time.deltaTime;

        if (timer <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "Player" || collision.gameObject.tag == "ground")
        {
            Destroy(gameObject);
            Instantiate(explosion, transform.position, transform.rotation);
        }
    }
}
