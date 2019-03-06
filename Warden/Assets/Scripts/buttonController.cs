using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class buttonController : MonoBehaviour
{
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

    public void quit()
    {
        Application.Quit();
    }
}
