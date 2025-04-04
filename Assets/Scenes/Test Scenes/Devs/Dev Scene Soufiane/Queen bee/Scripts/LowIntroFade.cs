using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine.UI;

public class FadeAndDestroy : MonoBehaviour
{

    /*
     De basis code is geschreven door Soufiane
    voor Quality Assurance is het nog aangepast door Thomas waar het nodig was.
    */

    [SerializeField] private float fadeDuration = 1f;  // Time to fade in/out
    [SerializeField] private float visibleDuration = 2f;  // Time before fading out
    [SerializeField] private float destroyDelay = 0.5f;  // Delay before destroying object

    private List<Renderer> renderers = new List<Renderer>();  // 3D Mesh and Sprite Renderers
    private CanvasGroup canvasGroup;  // UI Elements


    //Added by Thomas
    [SerializeField] Image HazardTop;
    [SerializeField] Image HazardBottom;
    [SerializeField] Image incomingText;
    [SerializeField] RectTransform Offset;
    //--------------------------------------

    private void Start()
    {
        // Try to get a CanvasGroup (for UI elements)
        canvasGroup = GetComponent<CanvasGroup>();

        // Find all renderers in this object and its children
        renderers.AddRange(GetComponentsInChildren<Renderer>());

        StartCoroutine(DoSequence());
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

    IEnumerator DoSequence() //function added by Thomas
    {
        incomingText.color = new Color(1, 0.4509434f, 0.4509434f, 0);
        HazardBottom.color = new Color(1, 0.9111129f, 0, 0);
        HazardTop.color = new Color(1, 0.9111129f, 0, 0);
        Offset.localScale = new Vector3(1,1,1);
        HazardTop.DOColor(new Color(1, 0.9111129f, 0, 1), 0.5f);
        HazardBottom.DOColor(new Color(1, 0.9111129f, 0, 1), 0.5f);
        incomingText.DOColor(new Color(1, 0.4509434f, 0.4509434f, 1),0.5f);
        Offset.DOScale(new Vector3(1, 1, 1), 0.5f).SetEase(Ease.InSine);
        HazardTop.GetComponent<RectTransform>().DOSizeDelta(new Vector2(6000f, 141.08f), 4).SetEase(Ease.Linear);
        HazardBottom.GetComponent<RectTransform>().DOSizeDelta(new Vector2(6000f, 141.08f), 4).SetEase(Ease.Linear);
        incomingText.GetComponent<RectTransform>().DOSizeDelta(new Vector2(5000f, 173.8f), 4).SetEase(Ease.Linear);
        yield return new WaitForSeconds(3f);
        HazardTop.DOColor(new Color(1, 0.9111129f, 0, 0), 0.5f);
        HazardBottom.DOColor(new Color(1, 0.9111129f, 0, 0), 0.5f);
        incomingText.DOColor(new Color(1, 0.4509434f, 0.4509434f, 0), 0.5f);

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
