using UnityEditorInternal;
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
        if (queenBeebehaviour != null && queenBeebehaviour.state == "HoneyAttack" && !hasAttacked)
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
        }

        else if (queenBeebehaviour.state != "HoneyAttack")
        {
            hasAttacked = false;


        }

        if (queenBeebehaviour != null && queenBeebehaviour.state == "Idle")
        {
            wiggleTime += Time.deltaTime * wiggleSpeed;

            float angle = Mathf.Sin(wiggleTime) * wiggleAngle;

            transform.localRotation = Quaternion.Euler(0, 0, angle);
        }
        if (queenBeebehaviour != null && queenBeebehaviour.state == "Summoning")
        {
            wiggleTime += Time.deltaTime * wiggleSpeed;

            float angle = Mathf.Sin(wiggleTime) * wiggleAngle;

            transform.localRotation = Quaternion.Euler(0, 0, angle);
        }
        if (queenBeebehaviour != null && queenBeebehaviour.state == "HoneyAttack")
        {
            wiggleTime += Time.deltaTime * 80f;

            float angle = Mathf.Sin(wiggleTime) * 0.25f;

            transform.localRotation = Quaternion.Euler(0, 0, angle);
        }
    }

    void PerformHoneyAttack()
    {

        if (honeyBlastAttack != null)
        {
            honeyBlastAttack.Play();
        }

        for (int i = 0; i < 3; i++)
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
