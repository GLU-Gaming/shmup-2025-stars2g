using UnityEngine;
using UnityEngine.Audio;

public class Phase2Transition : MonoBehaviour
{
    private QueenBeebehaviour queenBeebehaviour;
    private AudioSource pissedAudioSource;
    public ParticleSystem steamclouds;
    private bool hasPlayed = false;

    void Start()
    {
        pissedAudioSource = GetComponent<AudioSource>();
        queenBeebehaviour = FindFirstObjectByType<QueenBeebehaviour>();
    }

    void FixedUpdate()
    {
        if (queenBeebehaviour != null && queenBeebehaviour.isEnraged == true)
        {
            if (!hasPlayed)
            {
                hasPlayed = true;
                if (steamclouds != null)
                {
                    steamclouds.Play();
                }
                if (pissedAudioSource != null)
                {
                    pissedAudioSource.Play();
                }
            }
        }
    }
}
