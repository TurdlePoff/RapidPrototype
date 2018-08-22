using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempMove : MonoBehaviour
{
    public float speed = 0.5f;
    public Camera cam;
    public GameObject water;
    public Color fogColorVal = new Color(0.17f, 0.22f, 0.43f, 1.0f);
    public float fogDensity = 0.01f;

    // Use this for initialization
    void Start () {
        RenderSettings.fogColor = new Color(fogColorVal.r, fogColorVal.g, fogColorVal.b, fogColorVal.a);
        RenderSettings.fogDensity = fogDensity;
    }

    // Update is called once per frame
    void Update() {
        if (Input.GetKey("w"))
        {
            transform.position = (new Vector3(transform.position.x, transform.position.y + speed, transform.position.z));
        }

        if (Input.GetKey("s"))
        {
            transform.position = (new Vector3(transform.position.x, transform.position.y - speed, transform.position.z));
        }

        if (cam.transform.position.y < water.transform.position.y)
        {
            RenderSettings.fogColor = new Color(fogColorVal.r, fogColorVal.g, fogColorVal.b, fogColorVal.a);
            RenderSettings.fogDensity = fogDensity;


            RenderFog();
        }
        else
        {
            RevertFog();
        }
    }

    void RenderFog()
    {
        //revertFogState = RenderSettings.fog;
        RenderSettings.fog = true;
    }

    void RevertFog()
    {
        RenderSettings.fog = false;
    }
}
