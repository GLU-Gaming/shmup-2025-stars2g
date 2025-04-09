using System.Collections;
using UnityEngine;
using DG.Tweening;

public class QueenBeeChargeLaser : MonoBehaviour
{
    private QueenBeebehaviour queenBeebehaviour;
    private AudioSource chargeAudioSource;
    private AudioSource blastAudioSource;
    public GameObject laserPrefab;
    public GameObject laserIndicator;
    public GameObject laserPrefab2;
    public GameObject laserIndicator2;
    public GameObject laserPrefab3;
    public GameObject laserIndicator3;
    private CapsuleCollider laserCollider;
    private CapsuleCollider laserCollider2;
    private CapsuleCollider laserCollider3;
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
            laserCollider2 = laserPrefab2.GetComponent<CapsuleCollider>();
            if (laserCollider2 != null)
            {
                laserCollider2.enabled = false; // Start disabled
            }
            laserCollider3 = laserPrefab3.GetComponent<CapsuleCollider>();
            if (laserCollider3 != null)
            {
                laserCollider3.enabled = false; // Start disabled
            }
        }
    }

    void FixedUpdate()
    {
        if (queenBeebehaviour.isDying == true)
        {
            hasAttacked = true;
        }
            if (queenBeebehaviour.state == "Laser" && !hasAttacked && queenBeebehaviour.isDying == false)
        {
            fireTimer -= Time.fixedDeltaTime;
            StartCoroutine(LaserIndicator());

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
        if (queenBeebehaviour.state == "EnragedLaser" && !hasAttacked && queenBeebehaviour.isDying == false)
        {
            fireTimer -= Time.fixedDeltaTime;
            StartCoroutine(EnragedLaserIndicator());

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
                StartCoroutine(EnragedFireLaser());
                hasAttacked = true; // Prevent multiple shots
            }
        }
        else if (queenBeebehaviour.state != "Laser" && queenBeebehaviour.state != "EnragedLaser" && queenBeebehaviour.isDying == false)
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
        laserPrefab.transform.DOScale(new Vector3(0.025f, 0.2f, 0.005f), .1f);

        if (blastAudioSource != null)
            blastAudioSource.Play();
        chargeAudioSource.Stop();

        yield return new WaitForSeconds(1.4f);

        laserPrefab.transform.DOScale(new Vector3(0, 0.2f, 0), .5f);

        yield return new WaitForSeconds(0.3f);

        if (laserCollider != null)
            laserCollider.enabled = false;

        yield return new WaitForSeconds(0.2f);
        laserPrefab.SetActive(false);
    }
    IEnumerator EnragedFireLaser()
    {
        laserPrefab.SetActive(true);
        laserPrefab2.SetActive(true);
        laserPrefab3.SetActive(true);

        if (laserCollider2 != null)
            laserCollider2.enabled = true;
            laserCollider3.enabled = true;

        laserPrefab2.transform.localScale = new Vector3(0, 0.2f, 0);
        laserPrefab2.transform.DOScale(new Vector3(0.025f, 0.2f, 0.005f), .3f);
        laserPrefab3.transform.localScale = new Vector3(0, 0.2f, 0);
        laserPrefab3.transform.DOScale(new Vector3(0.025f, 0.2f, 0.005f), .3f);
        if (blastAudioSource != null)
            blastAudioSource.Play();

        yield return new WaitForSeconds(1.4f);
        laserPrefab2.transform.DOScale(new Vector3(0, 0.2f, 0), .3f);
        laserPrefab3.transform.DOScale(new Vector3(0, 0.2f, 0), .3f);

        laserPrefab.transform.DOScale(new Vector3(0.025f, 0.2f, 0.005f), .3f);
        laserCollider.enabled = true;
        if (blastAudioSource != null)
            blastAudioSource.Play();
        if (laserCollider2 != null)
            laserCollider2.enabled = false;
            laserCollider3.enabled = false;
        laserPrefab2.SetActive(false);
        laserPrefab3.SetActive(false);

        yield return new WaitForSeconds(1.4f);
        laserPrefab.transform.DOScale(new Vector3(0f, 0.2f, 0f), .3f);

        yield return new WaitForSeconds(0.3f);
        if (laserCollider != null)
            laserCollider.enabled = false;
            laserPrefab.SetActive(false);

    }

    IEnumerator LaserIndicator()
    {
        laserIndicator.SetActive(true);
        laserIndicator.transform.localScale = new Vector3(0, 0.2f, 0);
        laserIndicator.transform.DOScale(new Vector3(0.002f, 0.2f, 0.005f), .5f);
        yield return new WaitForSeconds(2.4f);
        laserIndicator.transform.DOScale(new Vector3(0, 0.2f, 0), .5f);
        yield return new WaitForSeconds(0.3f);
        laserIndicator.SetActive(false);
    }
    IEnumerator EnragedLaserIndicator()
    {

        laserIndicator2.SetActive(true);
        laserIndicator3.SetActive(true);

        laserIndicator.transform.localScale = new Vector3(0, 0.2f, 0);
        laserIndicator2.transform.localScale = new Vector3(0, 0.2f, 0);
        laserIndicator2.transform.DOScale(new Vector3(0.002f, 0.2f, 0.005f), .5f);
        laserIndicator3.transform.localScale = new Vector3(0, 0.2f, 0);
        laserIndicator3.transform.DOScale(new Vector3(0.002f, 0.2f, 0.005f), .5f);

        yield return new WaitForSeconds(2.4f);
        laserIndicator.SetActive(true);
        laserIndicator.transform.DOScale(new Vector3(0.002f, 0.2f, 0.005f), .5f);

        laserIndicator2.transform.DOScale(new Vector3(0, 0.2f, 0), .5f);
        laserIndicator3.transform.DOScale(new Vector3(0, 0.2f, 0), .5f);

        yield return new WaitForSeconds(1.4f);
        laserIndicator2.SetActive(false);
        laserIndicator3.SetActive(false);
        laserIndicator.transform.DOScale(new Vector3(0, 0.2f, 0), .5f);
        
        yield return new WaitForSeconds(0.3f);
        laserIndicator.SetActive(false);
    }
}
