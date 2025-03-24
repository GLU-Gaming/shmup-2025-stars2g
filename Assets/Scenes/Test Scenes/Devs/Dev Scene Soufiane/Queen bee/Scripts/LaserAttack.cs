using UnityEngine;

public class LaserAttack : MonoBehaviour
{
    public float disappearTime = 1.5f;
    [SerializeField] float DamageToDeal = 35;
    void Start()
    {
        disappearTime -= Time.deltaTime;
    }

    void FixedUpdate()
    {
        if (disappearTime == 0)
        {
            Destroy(gameObject);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GameObject root = other.transform.root.gameObject;
            GameObject healthUI = root.transform.Find("HealthUI").gameObject;
            PlayerHealth playerHealth = healthUI.GetComponent<PlayerHealth>();

            if (playerHealth != null)
            {
                // Apply damage to the player
                playerHealth.DamagePlayer(DamageToDeal);
            }
        }
    }
}
