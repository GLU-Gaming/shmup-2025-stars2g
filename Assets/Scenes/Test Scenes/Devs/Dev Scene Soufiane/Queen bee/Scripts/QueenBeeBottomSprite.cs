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
            transform.localRotation = Quaternion.Euler(-135.7f, -90, 0);
        }
        if (queenBeebehaviour != null && queenBeebehaviour.state == "EnragedHoneyAttack")
        {
            transform.localRotation = Quaternion.Euler(-135.7f, -90, 0);
        }
        else if (queenBeebehaviour.state != "HoneyAttack" && queenBeebehaviour.state != "EnragedHoneyAttack")
        {
            transform.localRotation = Quaternion.Euler(-90, -90, 0);
        }
    }
}
