using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : MonoBehaviour
{
    public Vector3 center;
    public Vector3 size;

    public GameObject teleportLocation;

    private float teleSpawnTimer = 10.0f;
    public float currentTeleSpawnTimer;

    void Start()
    {
        currentTeleSpawnTimer = teleSpawnTimer;
    }

    void Update()
    {
        currentTeleSpawnTimer -= Time.deltaTime;

        if (currentTeleSpawnTimer <= 0)
        {
            spawnTeleportLocation();
            currentTeleSpawnTimer = teleSpawnTimer;
        }
    }

    void spawnTeleportLocation()
    {
        Vector3 pos = center + new Vector3(Random.Range(-size.x / 2, size.x / 2), 0, Random.Range(-size.z / 2, size.z / 2));
        Instantiate(teleportLocation, pos, Quaternion.identity);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = new Color(1 ,0, 0, 0.5f);
        Gizmos.DrawCube(center, size);
    }
}
