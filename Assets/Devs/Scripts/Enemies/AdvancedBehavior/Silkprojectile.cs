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
        movespeed = Random.Range(-6f, -20f);
        jumppower = Random.Range(5f, 20f);
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
