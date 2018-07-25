using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuScreen : MonoBehaviour
{
    public GameObject MainMenuUI;
    public GameObject HelpMenuUI;

    public void PlayGame()
    {
        SceneManager.LoadScene("Scene1");
    }

    public void LoadMainMenu()
    {
        MainMenuUI.SetActive(true);
        HelpMenuUI.SetActive(false);
    }
    public void LoadHelpMenu()
    {
        HelpMenuUI.SetActive(true);
        MainMenuUI.SetActive(false);
    }

    public void QuitGame()
    {
        Debug.Log("Quitting Game");
        Application.Quit();
    }
}
