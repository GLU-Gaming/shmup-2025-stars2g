using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class MenuFlicker : MonoBehaviour
{
    [SerializeField] Image targetImage;
    [SerializeField] float minFlickerInterval = 0.1f;
    [SerializeField] float maxFlickerInterval = 0.5f;
    [SerializeField] float minAlpha = 0.1f;
    [SerializeField] float maxAlpha = 1.0f;

    private void FixedUpdate()
    {
        bool FlickerValue;
        FlickerValue = (int)Random.Range(0, 100) >= 97;

        if (FlickerValue)
        {
            targetImage.color = new Color(targetImage.color.r, targetImage.color.g, targetImage.color.b, 0.7f);
        }
        else
        {
            targetImage.color = new Color(targetImage.color.r, targetImage.color.g, targetImage.color.b, 1);
        }
    }
}
