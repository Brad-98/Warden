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

    public void knightV2()
    {
        SceneManager.LoadScene("Knight_V2", 0);
    }

   
    public void wizardV2()
    {
        SceneManager.LoadScene("Wizard_V2", 0);
    }

    
    public void FOVEnemy()
    {
        SceneManager.LoadScene("FOV", 0);
    }

    public void comboEnemy()
    {
        SceneManager.LoadScene("AdvancedAI_Combo", 0);
    }

    public void dialog()
    {
        SceneManager.LoadScene("Dialog", 0);
    }

    public void navMesh()
    {
        SceneManager.LoadScene("Pathfinding", 0);
    }


    public void quit()
    {
        Application.Quit();
    }
}
