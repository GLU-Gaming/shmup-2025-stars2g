using UnityEngine;

public class QueenBeebehaviour : MonoBehaviour
{
    [SerializeField] public string state = "Idle";
    private string[] states = { "Idle", "HoneyAttack", "Summoning", "Laser" };
    public float stateChangeInterval = 3.5f;
    public float floatSpeed = 1f;
    public float floatAmount = 0.2f;
    public float moveSpeed = 0.5f;
    public float moveAmount = 0.1f;
    private float baseY;
    private float randomOffset;

    void Start()
    {
        InvokeRepeating(nameof(ChangeState), stateChangeInterval, stateChangeInterval);
        baseY = transform.position.y;
        randomOffset = Random.Range(0f, 100f);
    }

    void ChangeState()
    {
        string newState;
        do
        {
            newState = states[Random.Range(0, states.Length)];
        } while (newState == state);

        state = newState;
        Debug.Log("New State: " + state);
    }

    void Update()
    {
        if (state == "Idle")
        {
            float newY = baseY + Mathf.Sin(Time.time * floatSpeed) * floatAmount;
            transform.position = new Vector3(0, newY, 0);
        }
    }
}

