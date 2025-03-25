using UnityEngine;

public class QueenBeeChargeLaser : MonoBehaviour
{
    private QueenBeebehaviour queenBeebehaviour;
    private AudioSource audioSource;
    public GameObject LaserPrefab;
    public ParticleSystem LaserCharge;
    public Transform spawnPoint;
    private bool hasAttacked = false;
    public bool hasPlayed = false;
    private float fireTimer = 2.4f;

    void Start()
    {
        queenBeebehaviour = FindFirstObjectByType<QueenBeebehaviour>();
        audioSource = GetComponent<AudioSource>();
    }

    void FixedUpdate()
    {
        if (queenBeebehaviour != null && queenBeebehaviour.state == "Laser")
        {
            if (!LaserCharge.isPlaying && hasPlayed == false)
            {
                LaserCharge.Play();
            }

            if (audioSource != null && !audioSource.isPlaying && hasPlayed == false)
            {
                audioSource.Play();
                hasPlayed = true;
            }

            fireTimer -= Time.fixedDeltaTime;

            if (hasAttacked == false && fireTimer <= 0)
            {
                if (LaserPrefab != null && spawnPoint != null)
                {
                    Instantiate(LaserPrefab, spawnPoint.position, Quaternion.identity);
                }

                hasAttacked = true;
            }
        }
        else if (queenBeebehaviour.state != "Laser")
        {
            hasAttacked = false;
            fireTimer = 2.4f;
        }
    }
}
