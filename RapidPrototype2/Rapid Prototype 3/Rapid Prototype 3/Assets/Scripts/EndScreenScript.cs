using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndScreenScript : MonoBehaviour {

    
    public void MainMenu()
    {
        SceneManager.LoadScene("MenuScreen");
    }

    public void RestartGame()
    {
        SceneManager.LoadScene("GamePlay");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
