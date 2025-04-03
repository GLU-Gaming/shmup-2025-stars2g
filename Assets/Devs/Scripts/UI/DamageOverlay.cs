using UnityEngine;
using UnityEngine.UI;

public class DamageOverlay : MonoBehaviour
{
    PlayerHealth health;

    Image damageOverlay;

    private void Start()
    {
        health = GameObject.FindFirstObjectByType<PlayerHealth>();
        damageOverlay = GetComponent<Image>();
    }

    private void Update()
    {
        float healthPercentage = health.maxHealth > 0 ? health.displayedHealth / health.maxHealth : 0; 
        damageOverlay.color = new Color(1,1,1,(1 - healthPercentage));
    }
}
