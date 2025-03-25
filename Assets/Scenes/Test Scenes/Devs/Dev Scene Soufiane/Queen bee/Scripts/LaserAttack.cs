using UnityEngine;

public class LaserAttack : MonoBehaviour
{
    public float disappearTime = 1.4f;
    private QueenBeebehaviour queenBeebehaviour;
    private QueenBeeChargeLaser queenBeeChargeLaser;
    void Start()
    {
        queenBeeChargeLaser = FindFirstObjectByType<QueenBeeChargeLaser>();
        queenBeebehaviour = FindFirstObjectByType<QueenBeebehaviour>();
    }

    void FixedUpdate()
    {
        if (disappearTime == 0)
        {
            Destroy(gameObject);
        }
        if (queenBeebehaviour != null && queenBeebehaviour.state == "Laser" && queenBeeChargeLaser.hasPlayed == true)
        {
            disappearTime -= Time.deltaTime;
        }
        if (queenBeebehaviour != null && queenBeebehaviour.state != "Laser")
        {
            disappearTime = 1.4f;
        }

        }
}
