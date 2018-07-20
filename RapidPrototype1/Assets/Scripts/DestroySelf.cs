using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroySelf : MonoBehaviour {

    public int time = 3;

    private float spawnTime;

    private void Start()
    {
        spawnTime = Time.time;
    }

    // Update is called once per frame
    void Update () {
		if(spawnTime + time < Time.time)
        {
            Destroy(gameObject);
        }
	}
}
