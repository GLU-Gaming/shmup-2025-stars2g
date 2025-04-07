using UnityEngine;
using UnityEngine.Audio;

public class MothBottomMovement : MonoBehaviour
{
    public float wiggleSpeed = 0.7f;
    public float wiggleAngle = 2f;
    private float wiggleTime;
    public float spreadAngle = 25f;
    public ParticleSystem silkBlastAttack;
    public GameObject SilkblobPrefab;
    public Transform spawnPoint;
    private AudioSource audioSource;
    private Moth moth;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        moth = GetComponentInParent<Moth>();
    }

    void FixedUpdate()
    {
        if (moth == null)
        {
            Debug.LogWarning("Moth reference is null!");
            return;
        }

        Debug.Log("Current State: " + moth.currentState);

        if (moth.currentState == Moth.MothState.Idle)
        {
            // Debug here too
            Debug.Log("Idle movement happening.");
            wiggleTime += Time.deltaTime * wiggleSpeed;
            float angle = Mathf.Sin(wiggleTime) * 2f;
            transform.localRotation = Quaternion.Euler(0, 0, angle);
            transform.localPosition = new Vector3(0f, 0f, 0);
        }
        else if (moth.currentState == Moth.MothState.SilkAttack)
        {
            Debug.Log("SilkAttack movement happening.");
            transform.localRotation = Quaternion.Euler(0, 0, 34.69f);
            transform.localPosition = new Vector3(-0.36f, 0.7f, 0);
        }
    }


    public void performSilkAttack()
    {
        if (silkBlastAttack != null)
        {
            silkBlastAttack.Play();
        }

        for (int i = 0; i < Random.Range(2, 4); i++)
        {
            shootSilkblob(0); // still hardcoded, could be -1, 0, or 1 if needed
        }
    }

void shootSilkblob(int index)
    {
        audioSource.Play();
        if (SilkblobPrefab == null || spawnPoint == null) return;

        GameObject Silkblob = Instantiate(SilkblobPrefab, spawnPoint.position, Quaternion.identity);
        Rigidbody rb = Silkblob.GetComponent<Rigidbody>();

        if (rb != null)
        {
            float angleOffset = index * spreadAngle;
            Vector3 shootDirection = Quaternion.Euler(0, angleOffset, 0) * transform.forward;
            rb.linearVelocity = shootDirection * 0.7f; // 5f is just an example speed
        }
    }

}
