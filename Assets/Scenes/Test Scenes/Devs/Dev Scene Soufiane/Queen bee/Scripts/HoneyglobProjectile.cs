using UnityEngine;

public class HoneyglobProjectile : MonoBehaviour
{
    public float speed;
    Rigidbody rb;
    public Vector3 direction;
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

        if (transform.position.y < -19)
        {
            Destroy(gameObject, 0.2f);
        }
    }
}
