using System.Collections;
using UnityEngine;
using DG.Tweening;

public class QueenBeeChargeLaser : MonoBehaviour
{
    private QueenBeebehaviour queenBeebehaviour;
    private AudioSource audioSource;
    public GameObject laserPrefab;
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
            if (LaserCharge != null && !LaserCharge.isPlaying && hasPlayed == false)
            {
                LaserCharge.Play();
                hasPlayed = true;
            }

            if (audioSource != null && !audioSource.isPlaying && hasPlayed == false)
            {
                audioSource.Play();
                hasPlayed = true;
            }

            fireTimer -= Time.fixedDeltaTime;

            if (!hasAttacked && fireTimer <= 0)
            {
                StartCoroutine(FireLaser());
            }
        }
        else
        {
            fireTimer = 2.4f;
        }
    }

    IEnumerator FireLaser()
    {
        laserPrefab.SetActive(true);
        laserPrefab.transform.localScale = new Vector3(0, 1.45f, 0);
        laserPrefab.transform.DOScale(new Vector3(0.020f, 1.45f, 0.005f), .5f);
        yield return new WaitForSeconds(1.4f);
        laserPrefab.transform.DOScale(new Vector3(0, 1.45f, 0), .5f);
        yield return new WaitForSeconds(1.4f);
        laserPrefab.SetActive(false);
        hasAttacked = false;
    }
}

