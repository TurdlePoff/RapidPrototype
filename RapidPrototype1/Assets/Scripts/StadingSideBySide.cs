using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StadingSideBySide : MonoBehaviour 
{
	public float ManaGainAmount = 1.0f;
	public Mana manaScript;

	void Start()
	{
		manaScript = GetComponentInParent<Mana> ();
		if (null == manaScript) {
			Debug.Log ("Can't find mana script");
		}
	}

	void OnTriggerStay(Collider other)
	{
		if (other.tag == "Player") 
		{
			if (manaScript != null) {
				manaScript.GainMana (ManaGainAmount);
				Debug.Log ("ManaIncrease");
			} else {
				Debug.Log ("Mana Null");
			}
		}
	}
}
