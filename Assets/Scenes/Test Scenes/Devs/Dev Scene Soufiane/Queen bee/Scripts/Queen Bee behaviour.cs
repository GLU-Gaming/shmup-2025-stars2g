using UnityEngine;

public class QueenBeebehaviour : MonoBehaviour
{
    [SerializeField] public string state = "Idle";
    private string[] states = { "Idle", "HoneyAttack", "Summoning", "Laser" };
    private string[] enragedStates = { "EnragedIdle", "EnragedHoneyAttack", "EnragedSummoning", "EnragedLaser" };

    public float stateChangeInterval = 3f;
    public float floatSpeed = 1f;
    public float floatAmount = 0.2f;
    private float baseY;

    [SerializeField] private QueenBeeHealth queenBeeHealth;
    public bool isEnraged = false;
    public bool isDying = false;  // NEW: Stops behavior when dying

    void Start()
    {
        queenBeeHealth = FindAnyObjectByType<QueenBeeHealth>();
        baseY = transform.position.y;
        InvokeRepeating(nameof(ChangeState), stateChangeInterval, stateChangeInterval);
    }

    void ChangeState()
    {
        if (isDying) return; // NEW: Stop state changes when dying

        string[] statePool = isEnraged ? enragedStates : states;
        string newState;

        do
        {
            newState = statePool[Random.Range(0, statePool.Length)];
        } while (newState == state);

        state = newState;
        Debug.Log("New State: " + state);

        stateChangeInterval = isEnraged ? (state == "EnragedLaser" ? 6f : 2f) : (state == "Laser" ? 6f : 3f);

        CancelInvoke(nameof(ChangeState));
        InvokeRepeating(nameof(ChangeState), stateChangeInterval, stateChangeInterval);
    }

    void Update()
    {
        if (isDying)
        {
            state = null;
            enragedStates = null;
        }; // NEW: Stop movement when dying

        Vector3 currentPosition = transform.position;
        float newY = baseY + Mathf.Sin(Time.time * (isEnraged ? 4f : floatSpeed)) * (isEnraged ? 0.2f : floatAmount);
        transform.position = new Vector3(currentPosition.x, newY, 0);

        if (!isEnraged && queenBeeHealth.displayedHealth < 1250)
        {
            isEnraged = true;
            Debug.Log("Boss is now enraged!");
        }
    }

    // NEW: Called when the boss starts dying
    public void TriggerDeath()
    {
        isDying = true;
        isEnraged = false;
        state = "cancelattacks";
        CancelInvoke(nameof(ChangeState));
    }
    public bool IsDying()
    {
        return isDying;
    }

}
