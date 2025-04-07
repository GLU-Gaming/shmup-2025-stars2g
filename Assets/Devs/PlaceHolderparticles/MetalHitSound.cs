using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class MetalHitSound : MonoBehaviour
{
    AudioSource audioSource;
    [SerializeField] AudioClip[] metalHitSounds;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = metalHitSounds[Random.Range(0, metalHitSounds.Length)];
        audioSource.Play();
    }
}
