using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Mana : MonoBehaviour 
{
	public float m_StartingMana = 50f;
    private float m_MaxMana = 100f;
    public Slider m_Slider;

	private float m_CurrentHealth;
	private bool m_Dead;
    private GameController gameOverScript;

    private void OnEnable()
	{
		m_CurrentHealth = m_StartingMana;
		m_Dead = false;

        GameObject gameManager = GameObject.FindGameObjectWithTag("GameController");
        gameOverScript = gameManager.GetComponent<GameController>();

        SetHealthUI ();
	}

	public void TakeDamage(float amount)
	{
		m_CurrentHealth -= amount;
		SetHealthUI ();
		if (m_CurrentHealth <= 0f && !m_Dead) 
		{
			OnDeath ();
		}
	}

	public void GainMana(float amount)
	{
        if(m_CurrentHealth < m_MaxMana)
        {
            m_CurrentHealth += amount;
            SetHealthUI();
        }
        else
        {
            m_CurrentHealth = 100.0f;
        }
    }

    public void LoseMana(float amount)
    {
        if (m_CurrentHealth > 0.0f)
        {
            m_CurrentHealth -= amount;
            SetHealthUI();
        }
        else
        {
            m_CurrentHealth = 0.0f;
            if (!m_Dead)
            {
                OnDeath();
            }
        }
    }

    public void UseMana()
    {
        m_CurrentHealth -= 10.0f;
        SetHealthUI();
    }

    public float GetMana()
    {
        return m_CurrentHealth;
    }

    private void SetHealthUI()
	{
		m_Slider.value = m_CurrentHealth;
        Debug.Log("Mana: " + m_CurrentHealth);
	}

	private void OnDeath()
	{
		m_Dead = true;

        gameOverScript.GameOver();
        Destroy(gameObject);
    }
}
