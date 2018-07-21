using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour {

    public float lookRadius = 50.0f;
    public float startingHealth = 20.0f;
    public float damage = 5.0f;
    public int enemyWorth = 10;

    Transform target;

    NavMeshAgent agent;

    private Mana manaScript;
    private GameController scoreScript;
    private float currentHealth;
    private float nextUpdate;

    private bool isDead;
    private bool playerDead;


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
        
        GameObject gameManager = GameObject.FindGameObjectWithTag("GameController");
        scoreScript = gameManager.GetComponent <GameController>();

        currentHealth = startingHealth;

        nextUpdate = 0;
        isDead = false;
        playerDead = false;
    }

    // Update is called once per frame
    void Update() {
        if (!playerDead)
        {
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
        if (other.tag == "Bullet")
        {
            Destroy(other.gameObject);
            TakeDamage(5);
        }
        else if (other.tag == "StaticBullet")
        {
            Destroy(other.gameObject);
            TakeDamage(20);
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            if (nextUpdate < Time.time)
            {
                //Debug.Log("Decrease Mana from Enemy");
                manaScript.LoseMana(damage);
                nextUpdate = Mathf.FloorToInt(Time.time) + 1;
            }
        }
    }

    private void TakeDamage(int amount)
    {
        currentHealth -= amount;

        Debug.Log("Enemy health: " + currentHealth);
        if (currentHealth <= 0 && !isDead)
        {
            isDead = true;
            scoreScript.IncreaseScore(enemyWorth);
            Destroy(gameObject);
        }
    }

    public void PlayersDied()
    {
        playerDead = true;
    }
}
