using UnityEngine;
using UnityEngine.Audio;

public class MothBottomMovement : MonoBehaviour
{
    public float wiggleSpeed = 0.7f;
    public float wiggleAngle = 2f;
    private float wiggleTime;
    public float spreadAngle = 30f;
    public ParticleSystem silkBlastAttack;
    public GameObject SilkblobPrefab;
    public Transform spawnPoint;
    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void FixedUpdate()
    {
        wiggleTime += Time.deltaTime * 0.1f;

        float angle = Mathf.Sin(wiggleTime) * 0.25f;

        transform.localRotation = Quaternion.Euler(0, 0, angle);

        transform.localPosition = new Vector3(0f, 0f, 0);

    }
    public void performSilkAttack()
    {
        if (silkBlastAttack != null)
        {
            silkBlastAttack.Play();

            wiggleTime += Time.deltaTime * 34.69f;

            float angle = Mathf.Sin(wiggleTime) * 0.25f;

            transform.localRotation = Quaternion.Euler(0, 0, angle);

            transform.localPosition = new Vector3(-0.36f, 0.7f, 0);
        }

        for (int i = 0; i < 2; i++)
        {
            shootSilkblob(i);
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
            float angleOffset = ((index - 1) * spreadAngle) / 2f;
        }
    }
}
