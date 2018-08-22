using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalFlock : MonoBehaviour {

    public GameObject[] fishPrefabs;
    public int ChanceOfPuffer = 20;
    public static float FishBarrierX = 100f;
    public static float FishBarrierY = 7.5f;
    public static float FishBarrierZ = 100f;
    public static int numberFish = 200;
    public static int numberOfGoals = 20;

    public static GameObject[] allFish = new GameObject[numberFish];

    public static Vector3[] goalPosVec = new Vector3[numberOfGoals];// = Vector3.zero;
    public static Vector3 goalPos;

    public static Vector3 globalFlockCentre;

    // Use this for initialization
    void Start ()
    {
        globalFlockCentre = new Vector3(transform.position.x, transform.position.y, transform.position.z);

        for (int i = 0; i < numberFish; ++i)
        {
            Vector3 pos = new Vector3(Random.Range(transform.position.x - FishBarrierX, transform.position.x + FishBarrierX),
                Random.Range(transform.position.y - FishBarrierY, transform.position.y + FishBarrierY),
                Random.Range(transform.position.z- FishBarrierZ, transform.position.z + FishBarrierZ));

            int fishChosen = 0;
            float fRandomNumber = Random.Range(0f, 100f);

            if(fRandomNumber < ChanceOfPuffer)
            {
                fishChosen = 1;
            }
            allFish[i] = (GameObject)Instantiate(fishPrefabs[fishChosen], pos, Quaternion.identity);
        }

        goalPos = new Vector3(transform.position.x, transform.position.y, transform.position.z);

    }
	
	// Update is called once per frame
	void Update ()
    {
        for (int i = 0; i < numberOfGoals; ++i)
        {
            if (Random.Range(0, 10000) < 50)
            {
                goalPosVec[i] = new Vector3(Random.Range(transform.position.x - FishBarrierX, transform.position.x + FishBarrierX),
                    Random.Range(transform.position.y - FishBarrierY, transform.position.y + FishBarrierY),
                    Random.Range(transform.position.z - FishBarrierZ, transform.position.z + FishBarrierZ));
            }
        }

        if (Random.Range(0, 10000) < 50)
        {
            goalPos = new Vector3(Random.Range(transform.position.x - FishBarrierX, transform.position.x + FishBarrierX),
                Random.Range(transform.position.y - FishBarrierY, transform.position.y + FishBarrierY),
                Random.Range(transform.position.z - FishBarrierZ, transform.position.z + FishBarrierZ));
        }
    }
}
