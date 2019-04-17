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
        Destroy(GameObject.Find("Rock(Clone)"));
        Instantiate(rock, spawnRockLocation.position, spawnRockLocation.rotation);
        GameObject.Find("Rock(Clone)").GetComponent<rockController>().rockTouchingGround = false;
    }

    void endThrowAnimation()
    {
        playerAnimations.SetBool("isThrowing", false);
    }
}
