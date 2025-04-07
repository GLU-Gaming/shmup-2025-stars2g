using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;
using UnityEngine.Rendering.Universal.Internal;
public class MenuHandler : MonoBehaviour
{
    Transitions Transitions;

    [SerializeField] RectTransform Logo;
    [SerializeField] RectTransform StartPanel;
    [SerializeField] RectTransform Main;
    [SerializeField] RectTransform Creds;
    private void Start()
    {
        Transitions = GameObject.FindWithTag("Transitionmanager").GetComponent<Transitions>();
        Transitions.SetTransition(false);
    }
    public void LoadLevel(string SceneName)
    {
        StartCoroutine(DoTransit(SceneName));
    }

    IEnumerator DoTransit(string SceneName)
    {
        Transitions.SetTransition(true);
        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadScene(SceneName);
    }

    IEnumerator OpenMain()
    {
        StartPanel.DOLocalMoveY(-700, 0.5f).SetEase(Ease.InOutSine);
        yield return new WaitForSeconds(1f);
        Logo.DOScale(Vector3.one, 1f).SetEase(Ease.InOutSine);
        Logo.DOLocalMoveY(290.7f, 1f).SetEase(Ease.InOutSine);
        yield return new WaitForSeconds(0.3f);
        Main.DOLocalMoveY(-172.4f, 0.8f).SetEase(Ease.InOutSine);
    }

    IEnumerator ToggleCreds(bool Toggle)
    {
        if (Toggle)
        {
            Main.DOLocalMoveY(-750f, .8f).SetEase(Ease.InOutSine);
            yield return new WaitForSeconds(0.5f);
            Logo.DOLocalMoveY(760f, 0.5f).SetEase(Ease.InOutSine);
            Creds.DOLocalMoveX(0f, 1f).SetEase(Ease.InOutSine);
        } else
        {
            Creds.DOLocalMoveX(-1950.45f, 1f).SetEase(Ease.InOutSine);
            yield return new WaitForSeconds(0.5f);
            Logo.DOLocalMoveY(290.7f, 1f).SetEase(Ease.InOutSine);
            yield return new WaitForSeconds(0.3f);
            Main.DOLocalMoveY(-172.4f, .8f).SetEase(Ease.InOutSine);
        }
    }









    #region Functions to assign to the buttons
    public void LoadMenu()
    {
        StartCoroutine(OpenMain());
    }

    public void LoadCredits(bool Tog)
    {
        StartCoroutine(ToggleCreds(Tog));
    }
    #endregion
}
