using UnityEngine;
using DG.Tweening;
using System.Collections;
using UnityEngine.UI;

public class Transitions : MonoBehaviour
{
    [SerializeField] RectTransform FadeTop;
    [SerializeField] RectTransform FadeBottom;

    private void Start()
    {
        DontDestroyOnLoad(gameObject);
    }

    IEnumerator fadeIn()
    {
        FadeTop.DOLocalMoveY(270, 0.5f);
        FadeBottom.DOLocalMoveY(-270, 0.5f);
        yield return null;
    }

    IEnumerator fadeOut()
    {
        FadeTop.DOLocalMoveY(810, 0.5f);
        FadeBottom.DOLocalMoveY(-810, 0.5f);
        yield return null;
    }

    public void SetTransition(bool fade)
    {       
        if (fade)
        {
            StartCoroutine(fadeIn());
        } else
        {
            StartCoroutine(fadeOut());
        }
    }
}
