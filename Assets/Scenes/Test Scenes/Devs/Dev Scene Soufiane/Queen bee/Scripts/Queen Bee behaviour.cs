using UnityEngine;

public class QueenBeebehaviour : MonoBehaviour
{
    [SerializeField] public string state = "Idle";
    private string[] states = { "Idle", "HoneyAttack", "Summoning", /*"Laser"*/ };
    public float stateChangeInterval = 3f;
    public float floatSpeed = 1f;
    public float floatAmount = 0.2f;
    public float moveSpeed = 0.5f;
    public float moveAmount = 0.1f;
    private float baseY;
    private float randomOffset;

    void Start()
    {
        baseY = transform.position.y;
        randomOffset = Random.Range(0f, 100f);
        InvokeRepeating(nameof(ChangeState), stateChangeInterval, stateChangeInterval);
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

        float newInterval = (state == "Laser") ? 6f : 3f;

        if (newInterval != stateChangeInterval)
        {
            stateChangeInterval = newInterval;
            CancelInvoke(nameof(ChangeState));
            InvokeRepeating(nameof(ChangeState), stateChangeInterval, stateChangeInterval);
        }
    }

    void Update()
    {
        float newY = baseY + Mathf.Sin(Time.time * floatSpeed) * floatAmount;
        transform.position = new Vector3(4.28f, newY, 0);
    }
}
