using UnityEngine;

public class BeeSpawner : MonoBehaviour
{
    public GameObject objectToSpawn; //dependant on bee coding
    float spawnInterval;
    private float lastSpawnTime;
    public float movementInterval = 0.01f;
    private float lastMoveTime;
    private QueenBeebehaviour queenBeebehaviour;
    private QueenBeeHeadAnimation queenBeeHeadAnimation;

    private void Start()
    {
        queenBeeHeadAnimation = FindFirstObjectByType<QueenBeeHeadAnimation>();
        queenBeebehaviour = FindFirstObjectByType<QueenBeebehaviour>();
    }

    void FixedUpdate()
    {
        if (Time.time >= lastMoveTime + movementInterval)
        {
            timeRundown();
            lastMoveTime = Time.time;
        }

        if (Time.time >= lastSpawnTime + spawnInterval && queenBeebehaviour.state == "Summoning")
        {
            SpawnObject();
            lastSpawnTime = 1f;
            lastSpawnTime = Time.time;
        }

        if (Time.time >= lastSpawnTime + spawnInterval && queenBeebehaviour.state == "EnragedSummoning")
        {
            EnragedSpawnObject();
            lastSpawnTime = 0.4f;
            lastSpawnTime = Time.time;
        }
    }

    void SpawnObject()
    {
        if (queenBeebehaviour.state == "Summoning" && queenBeeHeadAnimation.hasAttacked == false)
        {
            for (int i = 0; i < 3; i++)
            {
                GameObject Bee = Instantiate(objectToSpawn, transform.position, transform.rotation);
                Bee beeScript = Bee.GetComponent<Bee>();
                if (beeScript != null)
                {
                    beeScript.entryType = global::EntryType.onTheSpot;
                }
            }
        }
    }
    void EnragedSpawnObject()
    {
        if (queenBeebehaviour.state == "EnragedSummoning" && queenBeeHeadAnimation.hasAttacked == false)
        {
            for (int i = 0; i < 5; i++)
            {
                GameObject Bee = Instantiate(objectToSpawn, transform.position, transform.rotation);
                Bee beeScript = Bee.GetComponent<Bee>();
                if (beeScript != null)
                {
                    beeScript.entryType = global::EntryType.onTheSpot;
                }
            }
        }
    }

    void timeRundown()
    {
        Vector3 currentPosition = transform.position;
        transform.position = new Vector3(currentPosition.x, Random.Range(11.02f, -12.9f), currentPosition.z);
    }
}