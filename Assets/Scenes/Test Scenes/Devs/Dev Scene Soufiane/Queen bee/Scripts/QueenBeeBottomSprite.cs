using UnityEngine;

public class QueenBeeBottomSprite : MonoBehaviour
{
    private QueenBeebehaviour queenBeebehaviour;
    private QueenBeeBottomAnimation queenBeeBottomAnimation;

    void Start()
    {
        queenBeeBottomAnimation = FindFirstObjectByType <QueenBeeBottomAnimation>();
        queenBeebehaviour = FindFirstObjectByType<QueenBeebehaviour>();
    }

    void FixedUpdate()
    {
        if (queenBeebehaviour != null && queenBeebehaviour.state == "HoneyAttack")
        {
            transform.localRotation = Quaternion.Euler(0, 0, -52.74f);
        }
        else if (queenBeebehaviour != null && queenBeebehaviour.state == "Idle")
        {
            transform.localRotation = Quaternion.Euler(0, 0, 5.05f);
        }
        else if (queenBeebehaviour != null && queenBeebehaviour.state == "Summon")
        {
            transform.localRotation = Quaternion.Euler(0, 0, 5.05f);
        }
        else if (queenBeebehaviour != null && queenBeebehaviour.state == "Laser")
        {
            transform.localRotation = Quaternion.Euler(0, 0, 5.05f);
        }
    }
}
