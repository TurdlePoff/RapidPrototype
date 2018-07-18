using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Mana : MonoBehaviour 
{
	public float m_StartingMana = 100f;
	public Slider m_Slider;

	private float m_CurrentHealth;
	private bool m_Dead;

	private void OnEnable()
	{
		m_CurrentHealth = m_StartingMana;
		m_Dead = false;

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
		m_CurrentHealth += amount;
		SetHealthUI ();
	}

	private void SetHealthUI()
	{
		m_Slider.value = m_CurrentHealth;
	}

	private void OnDeath()
	{
		m_Dead = true;

		gameObject.SetActive (false);
	}
}
