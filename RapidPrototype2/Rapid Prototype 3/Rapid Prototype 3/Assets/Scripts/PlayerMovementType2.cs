using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementType2 : MonoBehaviour
{
    public float defaultSpeed = 20.0f;
    public float speed = 10f;
    public float turnSpeed = 50.0f;

    public float maxHeight = 10f;
    public float minHeight = -20f;
    public float heightIncrease = 0.5f;

    public float fCurrentVelocity = 1f;

    public ParticleSystem particles;

    public AudioSource wind;
    public AudioSource splash;
    public AudioSource exitSplash;

    private bool bPlayedPaticles = false;

    private Animator anim;

    public Camera cam;
    public GameObject water;
    public Color fogColorVal = new Color(0.17f, 0.22f, 0.43f, 1.0f);
    public float fogDensity = 0.01f;

    private bool CanMove = true;
    private Rigidbody rb;

    private Camera myCamera;
    private Transform myCameraTransform;
    
    private ParticleSystem[] ps;

    bool bInBarrier;

    // public float tiltSpeed = 50f;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        anim = GetComponentInChildren<Animator>();

        myCamera = GetComponentInChildren<Camera>();
        myCameraTransform = myCamera.transform;
        
        //bubblePS.Stop();
        ps = GetComponentsInChildren<ParticleSystem>();
        foreach (ParticleSystem x in ps)
        {
            x.Stop();
        }

        bInBarrier = true;
    }

    void FixedUpdate()
    {
        myCamera.transform.LookAt(transform);
        if (CanMove)
        {
            //anim.SetBool("TurningLeft", false);
            //anim.SetBool("TurningRight", false);
            anim.SetBool("FlyingLeft", false);
            anim.SetBool("FlyingRight", false);

            if (transform.position.y < -15.0f)
            {
                rb.transform.Translate(Vector3.forward * -15f * Time.deltaTime);
                // anim.SetBool("InWater", true);
                anim.SetBool("Swimming", true);

                if (Input.GetKey(KeyCode.LeftShift))
                {
                    fCurrentVelocity = Mathf.Clamp(fCurrentVelocity - 1.0f * Time.deltaTime, 1, 30);
                }
                else
                {
                    fCurrentVelocity = Mathf.Clamp(fCurrentVelocity - 1.5f * Time.deltaTime, 1, 30);
                }
            }
            else
            {
                //anim.SetBool("InWater", false);
                anim.SetBool("Swimming", false);
                if(bPlayedPaticles)
                {
                    exitSplash.Play();
                    particles.Play();
                    bPlayedPaticles = false;
                }
            }

            if (Input.GetKey(KeyCode.W))
            {
                if (transform.localEulerAngles.x > 280f || transform.localEulerAngles.x < 80f)
                {
                    if (transform.position.y < -15.0f)
                    {
                        Vector3 lookPos = transform.forward;
                        lookPos.y = 5.0f;
                        var rotation = Quaternion.LookRotation(lookPos);
                        rb.transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime);

                        rb.transform.Translate(Vector3.forward * (speed - 5f) * Time.deltaTime * 0.25f);
                        //transform.Translate(Vector3.up * defaultSpeed * Time.deltaTime);
                    }
                    else
                    {
                        //anim.SetBool("FlyingUp", true);
                        Vector3 lookPos = transform.forward;
                        lookPos.y = 5.0f;
                        var rotation = Quaternion.LookRotation(lookPos);
                        rb.transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime);

                        rb.transform.Translate(Vector3.forward * speed * Time.deltaTime * 0.25f);
                        //transform.Translate(Vector3.up * defaultSpeed * Time.deltaTime);
                    }
                }
            }
            if (Input.GetKey(KeyCode.S))
            {
                if (transform.localEulerAngles.x < 60f || transform.localEulerAngles.x > 225f)
                {
                    Vector3 lookPos = transform.forward;
                    lookPos.y = -5.0f;
                    var rotation = Quaternion.LookRotation(lookPos);
                    rb.transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime);

                    rb.transform.Translate(Vector3.forward * speed * Time.deltaTime * 0.25f);
                    //transform.Translate(Vector3.up * defaultSpeed * Time.deltaTime);
                }
            }
            
            /* Diving */
            if (transform.position.y > -15f && transform.localEulerAngles.x < 65f && transform.localEulerAngles.x > 25f)
            {
                anim.SetBool("FlyingDown", true);
                anim.SetBool("FlyingUp", false);
                if (Input.GetKey(KeyCode.LeftShift))
                {
                    fCurrentVelocity += 0.5f * Time.deltaTime;
                }
                else
                {
                    fCurrentVelocity += 0.25f * Time.deltaTime;
                }
            }
            /* Flying Upwards */
            else if (transform.position.y > -15f && transform.localEulerAngles.x > 275f && transform.localEulerAngles.x < 300f)
            {
                anim.SetBool("FlyingUp", true);
                anim.SetBool("FlyingDown", false);
                if (Input.GetKey(KeyCode.LeftShift))
                {
                    fCurrentVelocity = Mathf.Clamp(fCurrentVelocity - 0.1f * Time.deltaTime, 1, 30);
                }
                else
                {
                    fCurrentVelocity = Mathf.Clamp(fCurrentVelocity - 0.25f * Time.deltaTime, 1, 30);
                }
            }
            /* Gliding */
            else if (transform.position.y > -15f && !Input.GetKey(KeyCode.LeftShift))
            {
                anim.SetTrigger("Fly");
                anim.SetBool("FlyingDown", false);
                anim.SetBool("FlyingUp", false);
            }
            else
            {
                anim.SetBool("FlyingDown", false);
                anim.SetBool("FlyingUp", false);
            }

            if (Input.GetKey(KeyCode.A))
            {
                rb.transform.Rotate(Vector3.up, -turnSpeed * Time.deltaTime * 2f, Space.World);
                if (transform.position.y > -15f)
                {
                    // anim.SetBool("TurningLeft", true);
                    anim.SetBool("FlyingLeft", true);
                }
            }
            if (Input.GetKey(KeyCode.D))
            {
                rb.transform.Rotate(Vector3.up, turnSpeed * Time.deltaTime * 2f, Space.World);
                if (transform.position.y > -15f)
                {
                    //anim.SetBool("TurningRight", true);
                    anim.SetBool("FlyingRight", true);
                }
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

                
            }
            if (Input.GetKey(KeyCode.LeftShift))
            {
                //anim.SetBool("FlyingUp", true);
                anim.SetBool("Fast", true);
                if (rb.transform.position.y < -15.0f)
                {
                    rb.transform.Translate(Vector3.forward * defaultSpeed * Time.deltaTime * 0.25f);
                    //wind.Play();
                }
                else
                {
                    rb.transform.Translate(Vector3.forward * defaultSpeed * Time.deltaTime);
                }
            }
            else
            {
                anim.SetBool("Fast", false);
                //wind.Stop();
            }

            rb.transform.Translate(Vector3.forward * defaultSpeed * Time.deltaTime * fCurrentVelocity);
           // Debug.Log("fCurrentVelocity = " + fCurrentVelocity);

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
            Vector3 lookPos = Vector3.zero - rb.transform.position;
            lookPos.y = 0.0f;
            //var rotation = Quaternion.LookRotation(lookPos);
            //rb.transform.rotation = Quaternion.Slerp(rb.transform.rotation, rotation, Time.deltaTime);

            //rb.transform.Translate(Vector3.forward * defaultSpeed * Time.deltaTime * 0.25f);
            //rb.transform.Translate(Vector3.up * defaultSpeed * Time.deltaTime);

            Vector3 newDir = Vector3.RotateTowards(rb.transform.forward, lookPos, defaultSpeed * 0.25f * Time.deltaTime, 0.0f);
            rb.transform.Translate(Vector3.forward * defaultSpeed * Time.deltaTime * 0.25f);
            if (bInBarrier)
            {
                rb.transform.Translate(Vector3.up * defaultSpeed * Time.deltaTime);
            }

            // Move our position a step closer to the target.
            transform.rotation = Quaternion.LookRotation(newDir);
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

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            foreach (ParticleSystem x in ps)
            {
                if (x.tag == "WindParticles" && !x.isPlaying)
                {
                    x.Play();
                }
            }
            wind.Play();
        }

        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            foreach (ParticleSystem x in ps)
            {
                if (x.tag == "WindParticles" && x.isPlaying)
                {
                    x.Stop();
                }
            }

            wind.Stop();
        }

        if (-14.65f >= transform.position.y && !bPlayedPaticles)
        {
            particles.Play();
            splash.Play();
            bPlayedPaticles = true;
        }

        if (Input.GetKeyUp(KeyCode.Escape))
        {
            Application.Quit();
            Debug.Log("Quit");
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Terrain")
        {
            CanMove = false;
        }
        if(other.tag == "Barrier")
        {
            CanMove = true;
            bInBarrier = true;
        }
    }
    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Terrain")
        {
            CanMove = true;
        }
        if (other.tag == "Barrier")
        {
            CanMove = false;
            bInBarrier = false;
        }
    }
}
