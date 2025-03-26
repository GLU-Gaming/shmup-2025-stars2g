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
                transform.localRotation = Quaternion.Euler(0, 0, -20.56f);
                transform.localPosition = new Vector3(1.62f, 3.38f, 0);
                Screech();
                hasAttacked = true;
            }
        }

        else if (queenBeebehaviour.state != "Summoning")
        {
            transform.localRotation = Quaternion.Euler(0, 0, 0);
            transform.localPosition = new Vector3(1.79f, 3.12f, 0);
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