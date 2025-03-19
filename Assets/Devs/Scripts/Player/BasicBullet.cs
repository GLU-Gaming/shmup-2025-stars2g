using UnityEngine;


[RequireComponent(typeof(Rigidbody))]
public class BasicBullet : MonoBehaviour
{
    public Vector3 direction;
    public float speed;

    Rigidbody rb;

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
}
