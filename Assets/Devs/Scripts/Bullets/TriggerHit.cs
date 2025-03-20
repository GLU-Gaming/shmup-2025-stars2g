using UnityEngine;

public class TriggerHit : MonoBehaviour
{
    [SerializeField] float DamageToDeal = 1;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            GameObject root = other.transform.root.gameObject;
            GameObject healthUI = root.transform.Find("HealthUI").gameObject;
            EnemyHealth enemyHealth = healthUI.GetComponent<EnemyHealth>();

            if (enemyHealth != null)
            {
                // Apply damage to the enemy
                enemyHealth.DamageEnemy(DamageToDeal);
            }
            Destroy(gameObject);
        }
    }
}
