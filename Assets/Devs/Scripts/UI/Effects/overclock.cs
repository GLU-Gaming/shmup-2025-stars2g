using TMPro;
using UnityEngine;
using System.Collections;

public class overclock : MonoBehaviour
{
    [SerializeField] TMP_Text effectText;
    [SerializeField] float effectDuration = 5f;

    PlayerAttack playerAttack;

    public float Timer;
    float ticks;

    private string originalText = "!OVERCLOCK";

    private void Start()
    {
        //PERFORM PRECHECK TO MAKE SURE NOTHING STACKS
        var existingOverclock = GameObject.FindFirstObjectByType<overclock>();
        int tries = 10;
        while (existingOverclock != null && existingOverclock.gameObject == this.gameObject && tries > 0)
        {
            existingOverclock = GameObject.FindFirstObjectByType<overclock>();
            tries -= 1;
        }
        if (existingOverclock != null)
        {
            existingOverclock.Timer = effectDuration;
            Destroy(gameObject);
        }
        effectText = GetComponent<TMP_Text>();
        effectText.text = originalText;
        Timer = effectDuration;
        playerAttack = GameObject.FindFirstObjectByType<PlayerAttack>();
        OldFirerate = playerAttack.fireRate;
        transform.parent = GameObject.Find("EffectList").transform;
        transform.localScale = Vector3.one;
    }

    float OldFirerate;

    private void Update()
    {
        effectText.text = originalText + " " + Timer.ToString("F2");
        if (Timer <= 0)
        {
            playerAttack.fireRate = OldFirerate;
            Destroy(gameObject);
        }
        else
        {
            playerAttack.fireRate = OldFirerate * 2;
            Timer -= Time.deltaTime;
        }
    }
}
