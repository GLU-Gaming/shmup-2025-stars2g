using UnityEngine;

public class QueenBeeHeadSprite : MonoBehaviour
{
    private QueenBeebehaviour queenBeebehaviour;
    private QueenBeeHeadAnimation queenBeeHeadAnimation;

    void Start()
    {
        queenBeeHeadAnimation = FindFirstObjectByType<QueenBeeHeadAnimation>();
        queenBeebehaviour = FindFirstObjectByType<QueenBeebehaviour>();
    }

    void Update()
    {
        
    }
}
