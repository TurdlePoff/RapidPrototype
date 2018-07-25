using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuScreen : MonoBehaviour
{
    public GameObject MainMenuUI;
    public GameObject HelpMenuUI;
    public GameObject StartMenuUI;

    public void PlayGame()
    {
        SceneManager.LoadScene("Scene1");
    }

    public void LoadStartMenu()
    {
        StartMenuUI.SetActive(true);
        HelpMenuUI.SetActive(false);
        MainMenuUI.SetActive(false);
    }
    public void LoadMainMenu()
    {
        MainMenuUI.SetActive(true);
        HelpMenuUI.SetActive(false);
        StartMenuUI.SetActive(false);
    }
    public void LoadHelpMenu()
    {
        HelpMenuUI.SetActive(true);
        MainMenuUI.SetActive(false);
        StartMenuUI.SetActive(false);
    }


    public void QuitGame()
    {
        Debug.Log("Quitting Game");
        Application.Quit();
    }
}
