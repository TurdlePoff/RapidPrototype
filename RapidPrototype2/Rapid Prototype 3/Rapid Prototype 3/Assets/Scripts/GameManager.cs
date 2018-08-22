using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    public static GameManager instance = null;

    static private bool m_gameIsPaused = false;
    static private bool m_inMenu = false;
    static private bool m_stopTime = false;

    static private int m_Score = 0;
    static private int m_TotalScore = 0;
    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else if(instance != this)
        {
            Destroy(gameObject);
        }
        
        if (SceneManager.GetActiveScene().name == "GamePlay")
        {
            InvokeRepeating("FishRespawner", 60.0f, 30f);
        }

        DontDestroyOnLoad(gameObject);
    }

    void Update()
    {
        //Time pause code
        if(m_gameIsPaused)
        {
            Time.timeScale = 0.0f;
            m_stopTime = true;

        }
        else
        {
            Time.timeScale = 1.0f;
            m_stopTime = false;
        }

        //Ending game code
        if(SceneManager.GetActiveScene().name == "MenuScreen")
        {
            m_inMenu = true;
        }
        else if(SceneManager.GetActiveScene().name == "Main")
        {
            m_inMenu = false;
        }
        else if(SceneManager.GetActiveScene().name == "EndScreen")
        {
            
        }
    }

    static private void FishRespawner()
    {
        GameObject[] fish;
        fish = GlobalFlock.allFish;

        foreach (GameObject go in fish)
        {
            if(!go.activeSelf)
            {
                go.SetActive(true);
            }
        }
    }

    static public GameManager GetInstance()
    {
        return instance;
    }

    static public void SetGamePaused(bool paused)
    {
        m_gameIsPaused = paused;
    }

    static public bool IsGamePaused()
    {
        return m_gameIsPaused;
    }
    
    static public void SetInMenu(bool accessed)
    {
        m_inMenu = accessed;
    }

    static public bool GetInMenu()
    {
        return m_inMenu;
    }
    
    static public bool GetIsTimeStopped()
    {
        return m_stopTime;
    }

    static public void GameOver(bool _bEscaped)
    {
        SceneManager.LoadScene("EndScreen");
    }

    static public void IncreaseScore()
    {
        m_Score += 1;
        m_TotalScore += 1;
    }
    static public int GetScore()
    {
        return (m_Score);
    }
    static public void ClearScore()
    {
        m_Score = 0;
    }
    static public int GetTotalScore()
    {
        return (m_TotalScore);
    }

    static public void GameOver()
    {
        Debug.Log("Game Over");
        SceneManager.LoadScene("EndScreen");
    }
}
