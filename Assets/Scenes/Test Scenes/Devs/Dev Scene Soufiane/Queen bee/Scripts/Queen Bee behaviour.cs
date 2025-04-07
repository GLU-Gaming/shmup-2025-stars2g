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

    [SerializeField] private mapScroll mapScroll;
    [SerializeField] private QueenBeeHealth queenBeeHealth;
    public bool isEnraged = false;
    public bool isDying = false;  // NEW: Stops behavior when dying

    void Start()
    {
        {
            if (mapScroll == null)
                mapScroll = FindAnyObjectByType<mapScroll>();
        }
        queenBeeHealth = FindAnyObjectByType<QueenBeeHealth>();
        baseY = transform.position.y;
        InvokeRepeating(nameof(ChangeState), stateChangeInterval, stateChangeInterval);
    }

    void ChangeState()
    {
        if (isDying == true) return; // NEW: Stop state changes when dying

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
        if (queenBeeHealth.displayedHealth <= 0)
        { 
            TriggerDeath();
        }
            if (isDying == true) return;

            // Floating logic (only if not dying)
            Vector3 currentPosition = transform.position;
            float newY = baseY + Mathf.Sin(Time.time * (isEnraged ? 4f : floatSpeed)) * (isEnraged ? 0.2f : floatAmount);
            transform.position = new Vector3(currentPosition.x, newY, 0);

            // Enrage logic
            if (!isEnraged && queenBeeHealth.displayedHealth < queenBeeHealth.maxHealth / 2)
            {
                isEnraged = true;
                mapScroll.ScrollSpeed = mapScroll.ScrollSpeed + mapScroll.ScrollSpeed;
                Debug.Log("Boss is now enraged!");
            }
        }

    // NEW: Called when the boss starts dying
    public void TriggerDeath()
    {
        if (isDying == false)
        {
            isDying = true;
            isEnraged = false;
            state = null;
            enragedStates = null;
            mapScroll.ScrollSpeed = mapScroll.ScrollSpeed / 2;
            CancelInvoke(); // Cancel everything
        }
        //Debug.Log("Queen Bee is dying. All behaviors stopped.");
        // Optionally: this.enabled = false;
    }

}
