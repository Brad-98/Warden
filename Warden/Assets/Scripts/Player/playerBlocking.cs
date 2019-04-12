using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class playerBlocking : MonoBehaviour
{
    public Slider playerBlockBar;
    public bool playerBlockingForward;

    public float blockRecoveryTimerValue = 10.0f;
    public float blockRecoveryTimer;

    void Start()
    {
        //Hit blocking forward and 1 damage
        //Hit blocking left / right 2 damage
        //Hit jump attack 5 damage + knock down

    }

    void Update()
    {
        if (blockRecoveryTimer <= 0)
        {
            blockRecoveryTimer = 0;
        }

        if(blockRecoveryTimer > 0 && playerBlockBar.value < 10)
        {
            blockRecoveryTimer -= Time.deltaTime;
        }

        if(blockRecoveryTimer == 0)
        {
            playerBlockBar.value += 1.0f;
            blockRecoveryTimer = blockRecoveryTimerValue;
        }

        if (Input.GetMouseButton(1) && playerBlockBar.value != 0)
        {
            this.gameObject.GetComponent<playerController>().playerAnimations.SetBool("isBlocking", true);
        }
        else
        {
            this.gameObject.GetComponent<playerController>().playerAnimations.SetBool("isBlocking", false);
            playerBlockingForward = false;
        }
    }

    void stopMovingInBlock()
    {
        this.gameObject.GetComponent<playerController>().currentSpeed = 0;
        playerBlockingForward = true;
    }
}
