using TMPro;
using UnityEngine;
using System.Collections;

public class Splitgun : MonoBehaviour
{
    [SerializeField] TMP_Text effectText;
    public float effectDuration = 5f;

    EffectManager effectManager;
    PlayerAttack playerAttack;

    public float Timer;
    float ticks;

    private string originalText = "!SPREADCORE";

    private void Start()
    {
        effectText = GetComponent<TMP_Text>();
        effectText.text = originalText;
        Timer = effectDuration;
        playerAttack = GameObject.FindFirstObjectByType<PlayerAttack>();
        OldFirerate = playerAttack.fireRate;
        transform.parent = GameObject.Find("EffectList").transform;
        transform.localScale = Vector3.one;
        effectManager = GameObject.FindFirstObjectByType<EffectManager>();
    }

    float OldFirerate;

    private void Update()
    {
        effectText.text = originalText + " " + Timer.ToString("F2");
        if (Timer <= 0)
        {
            playerAttack.SwitchBullet(0);
            effectManager.DisableEffect(4);
            Destroy(gameObject);
        }
        else
        {
            playerAttack.SwitchBullet(1);
            Timer -= Time.deltaTime;
        }
    }
}
