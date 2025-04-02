using UnityEngine;

public class Powerup : MonoBehaviour
{
    EffectManager effectManager;
    [SerializeField, Range(0,4)] int effect;

    private void Start()
    {
        effectManager = GameObject.FindFirstObjectByType<EffectManager>();
    }

    private void Update()
    {
        transform.Translate(Vector3.left * Time.deltaTime * 6);
        if(transform.position.x < -25.2f)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            effectManager.ApplyEffect(effect);
            Destroy(gameObject);
        }
    }
}
