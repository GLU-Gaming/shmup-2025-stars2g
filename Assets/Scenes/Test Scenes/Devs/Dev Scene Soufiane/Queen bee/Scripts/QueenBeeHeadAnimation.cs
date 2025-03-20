using UnityEngine;

public class QueenBeeHeadAnimation : MonoBehaviour
{
    private bool hasAttacked = false;
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
        if (queenBeebehaviour != null && queenBeebehaviour.state == "Summoning" && !hasAttacked)
        {
            if (!hasAttacked)
            {
                Screech();
                hasAttacked = true;
            }
        }

        else if (queenBeebehaviour.state != "Summoning")
        {
            hasAttacked = false;
        }
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