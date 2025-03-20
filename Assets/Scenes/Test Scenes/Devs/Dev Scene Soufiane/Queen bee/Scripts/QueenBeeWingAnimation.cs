using UnityEngine;

public class QueenBeeWingAnimation : MonoBehaviour
{
    public float flapSpeed = 180f;
    public float flapAngle = 4f;
    private float flapTime;

    private QueenBeebehaviour queenBeebehaviour;

    void Start()
    {
        queenBeebehaviour = FindFirstObjectByType<QueenBeebehaviour>();
    }

    void FixedUpdate()
    {
            flapTime += Time.deltaTime * flapSpeed;

            float angle = Mathf.Sin(flapTime) * flapAngle;

            transform.localRotation = Quaternion.Euler(0, 0, angle);
    }
}
