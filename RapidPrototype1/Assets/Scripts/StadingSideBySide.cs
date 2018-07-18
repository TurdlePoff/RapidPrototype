using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StadingSideBySide : MonoBehaviour 
{
	public float ManaGainAmount = 1.0f;
	public Mana manaScript;

	private int nextUpdate = 1;
    private GameObject ps;
    private ParticleSystem em;


    void Start()
    {
        ps = GameObject.Find("ps");
        em = ps.GetComponent<ParticleSystem>();
       // var emission = em.emission;
       // emission.enabled = false;

        manaScript = GetComponentInParent<Mana> ();
		if (null == manaScript) {
			Debug.Log ("Can't find mana script");
		}
	}

    void Update()
    {
        //var emission = em.emission;
        //emission.enabled = false;
    }

	void OnTriggerStay(Collider other)
	{
        if (other.tag == "Player" && (Time.time >= nextUpdate)) 
		{
			if (manaScript != null) {
				manaScript.GainMana (ManaGainAmount);
				Debug.Log ("ManaIncrease");
			} else {
				Debug.Log ("Mana Null");
			}

			nextUpdate = Mathf.FloorToInt (Time.time) + 1;

            //var emission = em.emission;
           // emission.enabled = true;
            em.Play();
        }
	}

    void OnTriggerExit()
    {
        //var emission = em.emission;
        //emission.enabled = false;
    }
}
