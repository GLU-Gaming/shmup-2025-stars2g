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
            transform.localRotation = Quaternion.Euler(-10.085f, -90, 0);
            transform.localPosition = new Vector3(-10.33f, -2.57f, 0);
        }
        else if (queenBeebehaviour != null && queenBeebehaviour.state != "HoneyAttack")
        {
            transform.localRotation = Quaternion.Euler(35f, -90, 0);
            transform.localPosition = new Vector3(-8f, -6.109667f, 0);
        }
    }
}
