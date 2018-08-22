using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public float defaultSpeed = 20.0f;
    public float speed = 10f;
    public float turnSpeed = 50.0f;

    public float maxHeight = 10f;
    public float minHeight = -20f;
    public float heightIncrease = 0.5f;

    private Animator anim;

    public Camera cam;
    public GameObject water;
    public Color fogColorVal = new Color(0.17f, 0.22f, 0.43f, 1.0f);
    public float fogDensity = 0.01f;

    private bool CanMove = true;
    private Rigidbody rb;

    private Camera myCamera;
    
    private ParticleSystem[] ps;

    // public float tiltSpeed = 50f;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        anim = GetComponentInChildren<Animator>();

        myCamera = GetComponentInChildren<Camera>();
        //myCameraTransform = myCamera.transform;
        
        //bubblePS.Stop();
        ps = GetComponentsInChildren<ParticleSystem>();
        foreach (ParticleSystem x in ps)
        {
            x.Stop();
        }
    }

    void FixedUpdate()
    {
        myCamera.transform.LookAt(transform);
        if (CanMove)
        {
            anim.SetBool("FlyingUp", false);
            anim.SetBool("FlyingDown", false);

            if (transform.position.y < -15.0f)
            {
                rb.transform.Translate(Vector3.forward * -15f * Time.deltaTime);
            }

            if (Input.GetKey(KeyCode.W))
            {
                if (transform.position.y < -15.0f)
                {
                    rb.transform.Translate(Vector3.forward * (speed - 5f) * Time.deltaTime);
                }
                else
                {
                    rb.transform.Translate(Vector3.forward * speed * Time.deltaTime);
                }
            }
            if (Input.GetKey(KeyCode.S))
            {
                rb.transform.Translate(Vector3.forward * -speed * Time.deltaTime);
            }
            if (Input.GetKey(KeyCode.A))
            {
                rb.transform.Rotate(Vector3.up, -turnSpeed * Time.deltaTime * 2f);
                //if (transform.rotation.z <= 0.1f)
                //{
                //      //Clamp rotation so it doesn't go over
                //    transform.Rotate(Vector3.forward * Time.deltaTime * tiltSpeed, Space.Self);
                //}
            }
            if (Input.GetKey(KeyCode.D))
            {
                rb.transform.Rotate(Vector3.up, turnSpeed * Time.deltaTime * 2f);
                //if (transform.rotation.z >= -0.1f)
                //{
                //      //Clamp rotation so it doesn't go over
                //    transform.Rotate(Vector3.back * Time.deltaTime * tiltSpeed, Space.Self);
                //}
            }

            if (!Input.GetKey(KeyCode.Space) && !Input.GetKey(KeyCode.LeftShift))
            {
                if (myCamera.transform.localEulerAngles.x > 310f || myCamera.transform.localEulerAngles.x < 25f)
                {
                    myCamera.transform.Translate(Vector3.up * Time.deltaTime);
                }
                else if (myCamera.transform.localEulerAngles.x > 30f)
                {
                    myCamera.transform.Translate(Vector3.down * Time.deltaTime);
                }
            }

            if (Input.GetKey(KeyCode.Space))
            {
                anim.SetBool("FlyingUp", true);
                rb.transform.Translate(Vector3.up * heightIncrease * Time.deltaTime);

                if (myCamera.transform.localEulerAngles.x > 315f || myCamera.transform.localEulerAngles.x < 80f)
                {
                    myCamera.transform.Translate(Vector3.down * Time.deltaTime);
                }
            }
            if (Input.GetKey(KeyCode.LeftShift))
            {
                anim.SetBool("FlyingDown", true);

                if (transform.position.y < -15.0f)
                {
                    rb.transform.Translate(Vector3.up * -(heightIncrease * 0.5f) * Time.deltaTime);
                }
                else
                {
                    rb.transform.Translate(Vector3.up * -heightIncrease * Time.deltaTime);
                }

                if (myCamera.transform.localEulerAngles.x < 60f || myCamera.transform.localEulerAngles.x > 225f)
                {
                    myCamera.transform.Translate(Vector3.up * Time.deltaTime);
                }
            }

            transform.Translate(Vector3.forward * defaultSpeed * Time.deltaTime);

            if (cam.transform.position.y < water.transform.position.y)
            {
                RenderSettings.fogColor = new Color(fogColorVal.r, fogColorVal.g, fogColorVal.b, fogColorVal.a);
                RenderSettings.fogDensity = fogDensity;

                RenderSettings.fog = true;
            }
            else
            {
                RenderSettings.fog = false;
            }
        }
        else
        {
            Vector3 lookPos = Vector3.zero - transform.position;
            lookPos.y = 0.0f;
            var rotation = Quaternion.LookRotation(lookPos);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime);

            transform.Translate(Vector3.forward * defaultSpeed * Time.deltaTime * 0.25f);
            transform.Translate(Vector3.up * defaultSpeed * Time.deltaTime);
        }
    }

    private void Update()
    {
        if (cam.transform.position.y < water.transform.position.y)
        {
            RenderSettings.fogColor = new Color(fogColorVal.r, fogColorVal.g, fogColorVal.b, fogColorVal.a);
            RenderSettings.fogDensity = fogDensity;

            RenderSettings.fog = true;

            foreach (ParticleSystem x in ps)
            {
                if (x.tag == "BubbleParticles" && !x.isPlaying)
                {
                    x.Play();
                }
            }
        }
        else
        {
            RenderSettings.fog = false;
            foreach (ParticleSystem x in ps)
            {
                if (x.tag == "BubbleParticles" && x.isPlaying)
                {
                    x.Stop();
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.W))
        {
            foreach (ParticleSystem x in ps)
            {
                if (x.tag == "WindParticles" && !x.isPlaying)
                {
                    Debug.Log("Particle: " + x.tag);
                    x.Play();
                }
            }
        }

        if (Input.GetKeyUp(KeyCode.W))
        {
            foreach (ParticleSystem x in ps)
            {
                if (x.tag == "WindParticles" && x.isPlaying)
                {
                    Debug.Log("Particle: " + x.tag);
                    x.Stop();
                }
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Terrain")
        {
            CanMove = false;
        }
    }
    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Terrain")
        {
            CanMove = true;
        }
    }
}
