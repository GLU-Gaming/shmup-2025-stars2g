using System.Collections;
using UnityEngine;
using DG.Tweening;

public class QueenBeeChargeLaser : MonoBehaviour
{
    private QueenBeebehaviour queenBeebehaviour;
    private AudioSource chargeAudioSource;
    private AudioSource blastAudioSource;
    public GameObject laserPrefab;
    private CapsuleCollider laserCollider;
    public ParticleSystem LaserCharge;
    public Transform spawnPoint;
    private bool hasAttacked = false;
    private bool hasPlayed = false;
    private float fireTimer = 2.4f;

    void Start()
    {
        queenBeebehaviour = FindFirstObjectByType<QueenBeebehaviour>();
        AudioSource[] audioSources = GetComponents<AudioSource>();
        if (audioSources.Length >= 2)
        {
            chargeAudioSource = audioSources[0];  // First AudioSource = charge sound
            blastAudioSource = audioSources[1];   // Second AudioSource = blast sound
        }

        // Get the CapsuleCollider component from the laserPrefab
        if (laserPrefab != null)
        {
            laserCollider = laserPrefab.GetComponent<CapsuleCollider>();
            if (laserCollider != null)
            {
                laserCollider.enabled = false; // Start disabled
            }
        }
    }

    void FixedUpdate()
    {
        if (queenBeebehaviour.state == "Laser" && !hasAttacked)
        {
            fireTimer -= Time.fixedDeltaTime;

            if (!hasPlayed)
            {
                hasPlayed = true; // Ensure it plays only once
                if (LaserCharge != null)
                {
                    LaserCharge.Play();
                }
                if (chargeAudioSource != null)
                {
                    chargeAudioSource.Play();
                }
            }

            if (fireTimer <= 0)
            {
                StartCoroutine(FireLaser());
                hasAttacked = true; // Prevent multiple shots
            }
        }
        else if (queenBeebehaviour.state != "Laser")
        {
            // Reset when the state changes
            fireTimer = 2.4f;
            hasPlayed = false;
            hasAttacked = false;
        }
    }

    IEnumerator FireLaser()
    {
        laserPrefab.SetActive(true);

        if (laserCollider != null)
            laserCollider.enabled = true; // Enable the collider

        laserPrefab.transform.localScale = new Vector3(0, 0.2f, 0);
        laserPrefab.transform.DOScale(new Vector3(0.020f, 0.2f, 0.005f), .5f);

        if (blastAudioSource != null)
            blastAudioSource.Play();

        yield return new WaitForSeconds(1.4f);

        laserPrefab.transform.DOScale(new Vector3(0, 0.2f, 0), .5f);

        yield return new WaitForSeconds(5f);

        if (laserCollider != null)
            laserCollider.enabled = false; // Disable the collider

        laserPrefab.SetActive(false);
    }
}
