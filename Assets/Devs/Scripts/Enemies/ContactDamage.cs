using UnityEngine;

public class ContactDamage : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GameObject.FindFirstObjectByType<PlayerHealth>().DamagePlayer(10);//sinds de player niet direct is gelinkt aan de health.
        }
    }
}
