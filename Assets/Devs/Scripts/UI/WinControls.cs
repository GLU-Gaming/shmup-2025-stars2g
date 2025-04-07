using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class WinControls : MonoBehaviour
{

    Transitions Transitions;

    private void Start()
    {
        Transitions = GameObject.FindWithTag("Transitionmanager").GetComponent<Transitions>();
    }

    public void LoadScene(string SceneName)
    {
        Time.timeScale = 1f;
        StartCoroutine(DoTransit(SceneName));

    }

    public void LoadNextLeveL()
    {
        Time.timeScale = 1f;
        switch(SceneManager.GetActiveScene().name)
        {
            case "RegularLevel":
                StartCoroutine(DoTransit("BossLevel"));
                break;
            case "BossLevel":
                StartCoroutine(DoTransit("RegularLevel"));
                break;
        }
    }

    public void Reload()
    {
        Time.timeScale = 1f;
        StartCoroutine(DoTransit(SceneManager.GetActiveScene().name));
    }
    IEnumerator DoTransit(string SceneName)
    {
        Transitions.SetTransition(true);
        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadScene(SceneName);
    }
}
