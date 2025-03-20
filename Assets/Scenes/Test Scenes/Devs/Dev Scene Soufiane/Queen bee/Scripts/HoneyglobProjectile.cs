using UnityEngine;

public class HoneyglobProjectile : MonoBehaviour
{
    public float speed;
    Rigidbody rb;
    public Vector3 direction;
    [SerializeField] float DamageToDeal = 1;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        rb.linearVelocity = direction * speed;
        if (transform.position.x > 25)
        {
            Destroy(gameObject, 0.2f);
        }

        // Rotate based on angle of movement, only on the Z-axis
        if (direction != Vector3.zero)
        {
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg; //No idea how it works exactly but it does the angling :thumbsup:
            transform.rotation = Quaternion.Euler(0, 0, angle);
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
            Destroy(gameObject);

}
    }
}
