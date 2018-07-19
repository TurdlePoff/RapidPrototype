using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StandingSideBySide : MonoBehaviour 
{
    private ParticleSystem em;
    private ParticleSystem em2;

    private GameObject[] particleSystems;
    public float ManaGainAmount = 1.0f;
	private Mana manaScript;
    private bool isTogether = false;

	private int nextUpdate = 1;

	void Start()
	{
        particleSystems = GameObject.FindGameObjectsWithTag("PS");
        em = particleSystems[0].GetComponent<ParticleSystem>();
        em2 = particleSystems[1].GetComponent<ParticleSystem>();

        manaScript = GetComponentInParent<Mana> ();
		if (null == manaScript) {
			Debug.Log ("Can't find mana script");
		}
	}

    void Update()
    {
        if(!isTogether && (Time.time >= nextUpdate))
        {
            manaScript.LoseMana();
            Debug.Log("Mana Decrease");
            nextUpdate = Mathf.FloorToInt (Time.time) + 1;
        }
    }

	void OnTriggerStay(Collider other)
	{
		if (other.tag == "Player" && (Time.time >= nextUpdate)) 
		{
            //One issue = BOTH players gain and lose mana when function is triggered
			if (manaScript != null) {
				manaScript.GainMana (ManaGainAmount);
				Debug.Log ("ManaIncrease");
                isTogether = true;
                em.Play();
                em2.Play();
            } else {
				Debug.Log ("Mana Null");
			}

			nextUpdate = Mathf.FloorToInt (Time.time) + 1;
		}
	}

    void OnTriggerExit()
    {
        isTogether = false;
        //var emission = em.emission;
        //emission.enabled = false;
    }
}
