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
    }

    void timeRundown()
    {
        Vector3 currentPosition = transform.position;
        transform.position = new Vector3(currentPosition.x, Random.Range(11.02f, -12.9f), currentPosition.z);
    }
}