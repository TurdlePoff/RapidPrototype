using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour {

    //public Transform[] player;               // Reference to the player's position.
    //public PlayerHealth playerHealth;      // Reference to the player's health.
    //public EnemyHealth enemyHealth;        // Reference to this enemy's health.
    //public NavMeshAgent nav;               // Reference to the nav mesh agent.


    //void Awake()
    //{
    //    // Set up the references.
    //    player = GameObject.FindGameObjectWithTag("Player").transform;
    //    playerHealth = player.GetComponent();
    //    enemyHealth = GetComponent();
    //    nav = GetComponent();
    //}

    // Use this for initialization
    void Start () {
		
	}

    // Update is called once per frame
    void Update()
    {
        //    // If the enemy and the player have health left...
        //    if (enemyHealth.currentHealth > 0 && playerHealth.currentHealth > 0)
        //    {
        //        // ... set the destination of the nav mesh agent to the player.
        //        nav.SetDestination(player.position);
        //    }
        //    // Otherwise...
        //    else
        //    {
        //        // ... disable the nav mesh agent.
        //        nav.enabled = false;
        //    }
    }
}
