using UnityEngine;

public class QueenBeeHeadAnimation : MonoBehaviour
{
    public bool hasAttacked = false;
    private QueenBeebehaviour queenBeebehaviour;
    public ParticleSystem queenBeeScreech;
    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        queenBeebehaviour = FindFirstObjectByType<QueenBeebehaviour>();
    }


    void FixedUpdate()
    {
        if (queenBeebehaviour.IsDying()) // NEW: If boss is dying, do final screech
        {
            PerformScreech();
            return;
        }

        if (queenBeebehaviour != null && queenBeebehaviour.state == "Summoning" && !hasAttacked && queenBeebehaviour.isDying == false)
        {
            if (!hasAttacked)
            {
                transform.localRotation = Quaternion.Euler(0, 0, -20.56f);
                transform.localPosition = new Vector3(1.62f, 3.38f, 0);
                Screech();
                hasAttacked = true;
            }
        }
        if (queenBeebehaviour != null && queenBeebehaviour.state == "EnragedSummoning" && !hasAttacked && queenBeebehaviour.isDying == false)
        {
            if (!hasAttacked)
            {
                transform.localRotation = Quaternion.Euler(0, 0, -20.56f);
                transform.localPosition = new Vector3(1.62f, 3.38f, 0);
                Screech();
                hasAttacked = true;
            }
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
        }
    }
}