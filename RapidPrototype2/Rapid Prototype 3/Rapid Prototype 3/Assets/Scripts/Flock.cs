using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flock : MonoBehaviour {

    public float speed = 5.0f;
    public float minSpeed = 0.5f;
    public float maxSpeed = 5.0f;

    float rotationSpeed = 4.0f;
    Vector3 averageHeading;
    Vector3 averagePosition;
    float neighbourDistance = 3.0f;

    private int currentGoal = 0;

    bool turning = false;
    
	void Start ()
    {
        speed = Random.Range(minSpeed, maxSpeed);

        currentGoal = Random.Range(0, GlobalFlock.numberOfGoals);
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (Vector3.Distance(transform.position, GlobalFlock.goalPosVec[currentGoal]) >= 15.0f)
        {
            turning = true;
        }
        else
        {
            turning = false;
        }

        if (turning)
        {
            Vector3 direction = GlobalFlock.goalPosVec[currentGoal] - transform.position;
            transform.rotation = Quaternion.Slerp(transform.rotation,
                Quaternion.LookRotation(direction),
                rotationSpeed * Time.deltaTime);

            speed = Random.Range(0.5f, 1.0f);
        }
        else
        {
            if (Random.Range(0f, 5f) < 1.0)
            {
                ApplyRules();
            }
        }

        transform.Translate(0.0f, 0.0f, Time.deltaTime * speed);
	}

    void ApplyRules()
    {
        GameObject[] gos;
        gos = GlobalFlock.allFish;

        Vector3 vCentre = GlobalFlock.goalPosVec[currentGoal];
        Vector3 vAvoid = GlobalFlock.goalPosVec[currentGoal];
        float gSpeed = 0.1f;

        Vector3 goalPos = GlobalFlock.goalPosVec[currentGoal];

        float dist;

        int groupSize = 0;

        foreach(GameObject go in gos)
        {
            if(go != this.gameObject)
            {
                dist = Vector3.Distance(go.transform.position, this.transform.position);
                if(dist <= neighbourDistance)
                {
                    vCentre += go.transform.position;
                    ++groupSize;

                    if(dist < 1.0f)
                    {
                        vAvoid = vAvoid + (this.transform.position - go.transform.position);
                    }

                    Flock anotherFlock = go.GetComponent<Flock>();
                    gSpeed = gSpeed + anotherFlock.speed;
                }
            }
        }

        if(groupSize > 0)
        {
            vCentre = vCentre / groupSize + (goalPos - this.transform.position);
            speed = gSpeed / groupSize;

            Vector3 direction = (vCentre + vAvoid) - transform.position;
            if(direction != Vector3.zero)
            {
                transform.rotation = Quaternion.Slerp(transform.rotation,
                    Quaternion.LookRotation(direction),
                    rotationSpeed * Time.deltaTime);
            }
        }
    }
}
