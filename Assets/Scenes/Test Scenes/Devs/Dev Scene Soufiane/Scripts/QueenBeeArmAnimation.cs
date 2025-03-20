using UnityEngine;

public class QueenBeeArmAnimation : MonoBehaviour
{
    public float wiggleSpeed = 0.4f;
    public float wiggleAngle = 3f;
    private float wiggleTime;

    private QueenBeebehaviour queenBeebehaviour;

    void Start()
    {
        queenBeebehaviour = FindFirstObjectByType<QueenBeebehaviour>();
    }

    void FixedUpdate()
    {
        if (queenBeebehaviour != null && queenBeebehaviour.state == "Idle")
        {
            wiggleTime += Time.deltaTime * wiggleSpeed;

            float angle = Mathf.Sin(wiggleTime) * wiggleAngle;

            transform.localRotation = Quaternion.Euler(0, 0, angle);
        }
    }
}
