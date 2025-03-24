using UnityEngine;

public class ContactDamage : MonoBehaviour
{
    [SerializeField] bool isContinuous = false; // Allows for continuous contact damage, like on a laser.
    [SerializeField] float damageToDeal = 10;

    PlayerHealth playerHealth;


    bool hit = false;
    float coolDownTime = 0f;

    private void Start()
    {
        playerHealth = GameObject.FindFirstObjectByType<PlayerHealth>();
    }

    private void Update()
    {
        if (hit && coolDownTime <= 0)
        {
            playerHealth.DamagePlayer(damageToDeal);
            coolDownTime = 0.3f;
        }
        if (coolDownTime > 0)
        {
            coolDownTime -= Time.deltaTime;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (!isContinuous)
            {
                playerHealth.DamagePlayer(damageToDeal); // Since the player is not directly linked to the health.
            }
            else
            {
                hit = true;
                print("Hitting");
            }
        }
    }

    private void OnTriggerExit(Collider other) // Only applied to continuous contact damage
    {
        if (other.CompareTag("Player"))
        {
            hit = false;
            print("I |I II I_");
        }
    }
}
