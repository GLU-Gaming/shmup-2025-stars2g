using UnityEngine;

public class TransitionCreate : MonoBehaviour
{
    [SerializeField] GameObject TransitionPrefab;


    Transitions TransitionScript;

    private void Awake()
    {
        TransitionScript = FindFirstObjectByType<Transitions>();
        if (TransitionScript == null)
        {
            GameObject transitionObject = Instantiate(TransitionPrefab);

        }
    }
}
