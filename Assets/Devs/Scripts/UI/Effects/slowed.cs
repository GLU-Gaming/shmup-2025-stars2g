using TMPro;
using UnityEngine;
using System.Collections;

public class slowed : MonoBehaviour
{
    [SerializeField] TMP_Text effectText;
    public float effectDuration = 5f;

    EffectManager effectManager;
    PlayerMovement Plr;

    public float Timer;
    float ticks;

    private string originalText = "!HONEYBOUND";

    private void Start()
    {
        effectText = GetComponent<TMP_Text>();
        effectText.text = originalText;
        Timer = effectDuration;
        Plr = GameObject.FindFirstObjectByType<PlayerMovement>();
        transform.parent = GameObject.Find("EffectList").transform;
        transform.localScale = Vector3.one;
        effectManager = GameObject.FindFirstObjectByType<EffectManager>();
    }

    private void Update()
    {
        effectText.text = originalText + " " + Timer.ToString("F2");
        if (Timer <= 0)
        {
            Plr.speedModifier = 1;
            effectManager.DisableEffect(2);
            Destroy(gameObject);
        }
        else
        {
            Plr.speedModifier = .6f;
            Timer -= Time.deltaTime;
        }
    }
}
