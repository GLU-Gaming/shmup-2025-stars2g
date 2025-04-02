using UnityEngine;

public class QueenBeeHeadAnimation : MonoBehaviour
{
    public bool hasAttacked = false;
    private QueenBeebehaviour queenBeebehaviour;
    public ParticleSystem queenBeeScreech;
    private AudioSource audioSource;
    public GameObject objectToSpawn; //dependant on bee coding
    public Transform spawnPoint;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        queenBeebehaviour = FindFirstObjectByType<QueenBeebehaviour>();
    }

    void FixedUpdate()
    {

        if ((queenBeebehaviour.state == "Summoning" || queenBeebehaviour.state == "EnragedSummoning") && !hasAttacked && !queenBeebehaviour.isDying)
        {
            transform.localRotation = Quaternion.Euler(0, 0, -20.56f);
            transform.localPosition = new Vector3(1.62f, 3.38f, 0);
            Screech();
            hasAttacked = true;
        }
        else if (queenBeebehaviour.state != "Summoning" && queenBeebehaviour.state != "EnragedSummoning")
        {
            transform.localRotation = Quaternion.Euler(0, 0, 0);
            transform.localPosition = new Vector3(1.79f, 3.12f, 0);
            hasAttacked = false;
        }
    }

    void PerformScreech()
    {
        transform.localRotation = Quaternion.Euler(0, 0, -20.56f);
        transform.localPosition = new Vector3(1.62f, 3.38f, 0);
        Screech();
    }

    void Screech()
    {
        audioSource.Play();
        if (queenBeeScreech != null)
        {
            queenBeeScreech.Play();
            if (queenBeebehaviour.state == "Summoning")
            {
                SpawnObject();
            }
            if (queenBeebehaviour.state == "EnragedSummoning")
            {
                EnragedSpawnObject();
            }
        }
    }
    void SpawnObject()
    {
        if (queenBeebehaviour.state == "Summoning" && hasAttacked == false && queenBeebehaviour.isDying != true)
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
        if (queenBeebehaviour.state == "EnragedSummoning" && hasAttacked == false && queenBeebehaviour.isDying != true)
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
}