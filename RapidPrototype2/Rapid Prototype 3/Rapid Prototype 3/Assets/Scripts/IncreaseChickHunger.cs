using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IncreaseChickHunger : MonoBehaviour {

    private TimerandBirdThoughts parentCode;
    bool bAvalibleToIncrease;

	// Use this for initialization
	void Start ()
    {
        bAvalibleToIncrease = true;
        parentCode = GetComponentInParent<TimerandBirdThoughts>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            Debug.Log("Colliding with nest");
            if(bAvalibleToIncrease)
            {
                parentCode.IncreaseHungerBar(GameManager.GetScore());
                GameManager.ClearScore();
                bAvalibleToIncrease = false;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            bAvalibleToIncrease = true;
        }
    }
}
