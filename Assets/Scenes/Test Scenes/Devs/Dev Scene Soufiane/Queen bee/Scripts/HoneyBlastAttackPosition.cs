using UnityEngine;

public class HoneyBlastAttackPosition : MonoBehaviour
{
    private QueenBeebehaviour queenBeebehaviour;
    private QueenBeeBottomAnimation queenBeeBottomAnimation;

    void Start()
    {
        queenBeeBottomAnimation = FindFirstObjectByType<QueenBeeBottomAnimation>();
        queenBeebehaviour = FindFirstObjectByType<QueenBeebehaviour>();
    }

    void FixedUpdate()
    {
        if (queenBeebehaviour != null && queenBeebehaviour.state == "HoneyAttack")
        {
            transform.localRotation = Quaternion.Euler(24.95f, -90, 0);
        }
        else if (queenBeebehaviour != null && queenBeebehaviour.state == "Idle")
        {
            transform.localRotation = Quaternion.Euler(24.95f, -90, 0);
        }
        else if (queenBeebehaviour != null && queenBeebehaviour.state == "Summon")
        {
            transform.localRotation = Quaternion.Euler(24.95f, -90, 0);
        }
    }
}
