using UnityEngine;

public class QueenBeebehaviour : MonoBehaviour
{
    [SerializeField] public string state = "Idle";
    private string[] states = { "Idle", "HoneyAttack", "Summoning", "Laser" };
    private string[] enragedStates = { "EnragedIdle", "EnragedHoneyAttack", "EnragedSummoning", "EnragedLaser" };
    public float stateChangeInterval = 3f;
    public float floatSpeed = 1f;
    public float floatAmount = 0.2f;
    public float moveSpeed = 0.5f;
    public float moveAmount = 0.1f;
    private float baseY;
    private float randomOffset;

    [SerializeField] private QueenBeeHealth queenBeeHealth;
    private bool isEnraged = false;

    void Start()
    {
        queenBeeHealth = FindAnyObjectByType<QueenBeeHealth>();
        baseY = transform.position.y;
        randomOffset = Random.Range(0f, 100f);
        InvokeRepeating(nameof(ChangeState), stateChangeInterval, stateChangeInterval);
    }

    void ChangeState()
    {

        string[] statePool = isEnraged ? enragedStates : states;

        string newState;
        do
        {
            newState = statePool[Random.Range(0, statePool.Length)];
        } while (newState == state);

        state = newState;
        Debug.Log("New State: " + state);

        stateChangeInterval = isEnraged ? (state == "EnragedLaser" ? 4f : 2f) : (state == "Laser" ? 6f : 3f);

        CancelInvoke(nameof(ChangeState));
        InvokeRepeating(nameof(ChangeState), stateChangeInterval, stateChangeInterval);
    }

    void Update()
    {
        Vector3 currentPosition = transform.position;
        float newY = baseY + Mathf.Sin(Time.time * (isEnraged ? 4f : floatSpeed)) * (isEnraged ? 0.2f : floatAmount);
        transform.position = new Vector3(currentPosition.x, newY, 0);
        if (!isEnraged && queenBeeHealth.displayedHealth < 1250)
        {
            isEnraged = true;
            Debug.Log("Boss is now enraged!");
        }
    }
}

