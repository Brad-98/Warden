using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class minionSpawner : MonoBehaviour
{
    public GameObject minion;

    public float spawnTimer;
    public float currentSpawnTimer;

    public bool canSpawn = false;

    // Start is called before the first frame update
    void Start()
    {
        spawnTimer = Random.Range(3, 7);

        currentSpawnTimer = spawnTimer;
    }

    // Update is called once per frame
    void Update()
    {
        if(currentSpawnTimer > 0)
        {
            currentSpawnTimer -= Time.deltaTime;
        }

        if(currentSpawnTimer <= 0)
        {
            currentSpawnTimer = 0;
        }

        if(currentSpawnTimer == 0)
        {
            canSpawn = true;
        }

        if(canSpawn == true)
        {
            Instantiate(minion, transform.position, Quaternion.identity);
            canSpawn = false;
            currentSpawnTimer = spawnTimer;
        }
    }
}
