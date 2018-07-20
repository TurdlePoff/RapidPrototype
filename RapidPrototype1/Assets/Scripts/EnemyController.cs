using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour {

    public float lookRadius = 50.0f;
    public float startingHealth = 20.0f;
    public float damage = 5.0f;

    Transform target;

    NavMeshAgent agent;

    private Mana manaScript;
    private float currentHealth;
    private float nextUpdate;


    // Use this for initialization
    void Start() {

        //playertoTarget = Random.Range(0, 1);
        var possibleTargets = GameObject.FindGameObjectsWithTag("Player");

        target = possibleTargets[Random.Range(0, 2)].transform;

        agent = GetComponent<NavMeshAgent>();
        
        GameObject parentOfPlayers = GameObject.FindGameObjectWithTag("PlayerHolder");
        manaScript = parentOfPlayers.GetComponent<Mana>();
        if (null == manaScript)
        {
            Debug.Log("Can't find mana script - Enemy");
        }

        currentHealth = startingHealth;

        nextUpdate = 0;
    }

    // Update is called once per frame
    void Update() {
        float distance = Vector3.Distance(target.position, transform.position);

        if (distance <= lookRadius)
        {
            agent.SetDestination(target.position);

            if (distance <= agent.stoppingDistance)
            {
                //attack target
                //face target
            }
        }
    }

    void FaceTarget()
    {
        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5.0f);
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, lookRadius);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
           // InvokeRepeating("manaScript.LoseMana(damage)", 0.0f, 1.0f);
            if (nextUpdate < Time.time)
            {
                Debug.Log("Decrease Mana from Enemy");
                manaScript.LoseMana(damage);
                nextUpdate = Mathf.FloorToInt(Time.time) + 1;
            }
        }
        else if(other.tag == "Bullet")
        {
            TakeDamage();
        }
    }

    private void TakeDamage()
    {
        currentHealth -= 5;

        Debug.Log("Enemy health: " + currentHealth);
        if (currentHealth <= 0)
        {
            Destroy(gameObject);
        }
    }
}
