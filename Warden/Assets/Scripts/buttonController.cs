using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class buttonController : MonoBehaviour
{
    private void Start()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
    public void mainMenuToChooseLevel()
    {  
        SceneManager.LoadScene("chooseLevel", 0);
    }

    public void knightV1()
    {
        SceneManager.LoadScene("Knight_V1", 0);
    }

    public void knightV2()
    {
        SceneManager.LoadScene("Knight_V2", 0);
    }

    public void wizardV1()
    {
        SceneManager.LoadScene("Wizard_V1", 0);
    }

    public void quit()
    {
        Application.Quit();
    }
}
