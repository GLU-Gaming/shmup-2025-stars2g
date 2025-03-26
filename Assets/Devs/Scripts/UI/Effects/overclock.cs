using TMPro;
using UnityEngine;
using System.Collections;

public class overclock : MonoBehaviour
{
    [SerializeField] TMP_Text effectText;
    [SerializeField] float effectDuration = 5f;

    PlayerAttack playerAttack;

    float Timer;
    float ticks;

    private string originalText = "!OVERCLOCK";

    private void Start()
    {
        effectText = GetComponent<TMP_Text>();
        effectText.text = originalText;
        Timer = effectDuration;
        playerAttack = GameObject.FindFirstObjectByType<PlayerAttack>();
        OldFirerate = playerAttack.fireRate;
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
