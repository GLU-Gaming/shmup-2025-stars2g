using UnityEngine;

public class HoneyglobProjectile : MonoBehaviour
{
    public float speed;
    Rigidbody rb;
    public Vector3 direction;
    [SerializeField] float DamageToDeal = 1;
    public float movespeed;
    public float jumppower;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        movespeed = Random.Range(-6f, -20f);
        jumppower = Random.Range(8f, 30f);
        rb.AddForce( new Vector3(movespeed, jumppower, 0f) , ForceMode.Impulse);
    }

    void Update()
    {

        if (transform.position.y < -15)
        {
            Destroy(gameObject, 0.2f);
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
