using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class MenuIntro : MonoBehaviour
{
    [SerializeField] Image Overlay;
    [SerializeField] Image Background;
    [SerializeField] RectTransform CloudsBack;
    [SerializeField] Image CloudsFront;
    [SerializeField] RectTransform Beeswarm;
    Image SwarmImage;
    [SerializeField] RectTransform Queen;
    [SerializeField] RectTransform QueenUpper;
    [SerializeField] RectTransform QueenArms;
    [SerializeField] RectTransform QueenClaws;
    [SerializeField] Image QueenFlare;
    [SerializeField] Image OverlayFlare;

    private void Start()
    {
        Overlay.color = new Color(0, 0, 0, 1);
        QueenUpper.localPosition = new Vector3(0, 57.4f, 0);
        QueenFlare.color = new Color(1, 1, 1, 0);
        QueenArms.localPosition = new Vector3(0, -45.49f, 0);
        QueenClaws.localPosition = new Vector3(0, -244.76f, 0);
        StartCoroutine(AnimateIntro());
    }

    IEnumerator AnimateIntro()
    {
        yield return new WaitForSeconds(1);
        OverlayFlare.DOColor(new Color(1, 1, 1, 1), .3f).SetEase(Ease.OutSine);
        yield return new WaitForSeconds(.3f);
        OverlayFlare.DOColor(new Color(1, 1, 1, 0), .3f).SetEase(Ease.InSine);
        yield return new WaitForSeconds(.6f);
        Overlay.DOColor(new Color(0, 0, 0, 0), .3f).SetEase(Ease.Linear);
        yield return new WaitForSeconds(.3f);
        
    }
}
