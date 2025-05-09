using UnityEngine;
using System.Collections;

public class QueenBeeBottomAnimation : MonoBehaviour
{
    private bool hasAttacked = false;
    public float wiggleSpeed = 0.7f;
    public float wiggleAngle = 2f;
    private float wiggleTime;
    public ParticleSystem honeyBlastAttack;
    public GameObject honeyGlobPrefab;
    public Transform spawnPoint;
    public float spreadAngle = 30f;
    private float attackStartTime = 0f;
    private bool positionChanged = false;
    float fireTimer;

    private QueenBeebehaviour queenBeebehaviour;
    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        queenBeebehaviour = FindFirstObjectByType<QueenBeebehaviour>();
    }

    void FixedUpdate()
    {
        if (queenBeebehaviour.isDying == true)
        {
            hasAttacked = true;
        }
            if (queenBeebehaviour != null && queenBeebehaviour.state == "HoneyAttack" && !hasAttacked && queenBeebehaviour.isDying == false)
        {
            if (!positionChanged)
            {
                attackStartTime = Time.time;
                positionChanged = true;
            }

            if (!hasAttacked && fireTimer <= 0)
            {
                PerformHoneyAttack();
                hasAttacked = true;
            }
            else if (fireTimer > 0)
            {
                fireTimer -= Time.fixedDeltaTime;
            }
        }   else if (queenBeebehaviour.state == "EnragedHoneyAttack" && queenBeebehaviour.isDying == false)
        {
            if (!positionChanged)
            {
                attackStartTime = Time.time;
                positionChanged = true;
            }

            if (!hasAttacked && fireTimer <= 0)
            {
                EnragedHoneyAttack();
                hasAttacked = true;
            }
            else if (fireTimer > 0)
            {
                fireTimer -= Time.fixedDeltaTime;
            }
        }   else if (queenBeebehaviour.state != "HoneyAttack" && queenBeebehaviour.state != "EnragedHoneyAttack")
        {
            hasAttacked = false;
        }

        if (queenBeebehaviour != null && queenBeebehaviour.state == "Idle")
        {
            wiggleTime += Time.deltaTime * wiggleSpeed;

            float angle = Mathf.Sin(wiggleTime) * wiggleAngle;

            transform.localRotation = Quaternion.Euler(0, 0, angle);

            transform.localPosition = new Vector3(7.37f, 5.12f, 0);
        }
        if (queenBeebehaviour != null && queenBeebehaviour.state == "EnragedIdle")
        {
            wiggleTime += Time.deltaTime * 1.1f;

            float angle = Mathf.Sin(wiggleTime) * wiggleAngle;

            transform.localRotation = Quaternion.Euler(0, 0, angle);

            transform.localPosition = new Vector3(7.37f, 5.12f, 0);
        }
        if (queenBeebehaviour != null && queenBeebehaviour.state == "Summoning")
        {
            wiggleTime += Time.deltaTime * wiggleSpeed;

            float angle = Mathf.Sin(wiggleTime) * wiggleAngle;

            transform.localRotation = Quaternion.Euler(0, 0, angle);

            transform.localPosition = new Vector3(7.37f, 5.12f, 0);
        }
        if (queenBeebehaviour != null && queenBeebehaviour.state == "EnragedSummoning")
        {
            wiggleTime += Time.deltaTime * 1.1f;

            float angle = Mathf.Sin(wiggleTime) * wiggleAngle;

            transform.localRotation = Quaternion.Euler(0, 0, angle);

            transform.localPosition = new Vector3(7.37f, 5.12f, 0);
        }
        if (queenBeebehaviour != null && queenBeebehaviour.state == "Laser")
        {
            wiggleTime += Time.deltaTime * wiggleSpeed;

            float angle = Mathf.Sin(wiggleTime) * wiggleAngle;

            transform.localRotation = Quaternion.Euler(0, 0, angle);

            transform.localPosition = new Vector3(7.37f, 5.12f, 0);
        }
        if (queenBeebehaviour != null && queenBeebehaviour.state == "EnragedLaser")
        {
            wiggleTime += Time.deltaTime * 1.1f;

            float angle = Mathf.Sin(wiggleTime) * wiggleAngle;

            transform.localRotation = Quaternion.Euler(0, 0, angle);

            transform.localPosition = new Vector3(7.37f, 5.12f, 0);
        }
        if (queenBeebehaviour != null && queenBeebehaviour.state == "HoneyAttack" && queenBeebehaviour.isDying == false)
        {
            wiggleTime += Time.deltaTime * 80f;

            float angle = Mathf.Sin(wiggleTime) * 0.25f;

            transform.localRotation = Quaternion.Euler(0, 0, angle);

            transform.localPosition = new Vector3(8.5f, 3.89f, 0);
        }
        if (queenBeebehaviour != null && queenBeebehaviour.state == "EnragedHoneyAttack" && queenBeebehaviour.isDying == false)
        {
            wiggleTime += Time.deltaTime * 80f;

            float angle = Mathf.Sin(wiggleTime) * 0.25f;

            transform.localRotation = Quaternion.Euler(0, 0, angle);

            transform.localPosition = new Vector3(8.5f, 3.89f, 0);
        }
    }

    void PerformHoneyAttack()
    {

        if (honeyBlastAttack != null)
        {
            honeyBlastAttack.Play();
        }

        for (int i = 0; i < 5; i++)
        {
            ShootHoneyGlob(i);
        }
    }

    void EnragedHoneyAttack()
    {

        if (honeyBlastAttack != null)
        {
            honeyBlastAttack.Play();
        }

        for (int i = 0; i < 14; i++)
        {
            ShootHoneyGlob(i);
        }
    }

    void ShootHoneyGlob(int index)
        {
            audioSource.Play();
            if (honeyGlobPrefab == null || spawnPoint == null) return;
            GameObject honeyGlob = Instantiate(honeyGlobPrefab, spawnPoint.position, Quaternion.identity);
            Rigidbody rb = honeyGlob.GetComponent<Rigidbody>();

            if (rb != null)
            {
                float angleOffset = ((index - 1) * spreadAngle) / 2f;
            }
        }
    }
