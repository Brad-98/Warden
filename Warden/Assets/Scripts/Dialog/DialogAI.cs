using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogAI : MonoBehaviour
{
    public GameObject dialogBox;
    public GameObject player;
    public GameObject pressEText;
    public Camera mainCamera;

    public GameObject healthBar;
    public GameObject sprintBar;

    private bool turnPressETextOff = true;

    void Update()
    {
        if(turnPressETextOff == false)
        {
            pressEText.SetActive(false);
        }

        if(player.GetComponent<playerController>().isTalkingToDialogAI == true && turnPressETextOff == true)
        {
            pressEText.SetActive(true);

            if (Input.GetKeyDown(KeyCode.E))
            {
                turnPressETextOff = false;
                dialogBox.SetActive(true);
                healthBar.SetActive(false);
                sprintBar.SetActive(false);
                player.GetComponent<playerController>().enabled = false;
                player.GetComponent<playerAttack>().enabled = false;
                mainCamera.GetComponent<playerThirdPersonCamera>().enabled = false;
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
            }
        }
        else
        {
            pressEText.SetActive(false);
        }
    }

    public void buttonTeleportPlayer()
    {
        dialogBox.SetActive(false);
        healthBar.SetActive(true);
        sprintBar.SetActive(true);
        player.GetComponent<playerController>().enabled = true;
        player.GetComponent<playerAttack>().enabled = true;
        mainCamera.GetComponent<playerThirdPersonCamera>().enabled = true;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        player.transform.position = new Vector3(-6, 0, -30);
    }
}
