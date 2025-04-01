using UnityEngine;

public class AppearEffect : MonoBehaviour
{
    [SerializeField] ParticleSystem explosionTeleport;
    public float introCooldown = 5f;
    void Start()
    {
        
    }

    void Update()
    {
        introCooldown -= Time.deltaTime;
        if (introCooldown <= 0f)
        {
            explosionTeleport.Play();
        }
    }
}
