using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerThrowRock : MonoBehaviour
{
    public GameObject rock;
    public Transform spawnRockLocation;
    public Animator playerAnimations;

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            playerAnimations.SetBool("isThrowing", true);
        }
    }

    void throwRock()
    {
        Instantiate(rock, spawnRockLocation.position, spawnRockLocation.rotation);
    }

    void endThrowAnimation()
    {
        playerAnimations.SetBool("isThrowing", false);
    }
}
