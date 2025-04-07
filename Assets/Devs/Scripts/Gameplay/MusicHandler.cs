using UnityEngine;
using DG.Tweening;
using System.Collections;
[RequireComponent(typeof(AudioSource))]
public class MusicHandler : MonoBehaviour
{
    //A primary Music handler for base levels, other music is applied by other scripts.
    AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void SetVolume(float targetVol)
    {
        StartCoroutine(SetVolumeIE(targetVol));
    }

    IEnumerator SetVolumeIE(float TargetVol)
    {
        audioSource.DOFade(TargetVol, 1f);
        yield return new WaitForSeconds(1f);
    }
}
