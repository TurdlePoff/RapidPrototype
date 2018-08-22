using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pufferfishAnimation : MonoBehaviour
{
    private Animator anim;

    // Use this for initialization
    void Start ()
    {
        anim = GetComponentInChildren<Animator>();
    }
	
	// Update is called once per frame
	void Update ()
    {

	}

    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            anim.SetBool("Inflated", true);
        }
    }
    
    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            anim.SetBool("Inflated", false);
        }
    }
}
