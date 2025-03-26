using UnityEngine;

public class BeeSpawner : MonoBehaviour
{
    public GameObject objectToSpawn;
    public float spawnInterval = 0.5f;
    private float lastSpawnTime;
    public float movementInterval = 0.01f;
    private float lastMoveTime;
    private QueenBeebehaviour queenBeebehaviour;
    private QueenBeeHeadAnimation queenBeeHeadAnimation;

    private int spawnCount = 0;
    private const int maxSpawnCount = 3;

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

        if (queenBeebehaviour.state == "Summoning" && spawnCount == maxSpawnCount)
        {
            spawnCount = 0;
        }

        if (Time.time >= lastSpawnTime + spawnInterval && queenBeebehaviour.state == "Summoning" && spawnCount < maxSpawnCount)
        {
            SpawnObject();
            lastSpawnTime = Time.time;
        }
    }

    void SpawnObject()
    {
        if (objectToSpawn != null && queenBeebehaviour.state == "Summoning" && maxSpawnCount <= 3)
        {
            Instantiate(objectToSpawn, transform.position, transform.rotation);
            spawnCount = +3;
        }
    }

    void timeRundown()
    {
        Vector3 currentPosition = transform.position;
        transform.position = new Vector3(currentPosition.x, Random.Range(11.02f, -12.9f), currentPosition.z);
    }
}
