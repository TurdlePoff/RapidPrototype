using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour {
    
    public GameObject pauseMenuUI;
    public GameObject backpackUI;

    // Update is called once per frame
    void Update ()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if(GameManager.IsGamePaused())
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        GameManager.SetGamePaused(false);
    }

    void Pause()
    {
        pauseMenuUI.SetActive(true);
        GameManager.SetGamePaused(true);
    }

    public void LoadMenu()
    {
        Debug.Log("Loading Menu");
        SceneManager.LoadScene("MenuScreen");
    }

    //public void LoadBackpack()
    //{
    //    Debug.Log("Loading Backpack");
    //    backpackUI.SetActive(true);
    //    inBackpackMode = true;
    //    //GameManager.SetInInventory(true);
    //}

    public void CloseBackpack()
    {
        backpackUI.SetActive(false);
        GameManager.SetInInventory(false);
        Debug.Log("Closing Backpack");
    }

    public void QuitGame()
    {
        Debug.Log("Quitting Game");
        Application.Quit();
    }
}
