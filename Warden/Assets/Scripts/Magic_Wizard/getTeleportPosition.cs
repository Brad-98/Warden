using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class getTeleportPosition : MonoBehaviour
{
    public Vector3 myPosition;

    //public float teleportPositionDistanceFromPlayer;

    //Transform target;

    
    void Start()
    {
        myPosition = transform.position;
        //target = GameObject.Find("Player/enemyTarget").transform;
    }

    void Update()
    {
        //teleportPositionDistanceFromPlayer = Vector3.Distance(target.position, myPosition);

        Destroy(gameObject);
    }
}
