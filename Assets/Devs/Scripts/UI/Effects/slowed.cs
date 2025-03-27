using TMPro;
using UnityEngine;
using System.Collections;

public class slowed : MonoBehaviour
{
    [SerializeField] TMP_Text effectText;
    [SerializeField] float effectDuration = 5f;

    PlayerMovement Plr;

    float Timer;
    float ticks;

    private string originalText = "!HONEYBOUND";

    private void Start()
    {
        effectText = GetComponent<TMP_Text>();
        effectText.text = originalText;
        Timer = effectDuration;
        Plr = GameObject.FindFirstObjectByType<PlayerMovement>();
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
