using UnityEngine;

public class DestroyAfterTime : MonoBehaviour
{
    [SerializeField] float DestroyTime;

    private void Start()
    {
        Destroy(gameObject, DestroyTime);
    }
}
