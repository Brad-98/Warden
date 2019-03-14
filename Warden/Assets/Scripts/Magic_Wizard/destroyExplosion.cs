using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class destroyExplosion : MonoBehaviour
{
    private float timer = 3.0f;

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;

        if(timer <= 0)
        {
            Destroy(gameObject);
        }
    }
}
