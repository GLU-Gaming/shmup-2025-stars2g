using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class Finish : MonoBehaviour
{
    public GameObject winUI;
    public GameObject player;

    MusicHandler MusicHandler;

    mapScroll scoller;
    bool Debounce = false;

    private void Start()
    {
        MusicHandler = FindFirstObjectByType<MusicHandler>();
        player = GameObject.FindGameObjectWithTag("Player");
        scoller = FindFirstObjectByType<mapScroll>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerWin();
        }
    }

    public void PlayerWin()
    {
        if(!Debounce)
        {
            Debounce = true;
            StartCoroutine(AnimateFinish());
        }
    }

    IEnumerator AnimateFinish()
    {
        player.GetComponent<PlayerMovement>().EndAnim();
        MusicHandler.SetVolume(0f);
        yield return DOTween.To(() => scoller.ScrollSpeed, x => scoller.ScrollSpeed = x, 0f, 1f);
        yield return DOTween.To(() => Time.timeScale, x => Time.timeScale = x, 0, 3f).SetUpdate(true);
        yield return new WaitForSecondsRealtime(3);
        winUI.SetActive(true);
    }
}

