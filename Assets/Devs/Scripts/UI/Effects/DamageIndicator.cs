using UnityEngine;
using UnityEngine.UI;

public class DamageIndicator : MonoBehaviour
{
    [SerializeField] Image image;
    float alpha;

    private void Update()
    {
        if (alpha > 0)
        {
            alpha -= Time.deltaTime/2;

        }

        alpha = Mathf.Clamp(alpha, 0, 1);
        image.color = new Color(image.color.r, image.color.g, image.color.b, alpha);
    }

    public void Damaged()
    {
        alpha = .3f;
    }
}
