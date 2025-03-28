using UnityEngine;
using System;
using System.Collections.Generic;

public class EffectManager : MonoBehaviour
{
    [SerializeField] List<Effect> effects;

    [System.Serializable]
    public class Effect
    {
        public GameObject effect;
        public bool Triggered;
    }


    #region ugly code
    poison poisonScript;
    overclock clockScript;
    slowed slowScript;
    #endregion

    public void ApplyEffect(int type)
    {
        switch (type)
        {
            case 0:
                if (effects[0].Triggered == false)
                {
                    GameObject Poison = Instantiate(effects[0].effect);
                    poisonScript = Poison.GetComponent<poison>();
                    effects[0].Triggered = true;
                } else
                {
                    poisonScript.Timer = poisonScript.poisonDuration;
                }
                    break;
            case 1:
                if (effects[1].Triggered == false)
                {
                    GameObject overClock = Instantiate(effects[0].effect);
                    clockScript = overClock.GetComponent<overclock>();
                    effects[1].Triggered = true;
                }
                else
                {
                    clockScript.Timer = clockScript.effectDuration;
                }
                break;
            case 2:
                if(effects[2].Triggered == false)
                {
                    GameObject slow = Instantiate(effects[2].effect);
                    slowScript = slow.GetComponent<slowed>();
                    effects[2].Triggered = true;
                }
                else
                {
                    slowScript.Timer = slowScript.effectDuration;
                }
                break;
        }
    }

    public void DisableEffect(int type)
    {
        switch (type)
        {
            case 0:
                poisonScript = null;
                break;
            case 1:
                clockScript = null;
                break;
            case 2:
                slowScript = null;
                break;
        }
    }
}
