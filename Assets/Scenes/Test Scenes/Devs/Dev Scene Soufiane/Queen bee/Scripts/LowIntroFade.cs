using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FadeAndDestroy : MonoBehaviour
{
    [SerializeField] private float fadeDuration = 1f;  // Time to fade in/out
    [SerializeField] private float visibleDuration = 2f;  // Time before fading out
    [SerializeField] private float destroyDelay = 0.5f;  // Delay before destroying object

    private List<Renderer> renderers = new List<Renderer>();  // 3D Mesh and Sprite Renderers
    private CanvasGroup canvasGroup;  // UI Elements

    private void Start()
    {
        // Try to get a CanvasGroup (for UI elements)
        canvasGroup = GetComponent<CanvasGroup>();

        // Find all renderers in this object and its children
        renderers.AddRange(GetComponentsInChildren<Renderer>());

        StartCoroutine(FadeSequence());
    }

    private IEnumerator FadeSequence()
    {
        yield return StartCoroutine(FadeIn());

        yield return new WaitForSeconds(visibleDuration);

        yield return StartCoroutine(FadeOut());

        yield return new WaitForSeconds(destroyDelay);

        Destroy(gameObject);
    }

    private IEnumerator FadeIn()
    {
        float elapsedTime = 0f;
        while (elapsedTime < fadeDuration)
        {
            SetAlpha(elapsedTime / fadeDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        SetAlpha(1);
    }

    private IEnumerator FadeOut()
    {
        float elapsedTime = 0f;
        while (elapsedTime < fadeDuration)
        {
            SetAlpha(1 - (elapsedTime / fadeDuration));
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        SetAlpha(0);
    }

    private void SetAlpha(float alpha)
    {
        // For UI elements (CanvasGroup)
        if (canvasGroup != null)
        {
            canvasGroup.alpha = alpha;
        }

        // For 2D & 3D objects (MeshRenderer & SpriteRenderer)
        foreach (Renderer renderer in renderers)
        {
            if (renderer is SpriteRenderer spriteRenderer)
            {
                Color color = spriteRenderer.color;
                color.a = alpha;
                spriteRenderer.color = color;
            }
            else if (renderer.material.HasProperty("_Color")) // For 3D objects with materials
            {
                Color color = renderer.material.color;
                color.a = alpha;
                renderer.material.color = color;
            }
        }
    }
}
