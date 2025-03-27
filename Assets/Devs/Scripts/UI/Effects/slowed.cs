using TMPro;
using UnityEngine;
using System.Collections;

public class slowed : MonoBehaviour
{
    [SerializeField] TMP_Text effectText;
    [SerializeField] float effectDuration = 5f;

    PlayerMovement Plr;

    public float Timer;
    float ticks;

    private string originalText = "!HONEYBOUND";

    private void Start()
    {
        //PERFORM PRECHECK TO MAKE SURE NOTHING STACKS
        var existingSlow = GameObject.FindFirstObjectByType<slowed>();
        int tries = 10;
        while (existingSlow != null && existingSlow.gameObject == this.gameObject && tries > 0)
        {
            existingSlow = GameObject.FindFirstObjectByType<slowed>();
            tries -= 1;
        }
        if (existingSlow != null)
        {
            existingSlow.Timer = effectDuration;
            Destroy(gameObject);
        }

        effectText = GetComponent<TMP_Text>();
        effectText.text = originalText;
        Timer = effectDuration;
        Plr = GameObject.FindFirstObjectByType<PlayerMovement>();
        transform.parent = GameObject.Find("EffectList").transform;
        transform.localScale = Vector3.one;
    }

    private void Update()
    {
        effectText.text = originalText + " " + Timer.ToString("F2");
        if (Timer <= 0)
        {
            Plr.speedModifier = 1;
            Destroy(gameObject);
        }
        else
        {
            Plr.speedModifier = .6f;
            Timer -= Time.deltaTime;
        }
    }
}
