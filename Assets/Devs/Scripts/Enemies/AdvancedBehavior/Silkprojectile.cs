using UnityEngine;

public class Silkprojectile : MonoBehaviour
{
    public float speed;
    Rigidbody rb;
    public Vector3 direction;
    public float movespeed;
    public float jumppower;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        movespeed = Random.Range(-3f, -10f);
        jumppower = Random.Range(4f, 15f);
        rb.AddForce(new Vector3(movespeed, jumppower, 0f), ForceMode.Impulse);
    }

    void Update()
    {

        if (transform.position.y < -19)
        {
            Destroy(gameObject, 0.2f);
        }
    }
}
