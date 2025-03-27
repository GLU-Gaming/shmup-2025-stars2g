using UnityEngine;

public enum EffectType
{
    Poison,
    Overclock,
    Slow
}

public class EffectManager : MonoBehaviour
{
    [SerializeField] GameObject poisonEffectPrefab;
    [SerializeField] GameObject overclockEffectPrefab;
    [SerializeField] GameObject slowEffectPrefab;

    bool isPoisoned = false;
    bool isOverclock = false;
    bool isSlowed = false;

    poison poisonEffect;
    overclock clockEffect;
    slowed slowEffect;

    public void ApplyEffect(EffectType type)
    {
        switch(type)
        {
            case EffectType.Poison:
                if (isPoisoned)
                {
                    GameObject FX1 = Instantiate(poisonEffectPrefab);
                    poisonEffect = FX1.GetComponent<poison>();
                    isPoisoned = true;
                } else
                {
                    poisonEffect.Timer = 5;
                }
                    break;
            case EffectType.Overclock:
                if (isOverclock)
                {
                    GameObject FX1 = Instantiate(poisonEffectPrefab);
                    poisonEffect = FX1.GetComponent<poison>();
                    isOverclock = true;
                }
                else
                {
                    poisonEffect.Timer = 5;
                }
                break;
            case EffectType.Slow:
                isSlowed = true;
                break;
        }
    }
}
