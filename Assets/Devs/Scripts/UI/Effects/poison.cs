using TMPro;
using UnityEngine;
using System.Collections;

public class poison : MonoBehaviour
{
    [SerializeField] TMP_Text poisonText;
    [SerializeField] float poisonDuration = 5f;
    [SerializeField] int TickInterval; //after how many ticks the player needs to be damaged
    [SerializeField] float TickDamage;

    PlayerHealth playerHealth;

    public float Timer;
    float ticks;


    //For the text effect
    [SerializeField] float glitchMinInterval = 0.1f;
    [SerializeField] float glitchMaxInterval = 0.5f;
    [SerializeField] float glitchDuration = 0.05f;

    private string originalText = "!IRRADIATED";
    private char[] randomChars = "!@#$%^&*()<>?:;[]{}".ToCharArray(); //Characters that will replace the original text

    private void Start()
    {
        //PERFORM PRECHECK TO MAKE SURE NOTHING STACKS
        var existingPoison = GameObject.FindFirstObjectByType<poison>();
        int tries = 10;
        while (existingPoison != null && existingPoison.gameObject == this.gameObject && tries > 0)
        {
            existingPoison = GameObject.FindFirstObjectByType<poison>();
            tries -= 1;
        }
        if (existingPoison != null)
        {
            existingPoison.Timer = poisonDuration;
            Destroy(gameObject);
        }

        poisonText = GetComponent<TMP_Text>();
        poisonText.text = originalText;
        ticks = TickInterval;
        Timer = poisonDuration;
        playerHealth = GameObject.FindFirstObjectByType<PlayerHealth>();
        transform.parent = GameObject.Find("EffectList").transform;
        transform.localScale = Vector3.one;
    }

    private void Update()
    {
        poisonText.text = originalText + " " + Timer.ToString("F2");
        if (Timer <= 0)
        {
            Destroy(gameObject);
        }
        else
        {
            Timer -= Time.deltaTime;
        }
    }

    private void FixedUpdate()
    {
        if(Timer > 0)
        {
            if(ticks > 0)
            {
                ticks--;
            } else
            {
                ticks = TickInterval;
                playerHealth.DamagePlayer(TickDamage);
            }
        }
    }
}
