using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerandBirdThoughts : MonoBehaviour
{
    public Text birdThoughts;
    public int iChickHungerDefault = 100;
    public int iHungerDecreaseRate = 2;
    public Slider ChickHunger;
    public int iScoreTimesAmount = 5;
    public Image hungerFlash;
    public Image hungerBorder;

    public float flashSpeed = 5f;
    public Color flashColour;
    public float flashCooldown = 1f;
    public float flashCooldownSpeed = 1f;
    public AudioSource chickCalling;

    private int iCurrentChickHunger;
    private int iCurrentThought;
    private float fTalkCooldown;
	// Use this for initialization
	void Start ()
    {
        iCurrentThought = 0;
        fTalkCooldown = 0;
        iCurrentChickHunger = iChickHungerDefault;

        InvokeRepeating("DecreaseHunger", 1.0f, 1.0f);
        flashCooldown = Time.time + 5f;
    }
	
	// Update is called once per frame
	void Update ()
    {
		if(fTalkCooldown < Time.time)
        {
            switch(iCurrentThought)
            {
                case 0:
                    {
                        birdThoughts.text = "My babies are hungry back home";
                        iCurrentThought += 1;
                        break;
                    }
                case 1:
                    {
                        birdThoughts.text = "I need to get food for them";
                        iCurrentThought += 1;
                        break;
                    }
                case 2:
                    {
                        birdThoughts.text = "Fish is a nice easy option";
                        iCurrentThought += 1;
                        break;
                    }
                case 3:
                    {
                        birdThoughts.text = "Good thing I'm a kingfisher";
                        iCurrentThought += 1;
                        break;
                    }
                case 4:
                    {
                        birdThoughts.text = "";
                        if (fTalkCooldown + 10f < Time.time)
                        {
                            //Next thought
                            iCurrentThought += 1;
                        }
                        break;
                    }
                default:
                    birdThoughts.text = "";
                    break;
            }
            fTalkCooldown = Time.time + 2.5f;
        }

        ChickHunger.value = iCurrentChickHunger;

        chickCalling.volume = Mathf.Clamp(1 - 0.25f - (ChickHunger.value / 100f), 0, 100);
        if (iCurrentChickHunger < 10 && Time.time > flashCooldown)
        {
            // ... set the colour of the damageImage to the flash colour.
            hungerFlash.color = flashColour;
            flashCooldown = Time.time + flashCooldownSpeed;
        }
        // Otherwise...
        else
        {
            // ... transition the colour back to clear.
            hungerFlash.color = Color.Lerp(hungerFlash.color, Color.clear, flashSpeed * Time.deltaTime);
        }
        if (0 >= iCurrentChickHunger)
        {
            GameManager.GameOver();
        }
    }

    private void DecreaseHunger()
    {
        iCurrentChickHunger -= iHungerDecreaseRate;
    }
    public void IncreaseHungerBar(int _iAmount)
    {
        iCurrentChickHunger += (_iAmount * iScoreTimesAmount);
        iCurrentChickHunger = (int)Mathf.Clamp(iCurrentChickHunger, 0.0f, 100.0f);
    }
}
