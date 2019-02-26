﻿using UnityEngine.SceneManagement;
using UnityEngine;

public class restart : MonoBehaviour
{
    Scene currentScene;
    string currentSceneName;

    public bool FPS = false;

    private void Start()
    {
        currentScene = SceneManager.GetActiveScene();
        currentSceneName = currentScene.name;
    }
    // Update is called once per frame
    void Update()
    {
        if (FPS == true)
        {
            Debug.Log(1.0f / Time.deltaTime);
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(currentSceneName, 0);
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }
}
